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
        Camera cameraMapa;

        ObjetoJogo chao;

        Jogador jogador;

        Texture2D teste;

        KeyboardState teclado_atual, teclado_anterior;
        MouseState mouse_atual, mouse_anterior;
        GamePadState gamepad_atual, gamepad_anterior;
                
        Viewport viewportPadrao;
        Viewport viewportMapa1;
        Viewport viewportMapa2;
        Matrix MatrizViewMapa;
        Matrix MatrizProjecaoMapa;
        
        //TODO
        /*
         * Criar a câmera, o chão, um Jogador, uma lista de Energia, uma lista de Barreira (cilindro, cubo e piramide)
         * 1- em posições fixas pelo cenario OK - pinhel
         * 2- em posicoes aleatorias pelo cenario (cuidado com a quantidade e colisão entre eles) ~OK - pinhel
         * 3- mover o jogador e a camera junto atras dele OK
         * 4- fazer este mover com botao na tela por toque/mouse
         * 5- implementar fluxo de telas
         * 6- implementar lógica de jogo
         * 0- acrescentar o método de colisão na classe ObjetoJogo OK
         * 0- criar um repositório para sua versão e colocar o link no grupo OK
         * 
         * CRIAR 4 SETAS, POSICIONADAS "PERTO" DA NAVE, E QUANDO CLICAR NESTAS
         * MOVER A NAVE
         * 
         * todo: transparencia (classe sprite) e outra camera (mapa)
         */
                 

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
            
            //viewportPadrao = new Viewport();
            viewportPadrao = GraphicsDevice.Viewport;
            viewportMapa1 = new Viewport(0, 0, 80, 60);
            viewportMapa2 = new Viewport(600, 0, 80, 60);

            MatrizViewMapa = Matrix.CreateLookAt(new Vector3(0, 200, 0), Vector3.Zero, new Vector3(0, 0, 1));
            //MatrizProjecaoMapa = Matrix.CreateOrthographic(80, 60, .1f, 300);
            MatrizProjecaoMapa = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f), 4f / 3f, 1f, 200f);
            teste = Content.Load<Texture2D>("teste");

            camera = new Camera();
            cameraMapa = new Camera();
            cameraMapa.MatrizVisualizacao = MatrizViewMapa;
            cameraMapa.MatrizProjecao = MatrizProjecaoMapa;

            chao = new ObjetoJogo();
            chao.Modelo = Content.Load<Model>("chao");

            jogador = new Jogador(Content, "jogador");
            jogador.Escala = 0.1f;
            
            for (int i = 0; i < Energia.QTDTotal; i++)
            {
                Energia e = new Energia(Content, "energia");
                e.Escala = 0.05f;
                e.PosicaoX += random.Next(-90, 90);
                e.PosicaoZ += random.Next(-90, 90);
            }

            for (int i = 0; i < Barreira.QTDTotal; i++)
            {
                Barreira b = new Barreira(Content, "cilindro");
                b.Escala = 0.1f;
                b.PosicaoX += random.Next(-90, 90);
                b.PosicaoZ += random.Next(-90, 90);
            }

            for (int i = 0; i < Barreira.QTDTotal; i++)
            {
                Barreira b = new Barreira(Content, "cubo");
                b.Escala = 0.1f;
                b.PosicaoX += random.Next(-90, 90);
                b.PosicaoZ += random.Next(-90, 90);
            }

            for (int i = 0; i < Barreira.QTDTotal; i++)
            {
                Barreira b = new Barreira(Content, "piramide");
                b.Escala = 0.1f;
                b.PosicaoX += random.Next(-90, 90);
                b.PosicaoZ += random.Next(-90, 90);
            }

            
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
            ///TODO
            ///fazer o botão de sair
            
            #region atualização de inputs
            //feito assim com os do frame anterior na frente da atualização dos do frame atual...
            teclado_anterior = teclado_atual;
            mouse_anterior = mouse_atual;
            gamepad_anterior = gamepad_atual;
            teclado_atual = Keyboard.GetState();
            mouse_atual = Mouse.GetState();
            gamepad_atual = GamePad.GetState(PlayerIndex.One);
            #endregion

            jogador.Update(gameTime, teclado_atual, teclado_anterior);
            
            camera.Update(jogador.DirecaoFrontal, jogador.Posicao);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //GraphicsDevice.BlendState = BlendState.Opaque;
            //GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            //GraphicsDevice.RasterizerState = RasterizerState.CullNone;
            //GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
            //http://blogs.msdn.com/b/shawnhar/archive/2010/06/18/spritebatch-and-renderstates-in-xna-game-studio-4-0.aspx
            //TODO: research!

            GraphicsDevice.Viewport = viewportPadrao;
            
            foreach (ObjetoJogo objetoJogo in ObjetoJogo.listaObjetos)
            {
                objetoJogo.Desenhar(camera);
            }

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Opaque, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone);
            spriteBatch.Draw(teste, new Vector2(400, 0), Color.White);
            spriteBatch.End();

            GraphicsDevice.Viewport = viewportMapa1;

            foreach (ObjetoJogo objetoJogo in ObjetoJogo.listaObjetos)
            {
                objetoJogo.Desenhar(camera);
            }

            GraphicsDevice.Viewport = viewportMapa2;

            foreach (ObjetoJogo objetoJogo in ObjetoJogo.listaObjetos)
            {
                objetoJogo.Desenhar(cameraMapa);
            }

            base.Draw(gameTime);
        }
    }
}

