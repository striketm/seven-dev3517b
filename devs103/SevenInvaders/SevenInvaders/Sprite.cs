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

//http://xbox.create.msdn.com/en-US/education/catalog/tutorial/collision_2d_rectangle

namespace SevenInvaders
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

        public static List<Sprite> list = new List<Sprite>();

        //window static?
                
        /// <summary>
        /// 
        /// </summary>
        public Sprite(Texture2D texture)
        {
            this.current_animation = new animation();
            
            this.current_animation.fps = 1;
            this.current_animation.frame = new Rectangle(0, 0, texture.Width, texture.Height);
            this.current_animation.frames = 1;

            this.current_animation.active = true;
            this.current_animation.current_frame = 1;
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

            list.Add(this);

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
                current_animation.current_frame = (int)(gameTime.TotalGameTime.TotalSeconds * current_animation.fps) % current_animation.frames;

                Rectangle tmp =new Rectangle(current_animation.frame.Width * current_animation.current_frame, current_animation.frame.Y, current_animation.frame.Width, current_animation.frame.Height);

                spriteBatch.Draw(
                    this.Texture,
                    this.Destination,
                    tmp,
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
 * update
 * 
 *  if (!IsActive)
            return;

        ElapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (ElapsedTime > TimePerFrame)
        {
            CurrentFrame += 1;//move frame ahead one
            if (CurrentFrame >= FrameCount)
            {
                CurrentFrame = 0; //reset the animation;
                if (!IsLooping)
                    IsActive = false;
            }

            ElapsedTime -= TimePerFrame;
        }
 * 
 * _________
 * 
 * Texture2D character;
02	Vector2 Position = new Vector2(200,200);
03	Point frameSize = new Point(105,130);
04	Point currentFrame = new Point (0,2);
05	Point sheetSize = new Point(12,35);
06	float speed = 15;
07	 
08	KeyboardState currentState;
09	KeyboardState theKeyboardState;
10	KeyboardState oldKeyboardState;
11	 
12	enum State {
13	 
14	 Walking,
15	 Punch,
16	 Jump,
17	 JumpForward,
18	 JumpBackgwards
19	 
20	 
21	}
22	 
23	State mCurrentState = State.Walking;
24	 
25	TimeSpan nextFrameInterval = TimeSpan.FromSeconds((float)1/ 16);
26	TimeSpan nextFrame;
 * 
 * _______
 * 
 * Texture2D spriteTexture;
02	float timer = 0f;
03	float interval = 200f;
04	int currentFrame = 0;
05	int spriteWidth = 32;
06	int spriteHeight = 48;
07	int spriteSpeed = 2;
08	Rectangle sourceRect;
09	Vector2 position;
10	Vector2 origin;
 * 
 * ________
 * 
 * public void AnimateRight(GameTime gameTime)
02	{
03	    if (currentKBState != previousKBState)
04	    {
05	        currentFrame = 9;
06	    }
07	 
08	    timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
09	 
10	    if (timer > interval)
11	    {
12	        currentFrame++;
13	         
14	        if (currentFrame > 11)
15	        {
16	            currentFrame = 8;
17	        }
18	        timer = 0f;
19	    }
20	}
 * 
 *  public void AnimateDown(GameTime gameTime)
23	        {
24	            if (currentKBState != previousKBState)
25	            {
26	                currentFrame = 1;
27	            }
28	 
29	            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
30	 
31	            if (timer > interval)
32	            {
33	                currentFrame++;
34	 
35	                if (currentFrame > 3)
36	                {
37	                    currentFrame = 0;
38	                }
39	                timer = 0f;
40	            }
41	        }
 * 
 * if (gameState == GameStates.Playing)
2	{      
3	spriteBatch.Begin();
4	spriteBatch.Draw(redblinkScreen,
5	gameOverLocation, new Color(255, 255, 255, transparencyVal));
6	 
7	spriteBatch.End();                   
8	}
 * 
 * 
public float PlayerJump()
{
    PlayerHeight += Player_Jump + Player_Gravity;
    if (PlayerHeight > PlayerMaxHeight)
    {
        PlayerHeight = PlayerMaxHeight;
    }
    return PlayerHeight;
}
 * 
 * Define variables:

float bleedOff = 1.0f;
bool jumping = false;

Input update:

if(input.JumpKey())
{
    jumping = true;
}

Jumping update:

if(jumping)
{
    //Modify our y value based on a bleedoff
    //Eventually this value will be minus so we will start falling.
    position.Y += bleedOff;
    bleedOff -= 0.03f;

    //We should probably stop falling at some point, preferably when we reach the ground.
    if(position.Y <= ground.Y)
    {
        jumping = false;
    }
}

bleedOff = MathHelper.Clamp(bleedOff, -1f, 1f);
 * 
 */