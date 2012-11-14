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
        //VertexPositionColor[] primitivas;
        
        //Aqui nenhuma novidade, vamos apenas acrescentar as texturas:
        VertexPositionColorTexture[] primitivas;

        //Cria a instância de um shader (efeito especial) que iremos aplicar na nossa primitiva
        BasicEffect efeitoBasico;

        Matrix worldTest;

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
            //primitivas = new VertexPositionColorTexture[3];

            //para desenharmos um quadrado, precisamos de dois triângulos...
            //não podemos usar 4 pontos através do strip por causa das normais...
            //logo:

            primitivas = new VertexPositionColorTexture[6];

            //ajustando as posições do primeiro triângulo para fazer a parte de cima do quadrado:
            //lembrando a ordem e a normal
            //adicionando o posicionamento da textura
            primitivas[0] = new VertexPositionColorTexture();
            primitivas[0].Position = new Vector3(-0.5f, 0.5f, 0);
            primitivas[0].Color = Color.Red;
            
            primitivas[0].TextureCoordinate = new Vector2(0.0f, 0.0f);

            primitivas[1] = new VertexPositionColorTexture();
            primitivas[1].Position = new Vector3(0.5f, 0.5f, 0);
            primitivas[1].Color = Color.Green;
            
            primitivas[1].TextureCoordinate = new Vector2(1.0f, 0.0f);

            primitivas[2] = new VertexPositionColorTexture();
            primitivas[2].Position = new Vector3(0.5f, -0.5f, 0);
            primitivas[2].Color = Color.Blue;

            primitivas[2].TextureCoordinate = new Vector2(1.0f, 1.0f);

            //criando o triângulo da parte de baixo do quadrado:
            //lembrando a ordem e a normal
            //haverão dois pontos coincidentes
            //mudando as cores

            primitivas[3] = new VertexPositionColorTexture();
            primitivas[3].Position = new Vector3(-0.5f, 0.5f, 0);
            primitivas[3].Color = Color.Black;

            primitivas[3].TextureCoordinate = new Vector2(0.0f, 0.0f);

            primitivas[4] = new VertexPositionColorTexture();
            primitivas[4].Position = new Vector3(0.5f, -0.5f, 0);
            primitivas[4].Color = Color.White;

            primitivas[4].TextureCoordinate = new Vector2(1.0f, 1.0f);

            primitivas[5] = new VertexPositionColorTexture();
            primitivas[5].Position = new Vector3(-0.5f, -0.5f, 0);
            primitivas[5].Color = Color.Yellow;

            primitivas[5].TextureCoordinate = new Vector2(0.0f, 1.0f);

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

            //Habilita o efeito de cores nos vértices:
            //efeitoBasico.VertexColorEnabled = true;

            //Habilita as texturas nos vértices:
            efeitoBasico.TextureEnabled = true;

            //Tem que carregar a textura:
            efeitoBasico.Texture = Content.Load<Texture2D>("box");

            //Exercício 01: Criar uma classe que crie uma primitiva com textura, como mostrado...

            //Exercício 02: Criar uma classe Camera

            //Exercício 03: Usando a classe que cria a "parede" acima, crie uma caixa
            //DICA: você pode rotacionar e transladar um objeto com mais facilidade,
            //adicionando nele uma Matrix World e chamando os métodos CreateTranslation
            //e CreateRotation_

            //

            worldTest = Matrix.Identity;

            worldTest = Matrix.CreateTranslation(1.0f, 0.0f, 0.0f);

            //CUIDADO!

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

            efeitoBasico.World = worldTest;

            //não esquecer de mudar para list e a quantidade de primitivas desenhadas...
            GraphicsDevice.DrawUserPrimitives<VertexPositionColorTexture>(PrimitiveType.TriangleList,
                primitivas, 0, 2);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
