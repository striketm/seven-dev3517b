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

namespace RPG_The_Game
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Song musica;

        Objetos.Rato rato;
        Objetos.Cachorro trigo;

        KeyboardState teclado_atual, teclado_anterior;
        MouseState mouse_atual, mouse_anterior;
        GamePadState joystick_atual, joystick_anterior;
       
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

            musica = Content.Load<Song>("Sounds/Musics/Kalimba");

            //MediaPlayer.Play(musica);

            rato = new Objetos.Rato(Content.Load<Texture2D>("circulo"), Window, Content.Load<SoundEffect>("Sounds/Effects/ding"));

            //rato.camada = 0.1f;

            //trigo = new Objetos.Cachorro(Content.Load<Texture2D>("circuloTrigonometrico"));
            //Objetos.Cachorro.listaCachorros.Add(trigo);

            //trigo.camada = 0.9f;

            IsMouseVisible = true;
            Window.Title = "R-P-G The Game (Ratón, Perro, Gatón)";
            //graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 600;
            //graphics.ToggleFullScreen();
            graphics.ApplyChanges();
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
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (teclado_atual.IsKeyDown(Keys.Escape))
                this.Exit();

            //Window.Title = gameTime.ElapsedGameTime..ToString();

            teclado_atual = Keyboard.GetState();
            mouse_atual = Mouse.GetState();
            joystick_atual = GamePad.GetState(PlayerIndex.One);

            //para casa: fazer a música diminuir e aumentar o volume - botão menos e mais...
            //dar mute (volta volume de onde estava) - mesmo botão M
            //dar pause (recomeçar de onde parou)  - mesmo botão P
            //e stop (recomeçar do início) - mesmo botão S

            rato.Update(gameTime, teclado_atual, teclado_anterior);

            //if (mouse_atual.LeftButton == ButtonState.Pressed)
            //{
            //    rato.diferenca = new Vector2(rato.PosMouse.X - rato.Posicao.X, rato.PosMouse.Y - rato.Posicao.Y);
            //    rato.Posicao = new Vector2(mouse_atual.X, mouse_atual.Y);
            //}

            //if(joystick_atual.Buttons.A), B, X, Y, Left/Right Stick, Left/Right Shoulder, Start, 
            //if(joystick_atual.DPad.Down, Right, Left, Up
            if (joystick_atual.ThumbSticks.Right.X == 1)
            {
                rato.Posicao = Vector2.Zero;
            }

            if (teclado_atual.IsKeyDown(Keys.Z))
            {
                Objetos.Cachorro.listaCachorros.Add(new Objetos.Cachorro(Content.Load<Texture2D>("pentagono")));
            }

            teclado_anterior = teclado_atual;
            mouse_anterior = mouse_atual;
            joystick_anterior = joystick_atual;

            //Window.Title = mouse_atual.X.ToString();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.BackToFront, null);
            
            for (int i = 0; i < Objetos.Cachorro.listaCachorros.Count; i++)
            {
                //Objetos.Cachorro.listaCachorros[i].Draw(gameTime, spriteBatch);
            }
            rato.Draw(gameTime, spriteBatch, rato.andando);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
