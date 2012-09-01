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


namespace Pacman
{
    public class Game1 : Microsoft.Xna.Framework.Game 
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GraphicsDevice device;
        private KeyboardState keyboardState;

        //Here we create the Map object from the class we made.
        //It is called what you named the class to.
        Map map;

        //We make a new player
        Player player;

        SpriteFont font;


        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1120;
            graphics.PreferredBackBufferHeight = 704;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "Pacman";
            Content.RootDirectory = "Content";

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            //We new the map object. And since the constructor we made in
            //the class (the function that is executed at the objects creation)
            //wants a Game1. We simply give it one, and since we are already in Game1
            //we only need to write "this".
            map = new Map(this);

            //We new the player. Sending in a game1(this) and a Vector2 position
            player = new Player(this,new Vector2(150,150));
            //Then we call initialize on the player so it can set up it's basic members
            player.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);



            //Calls the objects load function to load all it's graphics
            map.Load();



            font = Content.Load<SpriteFont>("fonts/myFont");

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();


            //ESC KEY EXIT
            keyboardState = Keyboard.GetState();

            //We call update on our player
            player.Update(gameTime);

            // If the ESC key is pressed, skip the rest of the update.
            if (exitKeyPressed() == false)
            {
                base.Update(gameTime);
            }
        }
        bool exitKeyPressed()
        {
            // Check to see whether ESC was pressed on the keyboard or BACK was pressed on the controller.
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                Exit();
                return true;
            }
            return false;
        }
        //END ESC KEY EXIT


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            DrawText();



            //Calls the objects draw function wich draws the map. And gives it a gameTime 
            // since the function wants one.
            map.Draw(gameTime);


            //We draw our player
            player.Draw(gameTime);

            spriteBatch.End();


            base.Draw(gameTime);
        }

 private void DrawText()
 {

     spriteBatch.DrawString(font, "Score:", new Vector2(915, 45), Color.White);
     spriteBatch.DrawString(font, "Lives:", new Vector2(915, 200), Color.White);
     spriteBatch.DrawString(font, "Level:", new Vector2(915, 355), Color.White);
     spriteBatch.DrawString(font, "Programmed by:", new Vector2(865, 510), Color.White);
     spriteBatch.DrawString(font, "Casundrum", new Vector2(885, 550), Color.White);
 }
        }
    }

