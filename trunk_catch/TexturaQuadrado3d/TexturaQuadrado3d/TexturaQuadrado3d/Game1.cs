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

        /*Um objeto VertexDeclaration armazena o formato do v�rtice para o 
         * dado contido em cada em cada v�rtice da figura ou modelo. 
         * Antes de desenhar o objeto, o GraphicsDevice precisa ser 
         * modificado para usar o formato correto para permitir a 
         * recupera��o apropriada do dado do v�rtice de cada v�rtice do array.
         * */
        /// <summary>
        /// Armazena o formato do vertice, neste exemplo guardara o formato
        /// do vertice de tipo VertexPositionColor
        /// </summary>
        VertexDeclaration vertexDeclaration;

        /// <summary>
        /// Ser� aplicada uma textura completa
        /// </summary>
        VertexPositionTexture[] fullTextureTriangleFan;

        /// <summary>
        /// Ser� aplicada uma textura parcial; uma parte da textura, 
        /// neste caso, ser� a metade
        /// </summary>
        VertexPositionTexture[] partialTextureTriangleFan;

        /// <summary>
        /// Define um objeto retangular para o array de vertices FullTextureTriangleFan
        /// </summary>
        /// <param name="posicao">A Textura ser� renderizada a partir desse ponto</param>
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
        /// <param name="posicao">A Textura ser� renderizada a partir desse ponto</param>
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

            //Os 2 metodos de defini��o, foram chamados no metodo LoadContent,
            //pelo fato deles utilizarem valores da inst�ncia textura
            this.define_FULL_Rectangular_Object(this.posicaoDaTexturaCompleta);
            this.define_Partial_Rectangular_Object(this.posicaoDaTexturaParcial);

            this.basicEffect = new BasicEffect(this.GraphicsDevice);

            this.vertexDeclaration = new VertexDeclaration(
                this.GraphicsDevice, VertexPositionTexture.VertexElements);

            /*Mudando a Origem da tela; O local aonde os eixos se cruzam.
             * No XNA, por padr�o, a origem est� no meio da janela. Com o codigo abaixo,
             * a origem ficar� no canto superior esquerdo da tela da janela; 
             * o padrao usado no ambiente 2D
             * */
            this.basicEffect.Projection
            = Matrix.CreateOrthographicOffCenter(
                    0, this.GraphicsDevice.Viewport.Width,
                    this.GraphicsDevice.Viewport.Height, 0, 0, 1);

            //Abilita a texturiza��o; permiti que a textura seja aplicada
            this.basicEffect.TextureEnabled = true;

            /*� toda primitiva renderizada por este efeito(basicEffect), e
             * que tem a capacidade de guardar coordenadas de textura, 
             * a ela ser� aplicada esta textura*/
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
