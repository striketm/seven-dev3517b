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

        //AULA 7/5
        KeyboardState teclado_atual, teclado_anterior;//renamed
        MouseState mouse_atual, mouse_anterior;
        GamePadState joystick_atual, joystick_anterior;        

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


            // TODO: use this.Content to load your game content here
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            teclado_atual = Keyboard.GetState();
            mouse_atual = Mouse.GetState();//AULA 7/5
            joystick_atual = GamePad.GetState(PlayerIndex.One);

            moto1.Update(gameTime, teclado_atual, mouse_atual, joystick_atual);

            
            //if (mouse_atual.LeftButton == ButtonState.Pressed)
            //{
            //    posicao_moto.X = mouse_atual.X;
            //    posicao_moto.Y = mouse_atual.Y;
            //}

            ////if(joystick_atual.Buttons.A), B, X, Y, Left/Right Stick, Left/Right Shoulder, Start, 
            ////if(joystick_atual.DPad.Down, Right, Left, Up
            //if (joystick_atual.ThumbSticks.Right.X == 1)
            //{
            //    posicao_moto = Vector2.Zero;
            //}

            //if(teclado_atual.IsKeyDown(Keys.Right))
            //{
            //    posicao_moto.X += 5;
            //}
            //if (teclado_atual.IsKeyDown(Keys.Left))
            //{
            //    posicao_moto.X -= 5;
            //}
            //if (teclado_atual.IsKeyDown(Keys.Up))
            //{
            //    posicao_moto.Y -= 5;
            //}
            //if (teclado_atual.IsKeyDown(Keys.Down))
            //{
            //    posicao_moto.Y += 5;
            //}

            //if (posicao_moto.X < 0)
            //{
            //    posicao_moto.X = 0;
            //}

            //if (posicao_moto.X > Window.ClientBounds.Width - textura_moto.Width)
            //{
            //    posicao_moto.X = Window.ClientBounds.Width - textura_moto.Width;
            //}

            //if (posicao_moto.Y < 0)
            //{
            //    posicao_moto.Y = 0;
            //}

            //if (posicao_moto.Y > Window.ClientBounds.Height - textura_moto.Height)
            //{
            //    posicao_moto.Y = Window.ClientBounds.Height - textura_moto.Height;
            //}


            /*
             * Exercício para agora:
             * 
             * Faça sua moto/imagem andar para os 4 lados
             * 
             * Faça com que ela não ultrapasse os cantos da tela
             * (hard ;-)
             * 
             * Texture2D Width Height
             * Window ClientBounds Width Height
             * 
             */

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
            spriteBatch.End();

           base.Draw(gameTime);
        }

        #endregion

    }
}
