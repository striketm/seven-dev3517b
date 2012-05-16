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

namespace MotoGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region Atributos

        /// <summary>
        /// O acesso ao dispositivo de video
        /// </summary>
        GraphicsDeviceManager graphics;
        
        /// <summary>
        /// "Desenhador"
        /// </summary>
        SpriteBatch spriteBatch;

        //Texture2D textura_moto;

        //Vector2 posicao_moto;//renamed

        Moto moto1;
        Sprite teste;

        //AULA 7/5
        KeyboardState teclado_atual, teclado_anterior;//renamed
        MouseState mouse_atual, mouse_anterior;
        GamePadState joystick_atual, joystick_anterior;

        Song musica;

        SpriteFont arial;

        //SoundEffect efeitoSonoro;//no objeto

        #endregion

        #region Métodos

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //aula 2407
            IsMouseVisible = true;
            Window.Title = "dev2417";
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;//padrao

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //textura_moto = Content.Load<Texture2D>("moto");

            //posicao_moto = new Vector2(200, 300);

            moto1 = new Moto(Content, Window);
            
            musica = Content.Load<Song>("Sounds/Musics/music");

            MediaPlayer.Play(musica);

            arial = Content.Load<SpriteFont>("arial");

            //MediaPlayer.State == MediaState.

            //bool TelaCheia = false;

            //if(TelaCheia = false)
            //    if (/*apertei o botao certo*/true)
            //    {
            //        TelaCheia = !TelaCheia;
            //    }

            
            
            /*
             * Exercício: crie comandos de teclado para 
             * aumentar e reduzir o volume da música (+,-),  
             * dar pause e play (mesmo botão p, recomeça de onde parou)
             * e stop (recomeça do início)
             * e faça o mute no botão m
             */

            //Microsoft Visual Studio Express 2010 Download
            //Microsoft XNA 4.0 Download
            //Tortoise SVN Download
             
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime">O tempo do jogo</param>
        protected override void Update(GameTime gameTime)
        {
            teclado_atual = Keyboard.GetState();
            mouse_atual = Mouse.GetState();
            joystick_atual = GamePad.GetState(PlayerIndex.One);
            
            if (teclado_atual.IsKeyDown(Keys.Escape))
                this.Exit();

            if (teclado_atual.IsKeyDown(Keys.F11))
                graphics.ToggleFullScreen();

            if (teclado_atual.IsKeyDown(Keys.M) && !teclado_anterior.IsKeyDown(Keys.M))
                MediaPlayer.IsMuted = !MediaPlayer.IsMuted;

            if (teclado_atual.IsKeyDown(Keys.Subtract) && !teclado_anterior.IsKeyDown(Keys.Subtract))
                MediaPlayer.Volume -= 0.2f;

            if (teclado_atual.IsKeyDown(Keys.Add) && !teclado_anterior.IsKeyDown(Keys.Add))
                MediaPlayer.Volume += 0.2f;

            moto1.Update(gameTime, teclado_atual, mouse_atual, joystick_atual);
                
            teclado_anterior = teclado_atual;
            mouse_anterior = mouse_atual;
            joystick_anterior = joystick_atual;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Blue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            moto1.Draw(gameTime, spriteBatch);
            //spriteBatch.Draw(textura_moto, posicao_moto, Color.White);
            
            int pontos = 0;
            spriteBatch.DrawString(arial, "Pontos: " + pontos, new Vector2(10, 10), Color.Red);
 
            spriteBatch.End();

           base.Draw(gameTime);
        }

        #endregion

    }
}
