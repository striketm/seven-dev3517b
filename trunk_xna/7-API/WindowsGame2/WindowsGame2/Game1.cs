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

namespace WindowsGame2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // The images we will draw
        Texture2D aviao1;
        Texture2D aviao2;

        Vector2 aviao1vetor = new Vector2(200, 0);
        Vector2 aviao2vetor = new Vector2(50,0);



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


            aviao1 = Content.Load<Texture2D>("FA-18H");
            aviao2 = Content.Load<Texture2D>("FAX");

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
           
            // Get input
            KeyboardState keyboard = Keyboard.GetState();



            // Move the player left and right with arrow keys or d-pad
            if (keyboard.IsKeyDown(Keys.Left))
            {
                aviao1vetor.X -=1;
            }
            if (keyboard.IsKeyDown(Keys.Right) )
            {
                aviao1vetor.X += 1;
            }


            if (keyboard.IsKeyDown(Keys.Space))
            {
                aviao2vetor.X -= 1;
            }
            if (keyboard.IsKeyDown(Keys.LeftAlt))
            {
                aviao2vetor.X += 1;
            }


if( DetectaColisao(aviao1,aviao2))
Exit();

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
            spriteBatch.Draw(aviao1,aviao1vetor, Color.White);
            spriteBatch.Draw(aviao2, aviao2vetor, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }



        public bool DetectaColisao(Texture2D sprite1, Texture2D sprite2)
        {

            //Rectangle rectangleA, Color[] dataA,
                                     //Rectangle rectangleB, Color[] dataB)

            // The color data for the images; used for per pixel collision
            Color[] dataA;
            Color[] dataB;

            // Load textures
            //blockTexture = Content.Load<Texture2D>("Block");
            //personTexture = Content.Load<Texture2D>("Person");

            //// Extract collision data
            //blockTextureData =
            //    new Color[blockTexture.Width * blockTexture.Height];

            dataA = new Color[sprite1.Width * sprite1.Height];
            dataB = new Color[sprite2.Width * sprite2.Height];
            sprite1.GetData(dataA);
            sprite2.GetData(dataB);


            Rectangle rectangleA =
                        new Rectangle((int)aviao1vetor.X, (int)aviao1vetor.X,
                        sprite1.Width, sprite1.Height);

            Rectangle rectangleB =
                     new Rectangle((int)aviao2vetor.X, (int)aviao2vetor.X,
                     sprite2.Width, sprite2.Height);
        

            int top = Math.Max(rectangleA.Top, rectangleB.Top);
            int bottom = Math.Min(rectangleA.Bottom, rectangleB.Bottom);
            int left = Math.Max(rectangleA.Left, rectangleB.Left);
            int right = Math.Min(rectangleA.Right, rectangleB.Right);

            // Check every point within the intersection bounds
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    // Get the color of both pixels at this point
                    Color colorA = dataA[(x - rectangleA.Left) +
                                         (y - rectangleA.Top) * rectangleA.Width];
                    Color colorB = dataB[(x - rectangleB.Left) +
                                         (y - rectangleB.Top) * rectangleB.Width];

                    // If both pixels are not completely transparent,
                    if (colorA.A != 0 && colorB.A != 0)
                    {
                        // then an intersection has been found
                        return true;
                    }
                }
            }

            // No intersection found
            return false;
        }


    }



}
