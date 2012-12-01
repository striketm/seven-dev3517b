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

namespace SevenInvaders
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        /// <summary>
        /// Placa de vídeo
        /// </summary>
        GraphicsDeviceManager graphics;

        /// <summary>
        /// "Desenhador" de objetos 2d 
        /// </summary>
        SpriteBatch spriteBatch;
        
        /// <summary>
        /// Esta variável vai armazenar o estad atual do teclado
        /// </summary>
        KeyboardState teclado;
        
        Player ship1;
        Player ship2;
        
        Sprite sprite1;
        
        //EXERCÍCIO 2:
        //criar um segundo objeto do tipo Ship
        //também controlado pelo teclado
        //mas com botões *diferentes* para cada jogador

        /// <summary>
        /// Construtor, chamado sempre que um objeto da classe é criado
        /// </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "magic";
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
        /// Carregar o conteúdo do jogo
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ship1 = new Player(Content.Load<Texture2D>("sevenmv"), new Vector2(100, 0), 1, Window);
            ship2 = new Player(Content.Load<Texture2D>("seven"), new Vector2(200, 0), 2, Window);
            
            ship1.Color = Color.White;
            ship2.Color = Color.White;
            
            ship2.Rotation = MathHelper.ToRadians(45.0f);

            ship1.Frame = new Rectangle(0, 0, 95, 123);
            ship2.Frame = new Rectangle(0, 0, 88, 123);

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            IsMouseVisible = true;
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

            teclado = Keyboard.GetState();

            if (teclado.IsKeyDown(Keys.Escape)) 
            {
                this.Exit();
            }

            ship1.Update(teclado);
            ship2.Update(teclado);
            
            //Exercício: 
            //fazer com que a nave se mova da esquerda e para a direita
            //segundo os botões do teclado

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

            ship1.Draw(spriteBatch);
            ship2.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

/*EXERCÍCIO PARA A SEMANA QUE VEM
(onde chegaremos às 10:00?)
 * 
 * No nosso jogo de "Space Invaders", teremos a classe base Sprite, herdando dela teremos um jogador na tela (criar a classe Player), e três tipos de inimigos (EnemyType1, EnemyType2, EnemyType3) que herdam de uma classe geral abstrata Enemy, que herda de Sprite.
 * Preencher os Sumarios.
 */


