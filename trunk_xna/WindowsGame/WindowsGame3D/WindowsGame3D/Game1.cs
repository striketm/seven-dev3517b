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

namespace WindowsGame3D
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Camera camera;

        //VertexBuffer vertexBuffer;

        BasicEffect effect;

        VertexPositionColor[] lineList;

        Matrix worldRotation = Matrix.Identity;
        Matrix worldTranslation = Matrix.Identity;
        Matrix worldScale = Matrix.Identity;

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

            effect = new BasicEffect(GraphicsDevice);

            camera = new Camera(this, new Vector3(0, 25, 10), Vector3.Zero, Vector3.Up);
            Components.Add(camera);


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

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Grid(8, 8, 0);

            //vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor),
            //    lineList.Length, BufferUsage.None);

            //vertexBuffer.SetData(lineList);

            //GraphicsDevice.SetVertexBuffer(vertexBuffer);

            effect.World = worldScale * worldRotation * worldTranslation;
            effect.View = camera.View;
            effect.Projection = camera.Projection;
            effect.VertexColorEnabled = true;

            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList,
                    lineList, 0, 256);
            }

            base.Draw(gameTime);
        }
        
        public void Grid(int rows, int columns, float height)
        {
            this.lineList = new VertexPositionColor[rows * columns * 8];
            int count = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    this.lineList[count + 0].Position = new Vector3(0.0f + j, height, 0.0f + i);
                    this.lineList[count + 1].Position = new Vector3(0.0f + j, height, 1.0f + i);

                    this.lineList[count + 2].Position = new Vector3(0.0f + j, height, 1.0f + i);
                    this.lineList[count + 3].Position = new Vector3(1.0f + j, height, 1.0f + i);

                    this.lineList[count + 4].Position = new Vector3(1.0f + j, height, 1.0f + i);
                    this.lineList[count + 5].Position = new Vector3(1.0f + j, height, 0.0f + i);

                    this.lineList[count + 6].Position = new Vector3(1.0f + j, height, 0.0f + i);
                    this.lineList[count + 7].Position = new Vector3(0.0f + j, height, 0.0f + i);

                    count += 8;
                }
            }

        }

    }
}
