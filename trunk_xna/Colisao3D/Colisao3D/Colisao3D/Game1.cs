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

namespace Colisao3D
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Model cubo, plano;

        Matrix visao, projecao, mundoPlano, mundoCubo;

        Vector3 posicaoCamera, posicaoAlvo, posicaoCubo,
        posicaoPlano;

        BoundingBox colisaoCubo, colisaoPlano;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected BoundingBox CalculaColisao(Model modelo, Matrix mundo)
        {
            Vector3 max = new Vector3(float.MinValue, float.MinValue,
            float.MinValue);

            Vector3 min = new Vector3(float.MaxValue, float.MaxValue,
            float.MaxValue);

            foreach (ModelMesh mesh in modelo.Meshes)
            {


                foreach (ModelMeshPart meshPart in mesh.MeshParts)
                {

                    int vertexStride =
                    meshPart.VertexBuffer.VertexDeclaration.VertexStride;

                    int vertexBufferSize =
                    meshPart.NumVertices * vertexStride;


                    float[] vertexData =
                    new float[vertexBufferSize / sizeof(float)];


                    meshPart.VertexBuffer.GetData<float>(vertexData);

                    
                    for (int i = 0; i < vertexBufferSize / sizeof(float);
                     i += vertexStride / sizeof(float))
                    {
                        Vector3 transformedPosition = 
                        Vector3.Transform(new Vector3(vertexData[i],
                        vertexData[i + 1], vertexData[i + 2]), mundo);

                        min = Vector3.Min(min, transformedPosition);
                        max = Vector3.Max(max, transformedPosition);

                    }
                }
            }

            return new BoundingBox(min, max);

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
            posicaoCamera = new Vector3(10, 10, 0);
            posicaoAlvo = Vector3.Zero;
            posicaoCubo = Vector3.Zero;
            posicaoPlano = new Vector3(0, -3, 0);

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
            plano = Content.Load<Model>("plano");
            cubo = Content.Load<Model>("cubo");

            visao = Matrix.CreateLookAt(posicaoCamera,
            posicaoAlvo, Vector3.Up);

            projecao = Matrix.CreatePerspectiveFieldOfView(
            MathHelper.ToRadians(80),
            graphics.GraphicsDevice.Viewport.AspectRatio,
            0.1f,
            100.0f);

            mundoPlano = Matrix.CreateTranslation(posicaoPlano);

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

            KeyboardState teclado = Keyboard.GetState();

            if (teclado.IsKeyDown(Keys.Up))
                posicaoCubo.Y += 0.1f;
            else if (teclado.IsKeyDown(Keys.Down))
                posicaoCubo.Y -= 0.1f;

            colisaoCubo = CalculaColisao(cubo, mundoCubo);
            colisaoPlano = CalculaColisao(plano, mundoPlano);

            if (colisaoCubo.Intersects(colisaoPlano))
            {
                posicaoCubo.Y += 0.1f;
            }

            mundoCubo = Matrix.CreateTranslation(posicaoCubo);

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

            foreach (ModelMesh mesh in cubo.Meshes)
            {
                foreach (BasicEffect efeito in mesh.Effects)
                {
                    efeito.EnableDefaultLighting();
                }
            }

            foreach (ModelMesh mesh in plano.Meshes)
            {
                foreach (BasicEffect efeito in mesh.Effects)
                {
                    efeito.EnableDefaultLighting();
                }
            }

            // TODO: Add your drawing code here
            cubo.Draw(mundoCubo, visao, projecao);
            plano.Draw(mundoPlano, visao, projecao);

            base.Draw(gameTime);
        }
    }
}
