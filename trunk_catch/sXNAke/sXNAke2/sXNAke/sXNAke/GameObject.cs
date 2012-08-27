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

namespace sXNAke
{
    /// <summary>
    /// Classe usada para adicionar objetos na tela contendo um Update e um Draw
    /// </summary>
    abstract class GameObject
    {
        public enum Direction { LEFT, RIGHT, UP, DOWN, }

        #region Atributos

        protected Texture2D texture;
        protected Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        private Vector2 velocity;
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        /// <summary>
        /// Não é preciso atualizar a posicao
        /// </summary>
        private Rectangle collisionRect;
        public Rectangle CollisionRect
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            }
            set
            {
                collisionRect = value;
            }
        }

        protected GameWindow Window;
        protected Random random;
        private Color color;

        public Color TextureColor
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private bool visible;
        public bool Visible
        {
            get
            {
                return visible;
            }

            set
            {
                visible = value;
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Construtora da Classe
        /// </summary>
        public GameObject()
        {
            Visible = true;
        }
        /// <summary>
        /// Toda a Lógica do jogo é adicionada aqui
        /// </summary>
        public abstract void Update();
        
        /// <summary>
        /// bla bla bla
        /// </summary>

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Visible)
                spriteBatch.Draw(texture, position, Color.White);
        }

        #endregion
    }
}
