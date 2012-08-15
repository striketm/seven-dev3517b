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
    class TexturedPrimitive
    {
        ///
        /// Shader provido pelo XNA.
        /// Reponsavel por renderizar as primitivas
        ///
        BasicEffect basicEffect;

        ///
        /// Será aplicada uma textura completa
        ///
        VertexPositionColor[] fullTextureTriangleFan;
        
        GraphicsDevice graphicsDevice;

        Vector2 posicao;
        Texture2D textura;

        public TexturedPrimitive(GraphicsDevice graphicsDevice, ContentManager Content)
        {
            textura = Content.Load<Texture2D>("GameThumbnail");
            posicao = Vector2.Zero;

            this.graphicsDevice = graphicsDevice;

            this.basicEffect = new BasicEffect(graphicsDevice);

            //Instanciado os 4 vertices

            this.fullTextureTriangleFan = new VertexPositionColor[3];

            fullTextureTriangleFan[0].Position = new Vector3(0,1,0);//posicao.X;
            //fullTextureTriangleFan[0].Position.Y = new Vector3(0, 1, 0);// posicao.Y;
            fullTextureTriangleFan[0].Color = Color.Red;
            //fullTextureTriangleFan[0].TextureCoordinate = new Vector2(0, 0);

            //fullTextureTriangleFan[1].Position.X = posicao.X + this.textura.Width;
            fullTextureTriangleFan[1].Position = new Vector3(1, 0, 0);
            fullTextureTriangleFan[0].Color = Color.Green;
            //fullTextureTriangleFan[1].Position.Y = posicao.Y;
            //fullTextureTriangleFan[1].TextureCoordinate = new Vector2(1, 0);

            //fullTextureTriangleFan[2].Position.X = posicao.X + this.textura.Width;
            fullTextureTriangleFan[2].Position = new Vector3(0, 0, 0);
            fullTextureTriangleFan[0].Color = Color.Blue;
            //fullTextureTriangleFan[2].Position.Y = posicao.Y + this.textura.Height;
            //fullTextureTriangleFan[2].TextureCoordinate = new Vector2(1, 1);

            //fullTextureTriangleFan[3].Position.X = posicao.X;
            //fullTextureTriangleFan[3].Position.Y = posicao.Y + this.textura.Height;
            //fullTextureTriangleFan[3].TextureCoordinate = new Vector2(0, 1);

            
            //Habilita a texturização; permite que a textura seja aplicada           
            //this.basicEffect.TextureEnabled = true;

            //this.basicEffect.Texture = textura;
            //ativando as cores
            basicEffect.VertexColorEnabled = true;

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

            //desenhando as primitivas
            graphicsDevice.DrawUserPrimitives<VertexPositionColor>(
                PrimitiveType.TriangleStrip,//tipo da primitiva (linha ou triangulo)
                fullTextureTriangleFan,//o conjunto de vértices 
                0,//offset
                1);//contador de primitivas

        }

    }
}
