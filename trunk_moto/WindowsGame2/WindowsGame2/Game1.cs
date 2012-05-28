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
using MotoGame.Estados.Intro;
using MotoGame.Estados.Jogo;

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
        
        /// <summary>
        /// A leitura do teclado é feita de forma estática (única) no Game1 e distribuída pelo resto do código
        /// Não esquecer de testar o frame atual com o frame anterior
        /// </summary>
        public static KeyboardState teclado_atual, teclado_anterior;

        /// <summary>
        /// A leitura do mouse é feita de forma estática (única) no Game1 e distribuída pelo resto do código
        /// Não esquecer de testar o frame atual com o frame anterior
        /// </summary>
        public static MouseState mouse_atual, mouse_anterior;
        
        /// <summary>
        /// A leitura do gamepad é feita de forma estática (única) no Game1 e distribuída pelo resto do código
        /// Não esquecer de testar o frame atual com o frame anterior
        /// </summary>
        public static GamePadState gamepad_atual, gamepad_anterior;
                
        /// <summary>
        /// Os estados do jogo refletem uma divisao de como o código está organizado, 
        /// bem como a lógica do jogo em si
        /// </summary>
        public  enum Estado { INTRO, MENU, CREDITO, FASE1, FASE2, PAUSE, GAME_OVER, THE_END, SAIR};

        /// <summary>
        /// Atributo (variável) única que controla em que estado o jogo está
        /// </summary>
        public static Estado estado_atual = Estado.INTRO;

        /// <summary>
        /// Representa a introdução do jogo, pode ser um vídeo, etc, que explica e contextualiza o jogador
        /// </summary>
        Intro intro;

        /// <summary>
        /// Representa a tela com as opções do menu principal do jogo
        /// </summary>
        Menu menu;

        /// <summary>
        /// Representa a tela de créditos com o nome dos desenvoledores. Não esquecer do professor
        /// </summary>
        Credito credito;

        /// <summary>
        /// Toda a fase 1
        /// </summary>
        Fase1 fase1;

        /// <summary>
        /// Toda a fase 2
        /// </summary>
        Fase2 fase2;

        /// <summary>
        /// Tela que aparece quando o jogador perde
        /// </summary>
        GameOver gameOver;

        /// <summary>
        /// Tela que aparece quando o jogador termina o jogo vencendo
        /// </summary>
        TheEnd theEnd;

        #endregion

        #region Métodos

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

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
            
            intro = new Intro(Content, Window);

            menu = new Menu(Content, Window);

            credito = new Credito(Content, Window);

            fase1 = new Fase1(Content, Window);

            fase2 = new Fase2(Content, Window);

            gameOver = new GameOver(Content, Window);

            theEnd = new TheEnd(Content, Window);            
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
            gamepad_atual = GamePad.GetState(PlayerIndex.One);

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
                    
            switch (estado_atual)
            {
                case Estado.INTRO:
                    intro.Update(gameTime);
                    break;
                case Estado.MENU:
                    menu.Update(gameTime);
                    break;
                case Estado.CREDITO:
                    credito.Update(gameTime);
                    break;
                case Estado.FASE1:
                    fase1.Update(gameTime);
                    break;
                case Estado.FASE2:
                    fase2.Update(gameTime);
                    break;
                case Estado.THE_END:
                    theEnd.Update(gameTime);
                    break;
                case Estado.GAME_OVER:
                    gameOver.Update(gameTime);
                    break;
                case Estado.SAIR:
                    this.Exit();
                    break;
            }
                            
            teclado_anterior = teclado_atual;
            mouse_anterior = mouse_atual;
            gamepad_anterior = gamepad_atual;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Blue);
            
            spriteBatch.Begin();

            switch (estado_atual)
            {
                case Estado.INTRO:
                    intro.Draw(gameTime, spriteBatch);
                    break;
                case Estado.MENU:
                    menu.Draw(gameTime, spriteBatch);
                    break;
                case Estado.CREDITO:
                    credito.Draw(gameTime, spriteBatch);
                    break;
                case Estado.FASE1:
                    fase1.Draw(gameTime, spriteBatch);
                    break;
                case Estado.FASE2:
                    fase2.Draw(gameTime, spriteBatch);
                    break;
                case Estado.THE_END:
                    theEnd.Draw(gameTime, spriteBatch);
                    break;
                case Estado.GAME_OVER:
                    gameOver.Draw(gameTime, spriteBatch);
                    break;
                case Estado.SAIR:
                    
                    break;
            }

            spriteBatch.End();

           base.Draw(gameTime);
        }

        #endregion

    }
}
