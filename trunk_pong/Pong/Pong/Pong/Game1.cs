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

        enum Estados { INTRO, MENU, CREDITOS, JOGO, GAMEOVER, THEEND, PAUSE };

        Estados estado_atual = Estados.JOGO;

        /// <summary>
        /// Esta imagem que vai no fundo da tela.
        /// </summary>
        Texture2D fundo;

        Bola instanciaBola1;
        //Bola instanciaBola2;

        Paleta paletadireita;
        Paleta paletaesquerda;

        Song musica;

        KeyboardState teclado_atual, teclado_anterior;

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

            musica = Content.Load<Song>("Kalimba");
            MediaPlayer.Play(musica);
            MediaPlayer.Volume = (float)0.5;

            IsMouseVisible = true;
            Window.Title = "devs173c";
            Console.WriteLine("teste");

            instanciaBola1 = new Bola(Content, Window, random, 40);
            //instanciaBola2 = new Bola(Content, Window, random, -40);

            paletadireita = new Paleta(Content, Window, 1);
            paletaesquerda = new Paleta(Content, Window, 2);

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
            teclado_atual = Keyboard.GetState();

            if (teclado_atual.IsKeyDown(Keys.M) && !teclado_anterior.IsKeyDown(Keys.M))
            {
                MediaPlayer.IsMuted = !MediaPlayer.IsMuted;
            }

            switch (estado_atual)
            {
                case Estados.INTRO:

                    break;

                case Estados.JOGO:
                    instanciaBola1.Update(paletadireita, paletaesquerda);
                    //instanciaBola2.colisao);
                    //instanciaBola2.Update(colisaoPaletaDireita, colisaoPaletaEsquerda, instanciaBola1.colisao);
                    paletadireita.Update(1);
                    paletaesquerda.Update(2);
                    break;

                case Estados.GAMEOVER:

                    break;
            }



            teclado_anterior = teclado_atual;

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

            switch (estado_atual)
            {
                case Estados.INTRO:

                    break;

                case Estados.JOGO:
                    spriteBatch.Draw(fundo, new Vector2(0, 0), Color.White);
                    paletadireita.Draw(gameTime, spriteBatch, Color.Green);
                    paletaesquerda.Draw(gameTime, spriteBatch, Color.Red);
                    instanciaBola1.Draw(gameTime, spriteBatch, instanciaBola1.animacao_atual);
                    break;

                case Estados.GAMEOVER:

                    break;
            }


            spriteBatch.End();

            base.Draw(gameTime);


        }
    }
}

