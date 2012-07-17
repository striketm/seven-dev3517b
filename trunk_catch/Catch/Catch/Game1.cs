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
using CapturarObjetos.Nucleo;
using CapturarObjetos.Objetos;

namespace CapturarObjetos
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Random random = new Random();

        Camera camera;

        ObjetoJogo chao;

        Jogador jogador;

        //Energia energia;//manager

        Barreira cilindro, cubo, piramide;//manager

        //TODO
        /*
         * Criar a câmera, o chão, um Jogador (coisa), uma lista de Energia (objeto), uma lista de Barreira (cilindro, cubo e piramide)
         * 1- em posições fixas pelo cenario 
         * 2- em posicoes aleatorias pelo cenario (cuidado com a quantidade e colisão entre eles)
         * 3- mover o jogador e a camera junto atras dele
         * 4- fazer este mover com botao na tela por toque/mouse
         * 5- implementar fluxo de telas
         * 6- implementar lógica de jogo
         * 0- acrescentar o método de colisão na classe ObjetoJogo OK
         * 0- criar um repositório para sua versão e colocar o link no grupo 
         */
                 
        //ObjetoJogo cilindro;
        //ObjetoJogo coisa;
        //ObjetoJogo cubo;
        //ObjetoJogo esfera;
        //ObjetoJogo objeto;
        //ObjetoJogo piramide;        

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

            camera = new Camera();

            chao = new ObjetoJogo();
            chao.Modelo = Content.Load<Model>("chao");

            jogador = new Jogador(Content, "coisa");
            jogador.Escala = 0.1f;

            //energia = new Energia(Content, "objeto");
            //energia.Escala = 0.05f;
            //energia.PosicaoX += 3;

            for (int i = 0; i < Energia.QTDTotal; i++)
            {
                Energia e = new Energia(Content, "objeto");
                e.Escala = 0.05f;
                e.PosicaoX += random.Next(-3, 3);
            }

            cilindro = new Barreira(Content, "cilindro");
            cilindro.Escala = 0.1f;
            cilindro.PosicaoX += 5;

            cubo = new Barreira(Content, "cubo");
            cubo.Escala = 0.1f;
            cubo.PosicaoX -= 3;

            piramide = new Barreira(Content, "piramide");
            piramide.Escala = 0.1f;
            piramide.PosicaoX -= 5;
            
            //cilindro = new ObjetoJogo();
            //cilindro.Modelo = Content.Load<Model>("cilindro");
            //cilindro.Escala = 0.1f;
            //cilindro.PosicaoX -= 6;

            //coisa = new ObjetoJogo();
            //coisa.Modelo = Content.Load<Model>("coisa");
            //coisa.Escala = 0.1f;
            //coisa.PosicaoX -= 3;

            //cubo = new ObjetoJogo();
            //cubo.Modelo = Content.Load<Model>("cubo");
            //cubo.Escala = 0.1f;
            //cubo.PosicaoX -= 0;

            //esfera = new ObjetoJogo();
            //esfera.Modelo = Content.Load<Model>("esfera");//?cade
            //esfera.Escala = 0.9f;
            //esfera.PosicaoX -= 0;

            //objeto = new ObjetoJogo();
            //objeto.Modelo = Content.Load<Model>("objeto");
            //objeto.Escala = 0.05f;
            //objeto.PosicaoX += 2;

            //piramide = new ObjetoJogo();
            //piramide.Modelo = Content.Load<Model>("piramide");
            //piramide.Escala = 0.1f;
            //piramide.PosicaoX += 4;

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

            //"abrindo seus olhos":

            float rotacao = 0.0f;
            Vector3 posicao = new Vector3(0,0,0);
            camera.Update(rotacao, posicao);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            foreach (ObjetoJogo objetoJogo in ObjetoJogo.listaObjetos)
            {
                objetoJogo.Desenhar(camera);
            }

            base.Draw(gameTime);
        }
    }
}

