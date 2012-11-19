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

namespace WindowsGame1
{
    /// <summary>
    /// 
    /// </summary>
    abstract class Sprite
    {
        /// <summary>
        /// 
        /// </summary>
        GameWindow window;
        /// <summary>
        /// 
        /// </summary>
        protected GameWindow Window
        {
            get
            {
                return window;
            }
            set
            {
                window = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        Texture2D texture;
        /// <summary>
        /// 
        /// </summary>
        protected Texture2D Texture
        {
            get
            {
                return texture;
            }
            set
            {
                texture = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        Vector2 position;
        /// <summary>
        /// 
        /// </summary>
        protected Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        Vector2 velocity;
        /// <summary>
        /// 
        /// </summary>
        protected Vector2 Velocity
        {
            get
            {
                return velocity;
            }
            set
            {
                velocity = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        bool visible;
        /// <summary>
        /// 
        /// </summary>
        protected bool Visible
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

        /// <summary>
        /// 
        /// </summary>
        Rectangle frame;
        /// <summary>
        /// 
        /// </summary>
        protected Rectangle Frame
        {
            get
            {
                return frame;
            }
            set
            {
                frame = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        Rectangle destination;
        /// <summary>
        /// 
        /// </summary>
        protected Rectangle Destination
        {
            get
            {
                return destination;
            }
            set
            {
                destination = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        Rectangle collision;
        /// <summary>
        /// 
        /// </summary>
        protected Rectangle Collision
        {
            get
            {
                return collision;//PIVOT!!!
                //return new Rectangle((int)Posicao.X, (int)Posicao.Y, animacao_atual.quadro_X, animacao_atual.quadro_Y);
            }
            set
            {
                collision = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        Color color;
        /// <summary>
        /// 
        /// </summary>
        protected Color Color
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
        float alpha;
        /// <summary>
        /// 
        /// </summary>
        protected float Alpha
        {
            get
            {
                return alpha;
            }
            set
            {
                alpha = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        float rotation;
        /// <summary>
        /// 
        /// </summary>
        protected float Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        Vector2 pivot;
        /// <summary>
        /// 
        /// </summary>
        protected Vector2 Pivot
        {
            get
            {
                return pivot;
            }
            set
            {
                pivot = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        Vector2 scale;
        /// <summary>
        /// 
        /// </summary>
        protected Vector2 Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        float layer;
        /// <summary>
        /// 
        /// </summary>
        protected float Layer
        {
            get
            {
                return layer;
            }
            set
            {
                layer = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        bool right;
        /// <summary>
        /// 
        /// </summary>
        protected bool Right
        {
            get
            {
                return right;
            }
            set
            {
                right = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        bool up;
        /// <summary>
        /// 
        /// </summary>
        protected bool Up
        {
            get
            {
                return up;
            }
            set
            {
                up = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        struct animation
        {
            public int quadro_X;//get set origem destino
            public int quadro_Y;
            public int qtd_quadros;
            public int quadros_seg;
            public int Y_inicial;
            public int X_inicial;
            public string nome;
            public bool ativa;
            public int quadro_atual;
        }

        /// <summary>
        /// 
        /// </summary>
        animation current_animation;
        
        /// <summary>
        /// 
        /// </summary>
        public Sprite()
        {
            
            //this.textura = textura;
            //this.posicao = new Vector2(0, 0);
            //this.velocidade = new Vector2(1, 1);
            //this.origem = new Rectangle(0, 0, 60, 68);
            //this.destino = new Rectangle(0, 0, origem.Width, origem.Height);
            //this.rotacao = 0;// MathHelper.ToRadians(0);
            //this.pivo = Vector2.Zero;// Vector2(destino.Width / 2, destino.Height / 2);//influencia tudo...
            //this.direita = true;
            //this.visivel = true;
            //this.camada = 1.0f;
            //this.alfa = 1f;
            //this.cor = new Color(1.0f, 1.0f, 1.0f, alfa);//not totally ok yet, anda see alpha blend in blendstate...
            //this.colisao = new Rectangle((int)origem.X, (int)origem.Y, origem.Width, origem.Height);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        abstract public void Update(GameTime gameTime);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        protected void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (visible)
            {
                //int frame = (int)(gameTime.TotalGameTime.TotalSeconds * _animacao.quadros_seg) % _animacao.qtd_quadros;

                //spriteBatch.Draw(
                //        textura,
                //        new Rectangle(
                //            (int)posicao.X,
                //            (int)posicao.Y,
                //            _animacao.quadro_X,
                //            _animacao.quadro_Y),
                //        new Rectangle(
                //            frame * _animacao.quadro_X,
                //            _animacao.Y,
                //            _animacao.quadro_X,
                //            _animacao.quadro_Y),
                //        new Color(
                //            1.0f * alfa,
                //            1.0f * alfa,
                //            1.0f * alfa,
                //            alfa),
                //        rotacao,
                //        pivo,
                //        (direita) ? SpriteEffects.None : SpriteEffects.FlipHorizontally,
                //        camada);
            }
        }


    }
}
