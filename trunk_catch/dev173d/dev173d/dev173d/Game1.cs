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

namespace dev173d
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        BasicPrimitive strangeObject1;
        BasicPrimitive strangeObject2;
        BasicPrimitive strangeObject3;
        BasicPrimitive strangeObject4;
        BasicPrimitive strangeObject5;
        BasicPrimitive strangeObject6;
        
        BasicCamera camera;

        KeyboardState ks, old_ks;

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

            strangeObject1 = new BasicPrimitive(GraphicsDevice, Content, Color.White);
            strangeObject2 = new BasicPrimitive(GraphicsDevice, Content, Color.Green);
            strangeObject3 = new BasicPrimitive(GraphicsDevice, Content, Color.White);
            strangeObject4 = new BasicPrimitive(GraphicsDevice, Content, Color.White);
            strangeObject5 = new BasicPrimitive(GraphicsDevice, Content, Color.White);
            strangeObject6 = new BasicPrimitive(GraphicsDevice, Content, Color.White);

            strangeObject2.World = Matrix.CreateRotationY(MathHelper.ToRadians(-90.0f)) *
                Matrix.CreateTranslation(3.0f, 0, 0);

            strangeObject1.World = Matrix.CreateTranslation(0, 0, -3.0f);
            //strangeObject2.World = Matrix.CreateTranslation(3.0f, 0, 0);
            strangeObject3.World = Matrix.CreateTranslation(-3.0f, 0, 0);
            strangeObject4.World = Matrix.CreateTranslation(0, 3.0f, 0);
            strangeObject5.World = Matrix.CreateTranslation(0, -3.0f, 0);
            strangeObject6.World = Matrix.CreateTranslation(0, 0, 3.0f);
            
            camera = new BasicCamera(GraphicsDevice);
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
            strangeObject2.color = Color.Green;

            old_ks = ks;
            ks = Keyboard.GetState();

            if(ks.IsKeyDown(Keys.W))
                camera.Target = new Vector3(0,0,-1.0f);

            if (ks.IsKeyDown(Keys.D))
                camera.Target = new Vector3(1.0f, 0, 0);

            camera.Update(gameTime);

            strangeObject1.Update(gameTime);
            strangeObject2.Update(gameTime);
            strangeObject3.Update(gameTime);
            strangeObject4.Update(gameTime);
            strangeObject5.Update(gameTime);
            strangeObject6.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            RasterizerState myRasterizerStateNormal = new RasterizerState();
            RasterizerState myRasterizerStateWired = new RasterizerState();
            RasterizerState myRasterizerStateUnCulled = new RasterizerState();

            myRasterizerStateNormal.FillMode = FillMode.Solid;//padrão
            myRasterizerStateNormal.CullMode = CullMode.CullCounterClockwiseFace;//padrão
            
            myRasterizerStateWired.FillMode = FillMode.WireFrame;
            
            myRasterizerStateUnCulled.CullMode = CullMode.None;

            //GraphicsDevice.RasterizerState = myRasterizerStateNormal;

            //? slow?
            strangeObject1.Draw(gameTime, camera);
            strangeObject2.Draw(gameTime, camera);
            strangeObject3.Draw(gameTime, camera);
            strangeObject4.Draw(gameTime, camera);
            strangeObject5.Draw(gameTime, camera);
            strangeObject6.Draw(gameTime, camera);
            
            base.Draw(gameTime);
        }
    }
}


