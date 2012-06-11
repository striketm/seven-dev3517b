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

namespace Shader
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

        //Cria a instância de um efeito(shader) (no caso, o que nós criamos, como você pode ver ao lado lá no content)
         //Nomeado de "vermelho.fx" (DÊ DOIS CLIQUES NESSE ARQUIVO E VEJA SEU CONTEÚDO)
        Effect cor_vermelho;

        Vector3 posicaoPrimitiva;

       

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
            posicaoPrimitiva = Vector3.Zero; //ISSO É O MESMO QUE "new Vector3(0,0,0)" ...NÃO PRECISA DO "NEW"
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

            cor_vermelho = Content.Load<Effect>("vermelho");

            cor_vermelho.CurrentTechnique = cor_vermelho.Techniques["Technique1"];

            //Cria a Matriz de Mundo do objeto 3D (atribuindo uma matrix identidade)



            //Cria a Matriz de visão 
             cor_vermelho.Parameters["View"].SetValue(Matrix.CreateLookAt(new Vector3(0, 0, 3), posicaoPrimitiva, Vector3.Up));

            //Cria a Matriz de projeção
            cor_vermelho.Parameters["Projection"].SetValue(Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
            graphics.GraphicsDevice.Viewport.AspectRatio, 0.1f, 100.0f));

            //Cria a matrix de mundo
            cor_vermelho.Parameters["World"].SetValue(Matrix.CreateTranslation(posicaoPrimitiva));

           

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

            foreach (EffectPass efeito in cor_vermelho.CurrentTechnique.Passes)
            {
                efeito.Apply();
                GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip,
                    primitivas, 0, 1);

            }

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
