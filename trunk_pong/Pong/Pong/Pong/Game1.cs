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

namespace Pong
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont Arial;
        
        Random random = new Random();

        /// <summary>
        /// Esta imagem que vai no fundo da tela.
        /// </summary>
        Texture2D fundo;
    
             

        Bola instanciaBola1;
        //Bola instanciaBola2;

        Paleta paletadireita;
        Paleta paletaesquerda;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            Arial = Content.Load<SpriteFont>("Arial");

            fundo = Content.Load<Texture2D>("fundo");
            
            

            IsMouseVisible = true;
            Window.Title = "devs173c";
            Console.WriteLine("teste");

            instanciaBola1 = new Bola(Content, Window, random, 40);
            //instanciaBola2 = new Bola(Content, Window, random, -40);

            paletadireita = new Paleta(Content,Window,true);
            paletaesquerda = new Paleta(Content,Window,false);
            
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
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            

           

            instanciaBola1.Update(paletadireita.getColisao(), paletaesquerda.getColisao());
            //, instanciaBola2.colisao);
            //instanciaBola2.Update(colisaoPaletaDireita, colisaoPaletaEsquerda, instanciaBola1.colisao);
            paletadireita.Update(true);
            paletaesquerda.Update(false);
            base.Update(gameTime);
           
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            spriteBatch.Draw(fundo, new Vector2(0, 0), Color.White);
            paletadireita.Draw(gameTime, spriteBatch, Color.Green);
            paletaesquerda.Draw(gameTime, spriteBatch, Color.Red);
            instanciaBola1.Draw(gameTime, spriteBatch, instanciaBola1.animacao_atual);
            spriteBatch.DrawString(Arial, "Pontos: ", Vector2.Zero, Color.Aqua);
            //instanciaBola2.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);


        }
    }
}

