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

        public animacao andando;
        animacao parado;
        animacao atacando;
        animacao pulando;

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

            andando = new animacao();
            andando.qtd_quadros = 4;
            andando.quadros_seg = 2;
            andando.Y = 0;
            andando.quadro_X = textura.Width / andando.qtd_quadros;
            andando.quadro_Y = textura.Height;

            destino = new Rectangle(0, 0, andando.quadro_X, andando.quadro_Y);

        }

        public override void Update(GameTime gameTime) { }
             
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
        //public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        //{
        //     spriteBatch.Draw(imagem, posicao, Color.Yellow);
        //}

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
