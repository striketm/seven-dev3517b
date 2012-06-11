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

namespace Primitiva3D
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Define um array que vai armazenar os vertices de uma primitiva (triangulo)
        VertexPositionColor[] primitivas;

        //Cria a instância de um shader (efeito especial) que iremos aplicar na nossa primitiva
        BasicEffect efeitoBasico;

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

            //Alocando o objeto na memória 
            primitivas = new VertexPositionColor[3];

            primitivas[0] = new VertexPositionColor();
            primitivas[0].Position = new Vector3(0, 1, 0);
            primitivas[0].Color = Color.Red;

            primitivas[1] = new VertexPositionColor();
            primitivas[1].Position = new Vector3(1, -1, 0);
            primitivas[1].Color = Color.Green;

            primitivas[2] = new VertexPositionColor();
            primitivas[2].Position = new Vector3(-1, -1, 0);
            primitivas[2].Color = Color.Blue;

            efeitoBasico = new BasicEffect(GraphicsDevice);

            

            //Cria a Matriz de visão (aqui definimos a posicao da camera (primeiro argumento), a posição alvo na qual na qual ela vai estar vendo (segundo argumento) e o Vector.Up (argumento padrão))
            efeitoBasico.View = Matrix.CreateLookAt(new Vector3(0, 0, 3), new Vector3(0, 0, 0), Vector3.Up);

            //Cria a Matriz de projeção (define como o objeto será visualizado na tela)

            //Primeiro argumento definimos o campo de visão (MathHelp.PiOver4 = PI dividido por 4 (3.14 / 4))
            //O segundo argumento definimos o aspect ratio (que está relacionado à resolução da tela)
            //O terceiro argumento definimos a distância mínima na qual um objeto pode ser visualizado (0.1f)
            //O quarto argumento definimos a distância máxima na qual um objeto pode ser visualizado (100.0f)
            
            efeitoBasico.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
            graphics.GraphicsDevice.Viewport.AspectRatio, 0.1f, 100.0f);


            //Cria a Matriz de Mundo do objeto 3D (aqui definimos seu comportamento (translação/rotação/escala)) (atribuindo uma matrix identidade)
            efeitoBasico.World = Matrix.Identity;

            //Habilita o efeito de cores nos vertices
            efeitoBasico.VertexColorEnabled = true;

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
            GraphicsDevice.Clear(Color.Black);

            efeitoBasico.CurrentTechnique.Passes[0].Apply();

            GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip,
                primitivas, 0, 1);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
