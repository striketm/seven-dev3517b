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

//http://www.codeproject.com/Articles/15573/2D-Polygon-Collision-Detection

//http://www.progware.org/Blog/post/XNA-2D-Basic-Collision-Detection.aspx

namespace WindowsGame1
{
    /// <summary>
    /// 
    /// </summary>
    abstract class Sprite//GameObject2D
    {
        /// <summary>
        /// 
        /// </summary>
        GameWindow window;
        /// <summary>
        /// 
        /// </summary>
        public GameWindow Window
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
        public Texture2D Texture
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

        //move to, move by, rotate...

        /// <summary>
        /// 
        /// </summary>
        Vector2 position;
        /// <summary>
        /// 
        /// </summary>
        public Vector2 Position
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

        public float PositionX
        {
            get { return position.X; }
            set { position.X = value; }
        }

        public float PositionY
        {
            get { return position.Y; }
            set { position.Y = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        Vector2 velocity;
        /// <summary>
        /// 
        /// </summary>
        public Vector2 Velocity
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

        // x y ?

        /// <summary>
        /// 
        /// </summary>
        bool visible;
        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        Rectangle frame;//?
        /// <summary>
        /// 
        /// </summary>
        public Rectangle Frame
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
        Rectangle destination;//?
        /// <summary>
        /// 
        /// </summary>
        public Rectangle Destination
        {
            get
            {
                return new Rectangle(
                   (int)Position.X,
                   (int)Position.Y,
                   current_animation.frame.Width,//* SCALE!
                   current_animation.frame.Height);
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
        public Rectangle Collision
        {
            get
            {
                //return collision;//PIVOT!!!
                return new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    current_animation.frame.Width,//*SCALE!!!
                    current_animation.frame.Height);
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
        public Color Color
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

        //R G B? int, float?

        /// <summary>
        /// 
        /// </summary>
        float alpha;
        /// <summary>
        /// 
        /// </summary>
        public float Alpha
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
        public float Rotation
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
        public Vector2 Pivot
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
        public Vector2 Scale
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

        //protected int Scale
        //{
        //    //get
        //    //{
        //    //    return scale;
        //    //}
        //    set
        //    {
        //        scale = new Vector2(value, value);
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        float layer;
        /// <summary>
        /// 
        /// </summary>
        public float Layer
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
        public bool Right
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
        public bool Up
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
        public struct animation
        {
            public int frameWidth;
            public int frameHeight;
            public int frameX;
            public int frameY;

            public Rectangle frame;

            public int frames;
            public int fps;
            
            public string name;
            public bool active;
            public int current_frame;
        }

        /// <summary>
        /// 
        /// </summary>
        public animation current_animation;

        //animation list?
        //animation frame list?

        //list
        //static?

        //window static?
                
        /// <summary>
        /// 
        /// </summary>
        public Sprite(Texture2D texture)
        {
            this.current_animation = new animation();
            this.current_animation.active = true;
            this.current_animation.current_frame = 0;
            this.current_animation.fps = 1;
            this.current_animation.frame = new Rectangle(0, 0, texture.Width, texture.Height);
            this.current_animation.frames = 1;
            this.current_animation.name = "";
            
            this.Alpha = 1.0f;
            this.Color = new Color(1.0f, 1.0f, 1.0f, Alpha);
            this.Position = new Vector2(1.0f, 1.0f);
            
            this.Destination = new Rectangle((int)Position.X, (int)Position.Y, current_animation.frame.Width, current_animation.frame.Height);

            //this.Collision = new Rectangle

            this.Frame = new Rectangle(0, 0, 0, 0);

            this.Layer = 1.0f;

            this.Pivot = new Vector2(0.0f, 0.0f);////

            this.Right = true;

            this.Up = true;

            this.Rotation = 0.0f;

            this.Scale = new Vector2(1.0f, 1.0f);

            this.Texture = texture;

            this.Velocity = new Vector2(5.0f, 5.0f);//x y ?

            this.Visible = true;

            //this.Window = //to do

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
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Visible)
            {

                //int frame = (int)(gameTime.TotalGameTime.TotalSeconds * _animacao.quadros_seg) % _animacao.qtd_quadros;

                spriteBatch.Draw(
                    this.Texture,
                    this.Destination,
                    this.current_animation.frame,
                    Color,
                    Rotation,
                    Pivot,
                    (Right&&Up)?(SpriteEffects.None):
                    ((!Right&&Up)?(SpriteEffects.FlipHorizontally):
                    ((Right&&!Up)?(SpriteEffects.FlipVertically):
                    (SpriteEffects.FlipHorizontally|SpriteEffects.FlipVertically))),
                    Layer);
                    
            }
        }
    }
}
/*
 * (Right&&Up)?(SpriteEffects.None):
                    ((!Right&&Up)?(SpriteEffects.FlipHorizontally):
                    ((Right&&!Up)?(SpriteEffects.FlipVertically):
                    ((!Right&&!Up)?(SpriteEffects.FlipHorizontally|SpriteEffects.FlipVertically):
                    (SpriteEffects.None))))
 */