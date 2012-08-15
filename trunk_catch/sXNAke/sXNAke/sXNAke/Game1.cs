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
using sXNAke.GameStates;

namespace sXNAke
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public enum States { INTRO, MENU, LEVEL_01, PAUSE, GAME_OVER, THE_END, CREDITS, }

        public static States present_State = States.INTRO;

        Intro intro;

        Menu menu;

        Level_01 level_01;

        KeyboardState keyboardState,oldKeyboardState;

         //if(joystick_atual.Buttons.A), B, X, Y, Left/Right Stick, Left/Right Shoulder, Start, 
            //if(joystick_atual.DPad.Down, Right, Left, Up
         
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            Window.Title = "sXNAke";
            IsMouseVisible = true;
            graphics.ApplyChanges();
            
            Console.WriteLine("Hello world!");
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

            level_01 = new Level_01(Content, Window);


            // Que tal fazer -- singleton -- ?

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

            //Atualiazação do oldKeyboardState é feita antes do keyboardState para referencia
            oldKeyboardState = keyboardState;

            keyboardState = Keyboard.GetState();

            


            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            switch (present_State)
            {
                case States.INTRO:
                    intro.Update(gameTime, keyboardState);
                    break;

                case States.MENU:
                    menu.Update(gameTime, keyboardState);
                    break;

                case States.LEVEL_01:
                    level_01.Update(gameTime, keyboardState);
                    break;

                case States.PAUSE:
                    break;

                case States.GAME_OVER:
                    break;

                case States.THE_END:
                    break;

                case States.CREDITS:
                    break;

            }



            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            switch (present_State)
            {
                case States.INTRO:
                    intro.Draw(gameTime, spriteBatch);
                    break;

                case States.MENU:
                    menu.Draw(gameTime, spriteBatch);
                    break;

                case States.LEVEL_01:
                    level_01.Draw(gameTime, spriteBatch);
                    break;

                case States.PAUSE:
                    break;

                case States.GAME_OVER:
                    break;

                case States.THE_END:
                    break;

                case States.CREDITS:
                    break;

            }


            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
