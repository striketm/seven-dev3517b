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
        //protected GameWindow window;
        //protected Texture2D imagem;
        //protected Vector2 posicao;
        //protected Vector2 velocidade;
        //protected bool visivel;

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

        }

        public override void Update(GameTime gameTime) { }
             
        public void Update(GameTime gameTime, KeyboardState teclado, KeyboardState teclado_anterior)
        {
            if (diferenca.X < 0)
            {

            }
            if (diferenca.Y < 0)
            {

            }
            


            if (teclado.IsKeyDown(Keys.Right))
            {
                posicao.X += velocidade.X;
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

            if (posicao.X >= window.ClientBounds.Width - textura.Width)
            {
                posicao.X = window.ClientBounds.Width - textura.Width;
            }

            if (posicao.Y >= window.ClientBounds.Height - textura.Height)
            {
                posicao.Y = window.ClientBounds.Height - textura.Height;
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
        //public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        //{
        //     spriteBatch.Draw(imagem, posicao, Color.Yellow);
        //}
    }
}
