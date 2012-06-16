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

        Estados estado_atual = Estados.MENU;

        //para o vídeo: http://msdn.microsoft.com/en-us/library/dd254869.aspx

        Video video;

        VideoPlayer videoPlayer;

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

        Menu menu;

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
            //MediaPlayer.Play(musica);
            MediaPlayer.Volume = (float)0.5;

            video = Content.Load<Video>("LunarLander3D");
            videoPlayer = new VideoPlayer();


            IsMouseVisible = true;
            Window.Title = "devs173c";
            Console.WriteLine("teste");

            instanciaBola1 = new Bola(Content, Window, random, 40);
            //instanciaBola2 = new Bola(Content, Window, random, -40);

            paletadireita = new Paleta(Content, Window, 1);
            paletaesquerda = new Paleta(Content, Window, 2);

            menu = new Menu(Content, Window);

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

            switch (estado_atual)
            {
                case Estados.INTRO:
                    videoPlayer.Play(video);
                    if (teclado_atual.IsKeyDown(Keys.Space) && !teclado_anterior.IsKeyDown(Keys.Space))
                    {
                        estado_atual = Estados.JOGO;
                        videoPlayer.Stop();
                    }
                    break;

                case Estados.MENU:
                    menu.Update(gameTime);
                    if (teclado_atual.IsKeyDown(Keys.J) && !teclado_anterior.IsKeyDown(Keys.J))
                    {
                        estado_atual = Estados.JOGO;
                        
                    }
                    break;

                case Estados.JOGO:
                    instanciaBola1.Update(paletadireita, paletaesquerda);
                    //instanciaBola2.colisao);
                    //instanciaBola2.Update(colisaoPaletaDireita, colisaoPaletaEsquerda, instanciaBola1.colisao);
                    paletadireita.Update(1);
                    paletaesquerda.Update(2);
                    if (MediaPlayer.State != MediaState.Playing)
                    {
                        MediaPlayer.Play(musica);
                    }

                    

                    break;

                case Estados.GAMEOVER:

                    break;
            }



            teclado_anterior = teclado_atual;

            if (teclado_atual.IsKeyDown(Keys.F2) && !teclado_anterior.IsKeyDown(Keys.F2))
            {
                graphics.ToggleFullScreen();
            }

            if (teclado_atual.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            if (teclado_atual.IsKeyDown(Keys.M) && !teclado_anterior.IsKeyDown(Keys.M))
            {
                MediaPlayer.IsMuted = !MediaPlayer.IsMuted;
            }

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
                    spriteBatch.Draw(videoPlayer.GetTexture(),new Rectangle (0,0,Window.ClientBounds.Width, Window.ClientBounds.Height),Color.White);
                    break;

                case Estados.MENU:
                    menu.Draw(gameTime, spriteBatch);
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

