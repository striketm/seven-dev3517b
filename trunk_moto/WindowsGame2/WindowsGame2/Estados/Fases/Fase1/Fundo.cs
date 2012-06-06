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


namespace MotoGame.Estados.Fases.Fase1
{
    class Fundo
    {
        Texture2D texture;
        Rectangle origem;
        Rectangle destino;
        GameWindow Window;

        /// <summary>
        /// Velocidade relativa horizontal desta camada de fundo em relação ao objeto principal / personagem
        /// </summary>
        private float velocidadeRelativaX;

        public float VelocidadeRelativaX
        {
            private get
            {
                return velocidadeRelativaX;
            }
            set
            {
                velocidadeRelativaX = value;
            }
        }

        /// <summary>
        /// Velocidade relativa vertical desta camada de fundo em relação ao objeto principal / personagem
        /// </summary>
        private float velocidadeRelativaY;

        public float VelocidadeRelativaY
        {
            private get
            {
                return velocidadeRelativaY;
            }
            set
            {
                velocidadeRelativaY = value;
            }
        }



        public Fundo(Texture2D texture, GameWindow Window)
        {
            this.texture = texture;
            this.Window = Window;

            destino = new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height);

            origem = new Rectangle(0, texture.Height - Window.ClientBounds.Height, Window.ClientBounds.Width, Window.ClientBounds.Height);

            VelocidadeRelativaX = 1;
            VelocidadeRelativaY = 1;

        }

        public void Update(GameTime gameTime, Vector2 posicao)
        {
            origem.X += (int)(posicao.X * VelocidadeRelativaX);
            origem.Y += (int)(posicao.Y * VelocidadeRelativaY);

            //não pode ir mais para trás
            if(origem.X<0)
            {
                //não faz nada
                Console.WriteLine("origem.X<0");
                origem.X = 0;
            }

            //não pode ir mais para frente
            if (origem.X > texture.Bounds.Width - Window.ClientBounds.Width)
            {
                //não faz nada
                Console.WriteLine("origem.X > texture.Bounds.Width - Window.ClientBounds.Width");
                origem.X = texture.Bounds.Width - Window.ClientBounds.Width;
            }

            //não pode ir mais para cima
            if (origem.Y < 0)
            {
                //não faz nada
                Console.WriteLine("origem.Y < 0");
                origem.Y = 0;
            }

            //não pode ir mais para baixo
            if (origem.Y > texture.Bounds.Height - Window.ClientBounds.Height)
            {
                //não faz nada
                Console.WriteLine("origem.Y > texture.Bounds.Height - Window.ClientBounds.Height");
                origem.Y = texture.Bounds.Height - Window.ClientBounds.Height;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destino, origem, Color.White);
        }

    }
}
