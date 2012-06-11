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

namespace Objeto3D
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        //A classe Model é  responsável por carregar um modelo 3D (de arquivos de extensão X. e .FBX)
        Model cubo;

        Matrix visao, projecao, mundo;

        Vector3 posicaoCamera, posicaoCubo;

        
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
            posicaoCubo = new Vector3(0, 0, 0);
            posicaoCamera = new Vector3(10, 10, 0);

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

            //Carrego o modelo
            cubo = Content.Load<Model>("cubo");
            
            projecao = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                graphics.GraphicsDevice.Viewport.AspectRatio, 0.1f, 100.0f);

            visao = Matrix.CreateLookAt(posicaoCamera, posicaoCubo, Vector3.Up);

            mundo = Matrix.CreateTranslation(posicaoCubo);
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

            //O loop abaixo aplica um efeito de iluminação básica no cubo (NÃO É OBRIGATÓRIO PARA SUA VISUALIZAÇÃO, MAS É BOM PARA DAR "AQUELE DETALHE") 
            foreach (ModelMesh mesh in cubo.Meshes)
            {
                foreach (BasicEffect efeito in mesh.Effects)
                {
                    efeito.EnableDefaultLighting();
                }
            }

           

            //Aqui desenho o cubo na tela
            cubo.Draw(mundo, visao, projecao);
            base.Draw(gameTime);
        }
    }
}
