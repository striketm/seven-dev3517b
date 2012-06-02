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

namespace Pong
{
    /// <summary>
    /// Aqui será usado para criar várias bolas genéricas no jogo
    /// </summary>
    class Bola
    {
        #region atributos (substantivos, coisas que uma bola tem)

        int tamanho;
        int peso;

        public struct animacao
        {
            public int quadro_X;
            public int quadro_Y;
            public int qtd_quadros;
            public int quadros_seg;
            public int Y;
        }
                
        /// <summary>
        /// Esta imagem possui uma animação de 3 quadros
        /// </summary>
        Texture2D imagem;//textura

        Vector2 posicao;
        Vector2 velocidade;
        public Rectangle colisao;
        Color cor;

        GameWindow Window;

        Random random;

        public animacao girando_esquerda;
        public animacao girando_direita;
        public animacao animacao_atual;

        #endregion

        #region métodos (verbos, coisas que a bola faz)

        /// <summary>
        /// Construtor, chamado sempre que uma bola é criada
        /// </summary>
        public Bola(ContentManager Content, GameWindow Window, Random random, float deslocamentoInicialX)
        {
            this.random = random;
            this.imagem = Content.Load<Texture2D>("bola");
            this.posicao = new Vector2((Window.ClientBounds.Width / 2 - this.imagem.Width / 2)
                + deslocamentoInicialX, Window.ClientBounds.Height / 2 - this.imagem.Height / 2);
            
           
            this.velocidade = new Vector2(
                random.Next(-3,3), 
                random.Next(-3,3));

            this.colisao = new Rectangle((int)posicao.X, (int)posicao.Y, imagem.Width, imagem.Height);

            this.Window = Window;

            this.cor = Color.White;

            animacao_atual = new animacao();
            girando_direita = new animacao();
            girando_esquerda = new animacao();

            girando_direita.qtd_quadros = 3;
            girando_direita.quadros_seg = 3;
            girando_direita.quadro_X = 22;
            girando_direita.quadro_Y = 25;
            girando_direita.Y = 0;

            girando_esquerda.qtd_quadros = 3;
            girando_esquerda.quadros_seg = 3;
            girando_esquerda.quadro_X = 22;
            girando_esquerda.quadro_Y = 25;
            girando_esquerda.Y = 25;

            animacao_atual = girando_esquerda;

        }

        public void Update(Rectangle paletaD, Rectangle paletaE)//, Rectangle outraBola)
        {
            posicao += velocidade;

            #region manter na tela
            if (posicao.Y > Window.ClientBounds.Height - imagem.Height)
            {
                velocidade.Y *= -1;
            }

            if (posicao.X > Window.ClientBounds.Width - imagem.Width)
            {

                velocidade.X *= -1;
            }

            if (posicao.Y < 0)
            {

                velocidade.Y *= -1;
            }
            if (posicao.X < 0)
            {

                velocidade.X *= -1;
            }
            #endregion

            //atualizar posição do retângulo de colisão segundo posição
            colisao.Location = new Point((int)posicao.X, (int)posicao.Y);

            if (Colidiu(paletaD) || Colidiu(paletaE))
            {
                velocidade.X *= -1;
            }

            //if (Colidiu(outraBola))
            //{
            //    velocidade.X *= -1;
            //}

        }

        private bool Colidiu(Rectangle outraCoisa)
        {
            if (colisao.Intersects(outraCoisa))
            {
                return true;
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.imagem, this.posicao, this.cor);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, animacao _animacao)
        {
            
                int frame = (int)(gameTime.TotalGameTime.TotalSeconds * _animacao.quadros_seg) % _animacao.qtd_quadros;
            
                spriteBatch.Draw(
                        imagem,
                        new Rectangle(
                            (int)posicao.X,
                            (int)posicao.Y,
                            _animacao.quadro_X,
                            _animacao.quadro_Y),
                        new Rectangle(
                            frame * _animacao.quadro_X,
                            _animacao.Y,
                            _animacao.quadro_X,
                            _animacao.quadro_Y),
                        Color.White
                       );
            
        }
        #endregion        
    }
}
