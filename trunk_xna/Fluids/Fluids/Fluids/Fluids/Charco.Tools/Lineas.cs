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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Charco.Tools
{
    public class Lineas : DrawableGameComponent
    {        
        const int max = 4096;

        static public Lineas Instancia;

        
        Effect EffectLines;
       
        VertexPositionColor[] Vertices = new VertexPositionColor[max];
        
        int index = 0;

        public ICamara Camara { get; set; }
        public string Name { get; private set; }

        Lineas(Game game, ICamara camara) : base(game)             
        {            
            this.Camara = camara;
            
            this.EffectLines = game.Content.Load<Effect>(@"Lineas\lines");
        }

        #region Funciones

        public void AddLine2D(Matrix World, Vector2 A, Vector2 B, Color color)
        {
            AddLine3D(World, new Vector3(A, 0), new Vector3(B, 0), color);
        }


        public void AddLine3D(List<Vector3> Posiciones, Color color)
        {
            for (int i = 1; i < Posiciones.Count; i++)
            {
                AddLine3D(Posiciones[i - 1], Posiciones[i], color);
            }
        }

        public void AddSegment3D(List<Vector3> Posiciones, Color color)
        {
            for (int i = 1; i < Posiciones.Count; i+=2)
            {
                AddLine3D(Posiciones[i - 1], Posiciones[i], color);
            }
        }


        public void AddSegment3D(Matrix Transform, List<Vector3> Posiciones, Color color)
        {
            for (int i = 1; i < Posiciones.Count; i += 2)
            {
                AddLine3D(Transform, Posiciones[i - 1], Posiciones[i], color);
            }
        }

        public void AddLine3D(List<List<Vector3>> Lineas, Color color)
        {
            for (int i = 0; i < Lineas.Count; i++)
            {
                AddLine3D(Lineas[i], color);
            }
        }
     
        public void AddLine3D(Matrix World, List<List<Vector3>> Lineas, Color color)
        {
            for (int i = 0; i < Lineas.Count; i++)
            {
                AddLine3D(World, Lineas[i], color);
            }
        }

        public void AddLine3D(Matrix world, List<Vector3> Posiciones, Color color)
        {
            for (int i = 1; i < Posiciones.Count; i++)
            {
                AddLine3D(world, Posiciones[i - 1], Posiciones[i], color);
            }
        }

        public void AddLine3D(Matrix world, Vector3 A, Vector3 B, Color color)
        {
            Vector3 At = Vector3.Transform(A, world);
            Vector3 Bt = Vector3.Transform(B, world);
            AddLine3D(At, Bt, color);
        }

        public void AddLine3D(Vector3 A, Vector3 B, Color color)
        {
            AddLine3D(A, B, color, color);
        }
        
        public void AddLine3D(Vector3 A, Vector3 B, Color color1, Color color2)
        {
            Vertices[index].Position = A;
            Vertices[index++].Color = color1;
            Vertices[index].Position = B;
            Vertices[index++].Color = color2;

            if (index >= max)
            {
                Flush();
            }
        }

        public void AddCylinder(Matrix world, float Radio, float Altura, int Segmentos, Color color)
        {
            Vector3 A, B, C, D;
            
            A = B = C = D = Vector3.Zero;

            for (int i = 0; i < Segmentos; i++)
            {
                int j = (i + 1) % Segmentos;
                A = C = Radio * new Vector3((float)-Math.Cos(i * 2 * Math.PI / Segmentos), 0, (float)Math.Sin(i * 2 * Math.PI / Segmentos));
                B = D = Radio * new Vector3((float)-Math.Cos(j * 2 * Math.PI / Segmentos), 0, (float)Math.Sin(j * 2 * Math.PI / Segmentos));

                D.Y = C.Y = Altura;

                AddLine3D(world, A, B, color);
                AddLine3D(world, C, D, color);
                AddLine3D(world, A, C, color);
            }
            AddLine3D(world, B, D, color);
        }

        public void AddTriangle(Matrix world, Vector2 origin, float Radio, float Angle, Color color)
        {
            Matrix rot;
            Matrix trans;
            Matrix.CreateTranslation( -origin.X, -origin.Y, 0, out trans );
            Matrix.CreateRotationZ( Angle + MathHelper.PiOver2, out rot );
            Matrix.Multiply( ref trans, ref rot, out rot );
            Matrix.CreateTranslation( origin.X, origin.Y, 0, out trans );
            Matrix.Multiply( ref rot, ref trans, out rot );
            Matrix.Multiply( ref rot, ref world, out world );
            
            float Cos = (float) Math.Cos(2*MathHelper.Pi/3);
            float Sin = (float) Math.Sin(2*MathHelper.Pi/3);

            Vector2 A = origin - Vector2.UnitY * Radio;
            Vector2 B = origin +  new Vector2( -Cos, Sin ) * Radio;
            Vector2 C = origin +  new Vector2(  Cos, Sin ) * Radio;

            AddLine2D( world, A, B, color );
            AddLine2D( world, B, C, color );
            AddLine2D( world, C, A, color );
        }

        //public void AddArrow( Matrix matrix, Vector2 pos,  float Angle, Color color )
        //{
        //    AddLine2D( matrix, pos, pos + Director, color );
        //    float angle = (float) Math.Atan2( Director.Y, Director.X ) + 2*MathHelper.PiOver2;
        //    AddTriangle(  Matrix.CreateRotationZ(angle) * matrix , pos + Director, Director.Length( ) * 0.5f, color );
        //}

        public void AddSphere(Matrix world, Vector3 pivote, int Segmentos, float Radio, Color color)
        {
            Matrix rotY, rotZ;

            world *= Matrix.CreateTranslation(pivote);

            //world = Matrix.CreateBillboard(pivote, Camara.Position, Camara.Up, Camara.Front) * world;


            for (int k = 0; k < Segmentos; k++)
            {
                rotY = Matrix.CreateScale(Radio) * Matrix.CreateRotationY(MathHelper.ToRadians(k * 360.0f / Segmentos)) * world;
                rotZ = Matrix.CreateScale(Radio) * Matrix.CreateRotationX(MathHelper.ToRadians(k * 360.0f / Segmentos)) * world;

                for (int i = 0; i < Segmentos; i++)
                {
                    int j = (i + 1) % Segmentos;
                    Vector3 A = new Vector3((float)-Math.Cos(i * 2 * Math.PI / Segmentos), (float)Math.Sin(i * 2 * Math.PI / Segmentos), 0);
                    Vector3 B = new Vector3((float)-Math.Cos(j * 2 * Math.PI / Segmentos), (float)Math.Sin(j * 2 * Math.PI / Segmentos), 0);
                    AddLine3D(rotY, A, B, color);
                    AddLine3D(rotZ, A, B, color);
                }

            }
        }

        public void AddSphere(Matrix matrix, BoundingSphere X, int Segementos, Color color)
        {
            AddSphere(matrix, X.Center, Segementos, X.Radius, color);
        }

       

        public void AddRectangle(Matrix world, Rectangle selecccion)
        {
            Vector3 A = new Vector3(selecccion.X, selecccion.Y, 0);
            Vector3 B = new Vector3(selecccion.X + selecccion.Width, selecccion.Y, 0);
            Vector3 C = new Vector3(selecccion.X + selecccion.Width, selecccion.Y + selecccion.Height, 0);
            Vector3 D = new Vector3(selecccion.X, selecccion.Y + selecccion.Height, 0);

            AddLine3D(world, A, B, Color.White);
            AddLine3D(world, B, C, Color.White);
            AddLine3D(world, C, D, Color.White);
            AddLine3D(world, D, A, Color.White);
        }


        public void AddRectangle( Matrix world, Vector2 Center, float Size, Color color )
        {
            Size *= 0.5f;
            Vector3 A = new Vector3( Center.X - Size, Center.Y - Size, 0 );
            Vector3 B = new Vector3( Center.X + Size, Center.Y - Size, 0 );
            Vector3 C = new Vector3( Center.X + Size, Center.Y + Size, 0 );
            Vector3 D = new Vector3( Center.X - Size, Center.Y + Size, 0 );
            AddLine3D( world, A, B, color );
            AddLine3D( world, B, C, color );
            AddLine3D( world, C, D, color );
            AddLine3D( world, D, A, color );
        }

        public void AddRectangle(Matrix world, Vector2 Min, Vector2 Max, Color color) {
            Vector3 A = new Vector3(Min.X, Min.Y, 0);
            Vector3 B = new Vector3(Max.X, Min.Y, 0);
            Vector3 C = new Vector3(Max.X, Max.Y, 0);
            Vector3 D = new Vector3(Min.X, Max.Y, 0);

            AddLine3D(world, A, B, color);
            AddLine3D(world, B, C, color);
            AddLine3D(world, C, D, color);
            AddLine3D(world, D, A, color);
        
        }

        
        public void AddOrigen2D(Matrix Transformacion, Vector2 centro, float escala)
        {
            Matrix world = Transformacion * Matrix.CreateTranslation( centro.X, centro.Y, 0 );

            AddLine3D( world, Vector3.Zero, Vector3.UnitX * escala, Color.Red );
            AddLine3D( world, Vector3.Zero, Vector3.UnitY * escala, Color.Green );
            
        }

        public void AddOrigen(Matrix Transformacion, Vector3 pivote, float escala)
        {
            Matrix world = Transformacion * Matrix.CreateTranslation(pivote);

            AddLine3D(world, Vector3.Zero, Vector3.UnitX * escala, Color.Red);
            AddLine3D(world, Vector3.Zero, Vector3.UnitY * escala, Color.Green);
            AddLine3D(world, Vector3.Zero, -Vector3.UnitZ * escala, Color.Blue);
        }

        public void AddBox(Matrix world, Vector3 A, Vector3 B, Color color)
        {
            AddBox(world, new BoundingBox(A, B), color);
        }

        public void AddBox(Matrix world, BoundingBox box, Color color)
        {
            Vector3[] v = box.GetCorners();
            AddBox(world, v, color);
        }

        public void AddBox(Matrix world, Vector3[] v, Color color)
        {


            AddLine3D(world, v[0], v[1], color);
            AddLine3D(world, v[1], v[2], color);
            AddLine3D(world, v[2], v[3], color);
            AddLine3D(world, v[3], v[0], color);

            AddLine3D(world, v[4], v[5], color);
            AddLine3D(world, v[5], v[6], color);
            AddLine3D(world, v[6], v[7], color);
            AddLine3D(world, v[7], v[4], color);

            AddLine3D(world, v[0], v[4], color);
            AddLine3D(world, v[1], v[5], color);
            AddLine3D(world, v[2], v[6], color);
            AddLine3D(world, v[3], v[7], color);


            //AddLine3D(world, v[0], v[1], Color.Lerp(Color.Blue, color, 0.3f));
            //AddLine3D(world, v[1], v[2], Color.Lerp(Color.Blue, color, 0.3f));
            //AddLine3D(world, v[2], v[3], Color.Lerp(Color.Blue, color, 0.3f));
            //AddLine3D(world, v[3], v[0], Color.Lerp(Color.Blue, color, 0.3f));

            //AddLine3D(world, v[4], v[5], Color.Lerp(Color.Green, color, 0.3f));
            //AddLine3D(world, v[5], v[6], Color.Lerp(Color.Green, color, 0.3f));
            //AddLine3D(world, v[6], v[7], Color.Lerp(Color.Green, color, 0.3f));
            //AddLine3D(world, v[7], v[4], Color.Lerp(Color.Green, color, 0.3f));

            //AddLine3D(world, v[0], v[4], Color.Lerp(Color.Orange, color, 0.3f));
            //AddLine3D(world, v[1], v[5], Color.Lerp(Color.Orange, color, 0.3f));
            //AddLine3D(world, v[2], v[6], Color.Lerp(Color.Orange, color, 0.3f));
            //AddLine3D(world, v[3], v[7], Color.Lerp(Color.Orange, color, 0.3f));

        }

        public void AddFrustum(Matrix viewProjection)
        {
            Matrix world = Matrix.Invert(viewProjection);

            AddLine3D(world, new Vector3(-1, -1, 0), new Vector3(-1, -1, 1), Color.Green);
            AddLine3D(world, new Vector3(-1, 1, 0), new Vector3(-1, 1, 1), Color.Green);
            AddLine3D(world, new Vector3(1, -1, 0), new Vector3(1, -1, 1), Color.Green);
            AddLine3D(world, new Vector3(1, 1, 0), new Vector3(1, 1, 1), Color.Green);

            AddLine3D(world, new Vector3(-1, -1, 0), new Vector3(-1, 1, 0), Color.Blue);
            AddLine3D(world, new Vector3(-1, -1, 0), new Vector3(1, -1, 0), Color.Blue);
            AddLine3D(world, new Vector3(-1, 1, 0), new Vector3(1, 1, 0), Color.Blue);
            AddLine3D(world, new Vector3(1, -1, 0), new Vector3(1, 1, 0), Color.Blue);

            AddLine3D(world, new Vector3(-1, -1, 1), new Vector3(1, -1, 1), Color.Red);
            AddLine3D(world, new Vector3(-1, 1, 1), new Vector3(1, 1, 1), Color.Red);
            AddLine3D(world, new Vector3(-1, -1, 1), new Vector3(-1, 1, 1), Color.Red);
            AddLine3D(world, new Vector3(1, -1, 1), new Vector3(1, 1, 1), Color.Red);

        }

        #endregion

        public void Flush()
        {
            if (!Visible) index = 0;
            if (index == 0) return;

            Game.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            EffectLines.CurrentTechnique = EffectLines.Techniques["Lines"];

            EffectLines.Parameters["xViewProjection"].SetValue(Camara.ViewProjection);
            EffectLines.Parameters["xWorld"].SetValue(Matrix.Identity);

            foreach (EffectPass pass in EffectLines.CurrentTechnique.Passes)
            {
                pass.Apply();
                Game.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>
                    (PrimitiveType.LineList, Vertices, 0, index / 2);
            }
            index = 0;
        }

        public override void Draw(GameTime gameTime)
        {
            Flush();
        }



        public static void Initialize(Game game, ICamara camara)
        {
            if (Instancia == null)
            {
                Instancia = new Lineas(game, camara);
                Instancia.DrawOrder = int.MaxValue/2;
                game.Components.Add(Instancia);
                Instancia.Initialize();
            }
        }

        public static void Initialize2D(Game game)
        {
            Camara camara = new Camara();
            Vector2 center = game.GraphicsDevice.Viewport.Bounds.Center.ToVector2();
            camara.View = Matrix.CreateLookAt(new Vector3(center, 0), new Vector3(center, 1), new Vector3(0, -1, 0));
            camara.Projection = Matrix.CreateOrthographic(center.X * 2, center.Y * 2, -0.5f, 1);

            Initialize(game, camara);
        }






    } 
}
