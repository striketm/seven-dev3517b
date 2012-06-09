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

namespace DesenharSprite
{
    /// <summary>
    /// O sprite
    /// </summary>
    class Sprite
    {
        /// <summary>
        /// A imagem
        /// </summary>
        Texture2D imagem;
        public Texture2D Imagem
        {
            get { return imagem; }
            set { imagem = value; }
        }

        /// <summary>
        /// A posição
        /// </summary>
        Vector2 posicao;
        public Vector2 Posicao
        {
            get { return posicao; }
            set { posicao = value; }
        }

        /// <summary>
        /// A velocidade
        /// </summary>
        Vector2 velocidade;
        public Vector2 Velocidade
        {
            get { return velocidade; }
            set { velocidade = value; }
        }

        /// <summary>
        /// A cor
        /// </summary>
        Color cor;
        public Color Cor
        {
            get { return cor; }
            set { cor = value; }
        }

        GameWindow Window;

        public Sprite(ContentManager Content, GameWindow Window)
        {
            this.Window = Window;
            this.cor = Color.White;

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Imagem, Posicao, Cor);
        }
    }
}
