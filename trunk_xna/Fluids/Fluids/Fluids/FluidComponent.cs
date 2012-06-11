using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Charco.Tools;
using System.Threading;


namespace Astro
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class FluidComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch m_spriteBatch;
        Texture2D m_texture;
        Vector2 m_origin;

        // Sim
        private SPHSimulation m_fluidSim;
        private Vector2 m_gravity;
        private ParticleSystem m_particleSystem;
        private bool m_pause;

        // BoundingVolumes
        private CollisionResolver m_collisionSolver;
        private BoundingVolume m_selectedBoundingVolume;
        private List<Vector2> m_pointList;

        // Blobs
        //private BlobRenderer m_blobs;
        private bool m_useBlobs;
        private bool m_drawVel;

        //// OpenTK text
        //private ITextPrinter m_textPrinter;
        //private TextHandle m_textHandleHelp;
        //private TextHandle m_textHandleStats;
        //private TextureFont m_textFont;

        // Misc.
        private Random m_randGen;
        private bool m_showHelp;
        private int m_mouseWheelLastValue;

        AutoResetEvent DrawEvent = new AutoResetEvent( false );
        AutoResetEvent UpdateFluidsEvent = new AutoResetEvent( true );

        Thread UpdateFluidsThread;
        bool Exit = false;
        public FluidComponent( Game game )
            : base( game )
        {
            game.Exiting += new EventHandler<EventArgs>( game_Exiting );
        }

        void game_Exiting( object sender, EventArgs e )
        {
            Exit = true;
            UpdateFluidsEvent.Set( );
            DrawEvent.Set( );
        }

        public override void Initialize( )
        {

           
            //
            // Init. sim
            //
            m_pause = false;
            m_gravity = Constants.GRAVITY * Constants.PARTICLE_MASS;
            m_fluidSim = new SPHSimulation( Constants.CELL_SPACE, Constants.SIM_DOMAIN );
            m_collisionSolver = new CollisionResolver
            {
                BoundingVolumes = new BoundingVolumes
            {
               new OBB
               { 
                  Position    = new Vector2(Constants.SIM_DOMAIN.Width / 1.5f, Constants.SIM_DOMAIN.Height / 2),
                  Extents     = new Vector2(Constants.SIM_DOMAIN.Width / 6 , Constants.SIM_DOMAIN.Height / 30),
               }
            },
                Bounciness = 0.2f,
                Friction = 0.01f,
            };

            // Init. particle system
            double freq = 30 * 0.5f;
            int maxPart = 250;
            m_particleSystem = new ParticleSystem
            {
                Emitters = new ParticleEmitters 
            { 
               new ParticleEmitter
               {
                  Position       = new Vector2(Constants.SIM_DOMAIN.X + Constants.SIM_DOMAIN.Width * 0.75f, Constants.SIM_DOMAIN.Y),
                  VelocityMin    = Constants.PARTICLE_MASS * 0.010f,
                  VelocityMax    = Constants.PARTICLE_MASS * 0.015f,
                  Direction      = new Vector2(0, -1),
                  Distribution   = Constants.SIM_DOMAIN.Width * 0.0001f,
                  Frequency      = freq,
                  ParticleMass   = Constants.PARTICLE_MASS,
               },
                new ParticleEmitter
               {
                  Position       = new Vector2(Constants.SIM_DOMAIN.X + Constants.SIM_DOMAIN.Width * 0.95f, Constants.SIM_DOMAIN.Y),
                  VelocityMin    = Constants.PARTICLE_MASS * 0.01f,
                  VelocityMax    = Constants.PARTICLE_MASS * 0.015f,
                  Direction      = new Vector2(0, -1),
                  Distribution   = Constants.SIM_DOMAIN.Width * 0.0001f,
                  Frequency      = freq,
                  ParticleMass   = Constants.PARTICLE_MASS,
               },
            },

                MaxParticles = maxPart,
                MaxLife = ( int ) (( double ) maxPart / freq / Constants.DELTA_TIME_SEC),
                TestMaxLife = false,
            };


            //
            // Init blobs
            //
            ////m_blobs = new BlobRenderer( 1.0f, 0.4f, 0.8f, BLOB_RADIUS, TEX_SIZE, Color.LightSkyBlue );
            m_useBlobs = true;
            m_drawVel = true;


            //////
            ////// Init OpenGL
            //////
            ////GL.ClearColor( System.Drawing.Color.Black );
            ////GL.PointSize( 5.0f );
            ////GL.Hint( HintTarget.PerspectiveCorrectionHint, HintMode.Nicest );
            ////GL.BlendFunc( BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha );

            ////// Init OpenTK text
            m_showHelp = false;
            ////m_textPrinter = new TextPrinter( );
            ////m_textFont = new TextureFont( new Font( FontFamily.GenericMonospace, 12.0f ) );
            ////m_textPrinter.Prepare(
            ////  "Press [F1] to hide / show this text\r\n"
            ////+ "Press [R] to change particle rendering\r\n"
            ////+ "Press [D] to draw velocity direction\r\n"
            ////+ "Press [E] to switch emitter on / off\r\n"
            ////+ "Press [P] to pause\r\n"
            ////+ "Press [Space] to tilt (add random impulse)\r\n"
            ////+ "Press [Esc] to close the program\r\n"
            ////+ "\r\n"
            ////+ "Use left mouse button <LMB> to select a box\r\n"
            ////+ "Select a box + <LMB> to move the selected box\r\n"
            ////+ "Hold [Ctrl] + <RMB> to remove a box\r\n"
            ////+ "Hold [Ctrl] + <LMB> to draw a new box (AABB)\r\n"
            ////+ "(Release <LMB> to add the drawn box (AABB))\r\n"
            ////+ "\r\n"
            ////+ "Hold [Alt] + <LMB> to exert a negative force field\r\n"
            ////+ "Hold [Alt] + <RMB> to exert a positive force field\r\n"
            ////+ "\r\n"
            ////+ "Use mousewheel <MW> to change some values\r\n"
            ////+ "(Hold [Shift] to change smaller steps)\r\n"
            ////+ "Select a box + <MW> to rotate the selected box\r\n"
            ////+ "Hold [V] + <MW> to change viscosity\r\n"
            ////+ "Hold [B] + <MW> to change bounciness\r\n"
            ////+ "Hold [F] + <MW> to change friction\r\n"
            ////+ "\r\n"
            ////+ "This program is free software (GPLv3) -> License.txt",
            ////m_textFont, out m_textHandleHelp );

            ////// Add Keyboard- and Mouse-Handler
            ////Keyboard.KeyUp += new KeyUpEvent( Keyboard_KeyUp );
            ////Mouse.ButtonDown += new MouseButtonDownEvent( Mouse_ButtonDown );
            ////Mouse.ButtonUp += new MouseButtonUpEvent( Mouse_ButtonUp );

            ////// Init. misc.
            ////m_mouseWheelLastValue = Mouse.Wheel;
            ////base.Initialize( );

            m_spriteBatch = new SpriteBatch( this.Game.GraphicsDevice );
            m_texture = Game.Content.Load<Texture2D>( "particles/yellow_glow" );
            m_origin.X = m_texture.Width;
            m_origin.Y = m_texture.Height;
            m_origin *= 0.5f;

            ThreadStart threadStart = new ThreadStart( UpdateFluids );

            UpdateFluidsThread = new Thread( threadStart );

            UpdateFluidsThread.Start( );


        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update( GameTime gameTime )
        {
            // Solve collisions only for obbs (not particles)
            m_collisionSolver.Solve( );

            // Pause
            if ( m_pause )
            {
                return;
            }

           // UpdateFluids( );

            Logger.Write( m_particleSystem.Particles.Count.ToString() );
            base.Update( gameTime );
        }

        private void UpdateFluids( )
        {
            do
            {
                UpdateFluidsEvent.WaitOne( );
                
                if ( Exit ) break;

                // Update particle system
                this.m_particleSystem.Update( Constants.DELTA_TIME_SEC );

                // Interaction handling
                //AddInteractionForces( );

                // Solve collisions only for particles
                m_collisionSolver.Solve( this.m_particleSystem.Particles );

                // Do simulation
                this.m_fluidSim.Calculate( this.m_particleSystem.Particles, this.m_gravity, Constants.DELTA_TIME_SEC );

                DrawEvent.Set( );
            } while ( true );

        }

        public override void Draw( GameTime gameTime )
        {
            //base.OnRenderFrame( e );

            //GL.Clear( ClearBufferMask.ColorBufferBit );

            ////
            //// Draw particles as meta-circles (meta-balls / blobs)
            ////
            //if ( m_useBlobs )
            //{
            //    m_blobs.Render( this.m_particleSystem.Particles, m_fluidSim.Domain );
            //}
            //// Draw particles as points and a velocity line
            //else
            //{
            //    GL.Disable( EnableCap.Texture2D );
            //    GL.Color4( m_blobs.Color );
            //    foreach ( var particle in this.m_particleSystem.Particles )
            //    {
            //        GL.Begin( BeginMode.Points );
            //        GL.Vertex2( particle.Position.X, particle.Position.Y );
            //        GL.End( );
            //        if ( m_drawVel )
            //        {
            //            Vector2 vel = particle.Position + particle.Velocity * 0.1f;
            //            GL.Begin( BeginMode.Lines );
            //            GL.Vertex2( particle.Position.X, particle.Position.Y );
            //            GL.Vertex2( vel.X, vel.Y );
            //            GL.End( );
            //        }
            //    }
            //}

            DrawEvent.WaitOne( );

            if ( m_useBlobs )
            {
                m_spriteBatch.Begin( SpriteSortMode.Deferred, BlendState.Additive );
                foreach ( var particle in this.m_particleSystem.Particles )
                {
                    m_spriteBatch.Draw( m_texture, particle.Position, null, Color.Goldenrod, 0, m_origin, 1, SpriteEffects.None, 1  );
                }
                m_spriteBatch.End( );
            }else  foreach ( var particle in this.m_particleSystem.Particles )
            {
                Lineas.Instancia.AddRectangle( Matrix.Identity, particle.Position, Constants.CELL_SPACE*0.5f, Color.White );
                //GL.Begin( BeginMode.Points );
                //GL.Vertex2( particle.Position.X, particle.Position.Y );
                //GL.End( );
                if ( m_drawVel )
                {
                    Vector2 vel = particle.Position + particle.Velocity * 0.1f;

                    Lineas.Instancia.AddLine2D( Matrix.Identity, particle.Position, particle.Position + particle.Velocity * 0.1f, Color.White );
                    //GL.Begin( BeginMode.Lines );
                    //GL.Vertex2( particle.Position.X, particle.Position.Y );
                    //GL.Vertex2( vel.X, vel.Y );
                    //GL.End( );
                }
            }

            UpdateFluidsEvent.Set( );


            ////
            //// Draw Bounding Volumes
            ////
            m_collisionSolver.BoundingVolumes.Draw( Color.LightGreen );
            if ( m_selectedBoundingVolume != null )
            {
                //GL.Color3( Color.GreenYellow );
                m_selectedBoundingVolume.Draw( );
            }

            //// Draw PointList
            //if ( m_pointList != null )
            //{
            //    GL.Color4( Color.Red );
            //    GL.Begin( BeginMode.LineStrip );
            //    foreach ( var point in m_pointList )
            //    {
            //        GL.Vertex2( point.X, point.Y );
            //    }
            //    GL.End( );
            //}


            ////
            //// Draw OpenTK text
            ////

            //// Text Background
            //if ( m_showHelp )
            //{
            //    GL.PushAttrib( AttribMask.EnableBit );
            //    GL.Enable( EnableCap.Blend );
            //    GL.Color4( 0.0f, 0.0f, 0.0f, 0.8f );
            //    GL.Begin( BeginMode.Quads );
            //    GL.Vertex2( m_fluidSim.Domain.Left, m_fluidSim.Domain.Bottom );
            //    GL.Vertex2( m_fluidSim.Domain.Right, m_fluidSim.Domain.Bottom );
            //    GL.Vertex2( m_fluidSim.Domain.Right, m_fluidSim.Domain.Top );
            //    GL.Vertex2( m_fluidSim.Domain.Left, m_fluidSim.Domain.Top );
            //    GL.End( );
            //    GL.PopAttrib( );
            //}

            //GL.Color4( Color.White );
            //m_textPrinter.Begin( );

            //// FPS & Co.
            //// Show fps (Frames per second)
            //m_textPrinter.Prepare( CreateFPSText( e.Time ), m_textFont, out m_textHandleStats );
            //m_textPrinter.Draw( m_textHandleStats );

            //// Help
            //if ( m_showHelp )
            //{
            //    GL.MatrixMode( MatrixMode.Modelview );
            //    GL.PushMatrix( );
            //    GL.LoadIdentity( );
            //    GL.Translate( 0.0f, m_textFont.Height, 0.0f );
            //    m_textPrinter.Draw( m_textHandleHelp );
            //    GL.MatrixMode( MatrixMode.Modelview );
            //    GL.PopMatrix( );
            //}
            //m_textPrinter.End( );


            //// Present
            //SwapBuffers( );

            //// Check OGL errors
            //Utils.TraceGlError( );
        }
    }
}
