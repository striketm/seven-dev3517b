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

namespace RPG_The_Game.Objetos
{
    class Rato:Objetos.Sprite
    {
        SoundEffect efeitoSonoro;
        /// <summary>
        /// Para aonde o rato vai se mover
        /// </summary>
        public Vector2 posMouse;
        /// <summary>
        /// Calcular a diferença entre a posição rato e a do mouse
        /// </summary>
        public Vector2 diferenca;
        int vida;
        int pontos;

        public int jogador;

        public Vector2 PosMouse
        {
            get
            {
                return posMouse;
            }

            set
            {
                posMouse = value;
            }
        }
        public Vector2 Posicao
        {
            get
            {
                return posicao;
            }

            set
            {
                posicao = value;
            }
        }

        public animacao andando_direita_esquerda;
        animacao andando_cima_baixo;
        animacao parado;
        animacao animacao_atual;

        public Rato(Texture2D textura, Vector2 posicao, Vector2 velocidade, GameWindow window)
            :base(textura)
        {
            this.textura = textura;
            this.posicao = posicao;
            this.velocidade = velocidade;
            this.window = window;
        }

        public Rato(Texture2D textura, GameWindow window, SoundEffect efeitoSonoro)
            : base(textura)
        {
            this.textura = textura;
            this.window = window;
            this.efeitoSonoro = efeitoSonoro;

            andando_direita_esquerda = new animacao();
            andando_direita_esquerda.qtd_quadros = 8;
            andando_direita_esquerda.quadros_seg = 2;
            andando_direita_esquerda.Y = 0;
            andando_direita_esquerda.quadro_X = textura.Width / andando_direita_esquerda.qtd_quadros;
            andando_direita_esquerda.quadro_Y = textura.Height / 3;

            andando_cima_baixo = new animacao();
            andando_cima_baixo.qtd_quadros = 4;
            andando_cima_baixo.quadros_seg = 2;
            andando_cima_baixo.Y = 0;
            andando_cima_baixo.quadro_X = textura.Width / andando_direita_esquerda.qtd_quadros;
            andando_cima_baixo.quadro_Y = textura.Height / 3;

            animacao_atual = andando_cima_baixo;

            destino = new Rectangle(0, 0, andando_direita_esquerda.quadro_X, andando_direita_esquerda.quadro_Y);

        }

        public override void Update(GameTime gameTime) { /*conflfo*/}
             
        public void Update(GameTime gameTime, KeyboardState teclado, KeyboardState teclado_anterior)
        {

            colisao.X = (int)posicao.X;
            colisao.Y = (int)posicao.Y;
            colisao.Width = (int)origem.Width;
            colisao.Height = (int)origem.Height;

            if (jogador == 1)
            {

                if (teclado.IsKeyDown(Keys.Right))
                {
                    posicao.X += velocidade.X * (float)gameTime.ElapsedGameTime.Milliseconds;
                }

                if (teclado.IsKeyDown(Keys.Left))
                {
                    posicao.X -= velocidade.X;
                }

                if (teclado.IsKeyDown(Keys.Up))
                {
                    posicao.Y -= velocidade.Y;
                }

                if (teclado.IsKeyDown(Keys.Down))
                {
                    posicao.Y += velocidade.Y;
                }

                if (teclado.IsKeyDown(Keys.Space) && !teclado_anterior.IsKeyDown(Keys.Space))
                {
                    efeitoSonoro.Play();
                }
                
                if (posicao.X >= window.ClientBounds.Width - destino.Width)
                {
                    posicao.X = window.ClientBounds.Width - destino.Width;
                }

                if (posicao.Y >= window.ClientBounds.Height - destino.Height)
                {
                    posicao.Y = window.ClientBounds.Height - destino.Height;
                }

                if (posicao.X <= 0)
                {
                    posicao.X = 0;
                }

                if (posicao.Y <= 0)
                {
                    posicao.Y = 0;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(imagem, posicao, Color.Yellow);
            base.Draw();
        }

        public bool Bateu(Rato rato)
        {
            if(this.colisao.Intersects(rato.Colisao))
            {
                return true;
            }
            else return false;
  
        }
    }
}
