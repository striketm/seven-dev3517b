
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace RPG_The_Game
{
    class Rato
    {
        GameWindow window;
        Texture2D imagem;
        Vector2 posicao;
        Vector2 velocidade;
        public bool visivel;
        int vida;
        int pontos;


        public Rato(Texture2D imagem, Vector2 posicao, Vector2 velocidade, GameWindow window)
        {
            this.imagem = imagem;
            this.velocidade = velocidade;
            this.window = window;
            //this.posicao = GameManager.randomPosicao();
        }

        public void Update(GameTime gameTime, KeyboardState teclado)
        {
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

            if (posicao.X >= window.ClientBounds.Width - imagem.Width)
            {
                posicao.X = window.ClientBounds.Width - imagem.Width;
            }

            if (posicao.Y >= window.ClientBounds.Height - imagem.Height)
            {
                posicao.Y = window.ClientBounds.Height - imagem.Height;
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
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
             spriteBatch.Draw(imagem, posicao, Color.Yellow);
        }
    }
}
