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
    /// Classe para criar um triângulo colorido
    /// </summary>
    class BasicPrimitive
    {
        /// <summary>
        /// Um array de vértices com posição e cor
        /// </summary>
        VertexPositionColorTexture[] primitives;

        /// <summary>
        /// Um efeito básico para desenho3d
        /// </summary>
        BasicEffect basicEffect;

        /// <summary>
        /// A placa de vídeo
        /// </summary>
        GraphicsDevice graphicsDevice;

        /// <summary>
        /// Matriz de "mundo" - posicionamento, escala e rotação... sempre atualizado
        /// </summary>
        public Matrix World = Matrix.Identity;

        Vector2 posicaoTextura;
        Texture2D textura;
        
        /// <summary>
        /// Construtor de um triângulo com cores de vértice fixas
        /// </summary>
        /// <param name="graphicsDevice">A refência da placa de vídeo</param>
        public BasicPrimitive(GraphicsDevice graphicsDevice, ContentManager Content)
        {
            this.graphicsDevice = graphicsDevice;

            //Criar os vértices do triangulo:
            primitives = new VertexPositionColorTexture[4];

            primitives[0] = new VertexPositionColorTexture();
            primitives[0].Position = new Vector3(0, 1.0f, 0);
            primitives[0].Color = Color.Red;

            primitives[1] = new VertexPositionColorTexture();
            primitives[1].Position = new Vector3(1, -0.5f, 0);
            primitives[1].Color = Color.Green;

            primitives[2] = new VertexPositionColorTexture();
            primitives[2].Position = new Vector3(-1, -0.5f, 0);
            primitives[2].Color = Color.Blue;

            primitives[3] = new VertexPositionColorTexture();
            primitives[3].Position = new Vector3(0, -1.0f, 0);
            primitives[3].Color = Color.Yellow;
            
            //Criar novo efeito básico e propriedades:
            basicEffect = new BasicEffect(graphicsDevice);

            //ativando as cores
            //basicEffect.VertexColorEnabled = true;

            basicEffect.TextureEnabled = true;
            basicEffect.Texture = Content.Load<Texture2D>("GameThumbnail");

            primitives[0].TextureCoordinate = new Vector2(0, 0);
            primitives[1].TextureCoordinate = new Vector2(1, 0);
            primitives[2].TextureCoordinate = new Vector2(1, 1);
            primitives[3].TextureCoordinate = new Vector2(0, 1);

        }

        /// <summary>
        /// Método de atualização
        /// </summary>
        /// <param name="gameTime">Tempo do jogo</param>
        public void Update(GameTime gameTime)
        {
            
        }

        /// <summary>
        /// Método para desenho
        /// </summary>
        /// <param name="gameTime">Tempo do jogo</param>
        public void Draw(GameTime gameTime, BasicCamera camera)
        {
            //passando o efeito básico
            basicEffect.CurrentTechnique.Passes[0].Apply();

            basicEffect.View = camera.ViewMatrix;
            basicEffect.Projection = camera.ProjectionMatrix;
            
            basicEffect.World = this.World;

            //desenhando as primitivas
            graphicsDevice.DrawUserPrimitives<VertexPositionColorTexture>(
                PrimitiveType.TriangleStrip,//tipo da primitiva (linha ou triangulo)
                primitives,//o conjunto de vértices 
                0,//offset
                2);//contador de primitivas

        }
    }
}
