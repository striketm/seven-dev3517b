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

namespace Objeto3D_2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Model carro;

        Matrix visao, projecao, mundo;

        Vector3 posicaoCamera, posicaoCarro;

        float rotacao;

        float velocidade;



        KeyboardState teclado;
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
            posicaoCarro = new Vector3(0, 0, 0);
            posicaoCamera = new Vector3(10, 10, 0);
            rotacao = 0;
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
            carro = Content.Load<Model>("modelos/carro");

            projecao = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                graphics.GraphicsDevice.Viewport.AspectRatio, 0.1f, 100.0f);

            visao = Matrix.CreateLookAt(posicaoCamera, posicaoCarro, Vector3.Up);
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
           teclado = Keyboard.GetState();

           if (teclado.IsKeyDown(Keys.Up))
           {
               if (velocidade < 1) 
               velocidade += 0.03f;
             
           }
           else if (teclado.IsKeyDown(Keys.Down))
           {
               if (velocidade > -1) 
               velocidade -= 0.03f;
            
           }
           else
           {
               velocidade = 0;
           }

           if (teclado.IsKeyDown(Keys.Left))
               rotacao += 0.1f;
           else if (teclado.IsKeyDown(Keys.Right))
               rotacao -= 0.1f;
          

           // O CÓDIGO ABAIXO, UM POUCO DIFERENTE DOS OUTROS, ATUALIZA OS EIXOS X E Z, DE ACORDO COM A ROTAÇÃO DO CARRO (NO EIXO Y)
            
           //VEJAM O CÓDIGO E AGUARDEM. 

           //OBS : UTILIZE ESSE CÓDIGO CASO QUEIRA CRIAR UM JOGO DE CORRIDA

           Vector3 novaPosicaoCarro = new Vector3(0, 0, velocidade);
         
           posicaoCarro.Z += Vector3.Transform(novaPosicaoCarro, Matrix.CreateRotationY(rotacao)).Z;
           posicaoCarro.X += Vector3.Transform(novaPosicaoCarro, Matrix.CreateRotationY(rotacao)).X;


          
           //A MATRIZ DE VISÃO É SEMPRE ATUALIZADA DE FORMA QUE A CAMERA SEMPRE "OLHE" PARA O CARRO
           visao = Matrix.CreateLookAt(posicaoCamera, posicaoCarro, Vector3.Up);

           mundo = Matrix.CreateRotationY(rotacao) * Matrix.CreateTranslation(posicaoCarro);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            
            //QUE TAL COLOCAR AQUI O CÓDIGO DE ILUMINAÇÃO BÁSICA HEIN ? É O MESMO DO CUBO DO PROJETO "Objeto3D"

            carro.Draw(mundo , visao, projecao);
            base.Draw(gameTime);
        }
    }
}
