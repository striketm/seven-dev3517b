using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace TexturaQuadrado3d
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        /// <summary>
        /// Shader provido pelo XNA.
        /// Reponsavel por renderizar as primitivas
        /// </summary>
        BasicEffect basicEffect;

        Texture2D textura;

        Vector2 posicaoDaTexturaCompleta;
        Vector2 posicaoDaTexturaParcial;

        /*Um objeto VertexDeclaration armazena o formato do vértice para o 
         * dado contido em cada em cada vértice da figura ou modelo. 
         * Antes de desenhar o objeto, o GraphicsDevice precisa ser 
         * modificado para usar o formato correto para permitir a 
         * recuperação apropriada do dado do vértice de cada vértice do array.
         * */
        /// <summary>
        /// Armazena o formato do vertice, neste exemplo guardara o formato
        /// do vertice de tipo VertexPositionColor
        /// </summary>
        VertexDeclaration vertexDeclaration;

        /// <summary>
        /// Será aplicada uma textura completa
        /// </summary>
        VertexPositionTexture[] fullTextureTriangleFan;

        /// <summary>
        /// Será aplicada uma textura parcial; uma parte da textura, 
        /// neste caso, será a metade
        /// </summary>
        VertexPositionTexture[] partialTextureTriangleFan;

        /// <summary>
        /// Define um objeto retangular para o array de vertices FullTextureTriangleFan
        /// </summary>
        /// <param name="posicao">A Textura será renderizada a partir desse ponto</param>
        void define_FULL_Rectangular_Object(Vector2 posicao)
        {
            //Instanciado os 4 vertices
            this.fullTextureTriangleFan = new VertexPositionTexture[4];

            fullTextureTriangleFan[0].Position.X = posicao.X;
            fullTextureTriangleFan[0].Position.Y = posicao.Y;
            fullTextureTriangleFan[0].TextureCoordinate = new Vector2(0, 0);

            fullTextureTriangleFan[1].Position.X = posicao.X + this.textura.Width;
            fullTextureTriangleFan[1].Position.Y = posicao.Y;
            fullTextureTriangleFan[1].TextureCoordinate = new Vector2(1, 0);

            fullTextureTriangleFan[2].Position.X = posicao.X + this.textura.Width;
            fullTextureTriangleFan[2].Position.Y = posicao.Y + this.textura.Height;
            fullTextureTriangleFan[2].TextureCoordinate = new Vector2(1, 1);

            fullTextureTriangleFan[3].Position.X = posicao.X;
            fullTextureTriangleFan[3].Position.Y = posicao.Y + this.textura.Height;
            fullTextureTriangleFan[3].TextureCoordinate = new Vector2(0, 1);
        }

        /// <summary>
        /// Define um objeto retangular para o array de vertices PartialTextureTriangleFan
        /// </summary>
        /// <param name="posicao">A Textura será renderizada a partir desse ponto</param>
        void define_Partial_Rectangular_Object(Vector2 posicao)
        {
            //Instanciado os 4 vertices
            this.partialTextureTriangleFan = new VertexPositionTexture[4];

            partialTextureTriangleFan[0].Position.X = posicao.X;
            partialTextureTriangleFan[0].Position.Y = posicao.Y;
            partialTextureTriangleFan[0].TextureCoordinate = new Vector2(0, 0);

            partialTextureTriangleFan[1].Position.X = posicao.X + this.textura.Width;
            partialTextureTriangleFan[1].Position.Y = posicao.Y;
            partialTextureTriangleFan[1].TextureCoordinate = new Vector2(0.5f, 0);

            partialTextureTriangleFan[2].Position.X = posicao.X + this.textura.Width;
            partialTextureTriangleFan[2].Position.Y = posicao.Y + this.textura.Height;
            partialTextureTriangleFan[2].TextureCoordinate = new Vector2(0.5f, 1);

            partialTextureTriangleFan[3].Position.X = posicao.X;
            partialTextureTriangleFan[3].Position.Y = posicao.Y + this.textura.Height;
            partialTextureTriangleFan[3].TextureCoordinate = new Vector2(0, 1);
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this.posicaoDaTexturaCompleta = new Vector2(25, 100);

            this.posicaoDaTexturaParcial = new Vector2(400, 100);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            textura = this.Content.Load<Texture2D>("Inventario");

            //Os 2 metodos de definição, foram chamados no metodo LoadContent,
            //pelo fato deles utilizarem valores da instância textura
            this.define_FULL_Rectangular_Object(this.posicaoDaTexturaCompleta);
            this.define_Partial_Rectangular_Object(this.posicaoDaTexturaParcial);

            this.basicEffect = new BasicEffect(this.GraphicsDevice);

            this.vertexDeclaration = new VertexDeclaration(
                this.GraphicsDevice, VertexPositionTexture.VertexElements);

            /*Mudando a Origem da tela; O local aonde os eixos se cruzam.
             * No XNA, por padrão, a origem está no meio da janela. Com o codigo abaixo,
             * a origem ficará no canto superior esquerdo da tela da janela; 
             * o padrao usado no ambiente 2D
             * */
            this.basicEffect.Projection
            = Matrix.CreateOrthographicOffCenter(
                    0, this.GraphicsDevice.Viewport.Width,
                    this.GraphicsDevice.Viewport.Height, 0, 0, 1);

            //Abilita a texturização; permiti que a textura seja aplicada
            this.basicEffect.TextureEnabled = true;

            /*À toda primitiva renderizada por este efeito(basicEffect), e
             * que tem a capacidade de guardar coordenadas de textura, 
             * a ela será aplicada esta textura*/
            this.basicEffect.Texture = this.textura;

            base.LoadContent();
        }

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            this.GraphicsDevice.VertexDeclaration = this.vertexDeclaration;

            this.basicEffect.Begin();

            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Begin();

                this.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleFan,
                    fullTextureTriangleFan, 0, 2);

                this.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleFan,
                    partialTextureTriangleFan, 0, 2);

                pass.End();
            }

            this.basicEffect.End();

            base.Draw(gameTime);
        }
    }
}
