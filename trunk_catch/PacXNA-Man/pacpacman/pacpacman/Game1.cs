#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
#endregion

namespace pacpacman
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        clsSprite mySprite;
        clsSprite wall;
        clsSprite pac;
        SpriteBatch spriteBatch;
        Texture2D ghosts;
        byte[,] map;
        SpriteFont font;

        int nextGridPosX;
        int nextGridPosY;
        int nextGridPosXg;
        int nextGridPosYg;
        string direction;
        protected Random random;
        public List<Rectangle> new_beanslist = new List<Rectangle>();
        public Vector2 ghostpos = new Vector2(415, 352);
        public Vector2 ghostposlast = new Vector2(415, 352);
        public Sound soundone;


        private int scoreis = 0;

        protected Texture2D bean;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            random = new Random(GetHashCode());
            soundone = new Sound(this);

            map = new byte[31, 28]{
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 2, 2, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 2, 2, 2, 2, 2, 2, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 2, 2, 2, 2, 2, 2, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 2, 2, 2, 2, 2, 2, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 0},
            {0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0},
            {0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0},
            {0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 0},
            {0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},

        };
        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        public void draw_bean()
        {

            for (int i = 0; i < 31; i++)
            {
                for (int j = 0; j < 28; j++)
                {
                    Rectangle imageRect;
                    if (map[i, j] == 1)
                    {
                        imageRect = new Rectangle(j * 32 + 16,
                                                    i * 32 + 16,
                                                    bean.Width,
                                                    bean.Height);
                        new_beanslist.Add(imageRect);
                    }
                }
            }
            new_beanslist.Remove(new Rectangle(432,
                                                752,
                                                bean.Width,
                                                bean.Height
                                               ));


        }


        protected Point frame;
        private int count;
        protected TimeSpan elapsedTime = TimeSpan.Zero;
        protected Rectangle currentFrame;
        protected long frameDelay = 100;

        public void picframe(GameTime gameTime)
        {

            frame = new Point(10, 0);


            elapsedTime += gameTime.ElapsedGameTime;

            // it's time to a next frame?
            if (elapsedTime > TimeSpan.FromMilliseconds(frameDelay))
            {
                elapsedTime -= TimeSpan.FromMilliseconds(frameDelay);
                if (direction == "Right")
                {

                    currentFrame = new Rectangle((frame.X + count) * 32,
                                                 frame.Y,
                                                 32,
                                                 32);
                    count++;
                }

                else if (direction == "Left")
                {

                    currentFrame = new Rectangle((frame.X + count) * 32,
                                                 (frame.Y + 2) * 32,
                                                 32,
                                                 32);
                    count++;
                }
                else if (direction == "Down")
                {

                    currentFrame = new Rectangle((frame.X + count) * 32,
                                                 (frame.Y + 1) * 32,
                                                 32,
                                                 32);
                    count++;
                }
                else if (direction == "Up")
                {

                    currentFrame = new Rectangle((frame.X + count) * 32,
                                                 (frame.Y + 3) * 32,
                                                 32,
                                                 32);
                    count++;
                }
                else
                {
                    if (currentFrame == Rectangle.Empty)
                    {
                        currentFrame = new Rectangle(frame.X * 32,
                                                     frame.Y * 32,
                                                     32,
                                                     32);
                    }
                }

                if (count >= 2)
                {
                    count = 0;
                }
            }
        }

        protected Point frameg;
        private int countg;
        protected TimeSpan elapsedTimeg = TimeSpan.Zero;
        protected Rectangle currentFrameg;


        public void ghostframe(GameTime gameTime)
        {

            frameg = new Point(0, 0);
            elapsedTimeg += gameTime.ElapsedGameTime;

            // it's time to a next frame?
            if (elapsedTimeg > TimeSpan.FromMilliseconds(frameDelay))
            {
                elapsedTimeg -= TimeSpan.FromMilliseconds(frameDelay);
                if (ghostdir == "right")
                {

                    currentFrameg = new Rectangle((frameg.X + countg) * 32,
                                                 frameg.Y,
                                                 32,
                                                 32);
                    countg++;
                }

                else if (ghostdir == "left")
                {

                    currentFrameg = new Rectangle((frameg.X + countg) * 32,
                                                 (frameg.Y + 2) * 32,
                                                 32,
                                                 32);
                    countg++;
                }
                else if (ghostdir == "down")
                {

                    currentFrameg = new Rectangle((frameg.X + countg) * 32,
                                                 (frameg.Y + 1) * 32,
                                                 32,
                                                 32);
                    countg++;
                }
                else if (ghostdir == "up")
                {

                    currentFrameg = new Rectangle((frameg.X + countg) * 32,
                                                 (frameg.Y + 3) * 32,
                                                 32,
                                                 32);
                    countg++;
                }
                else
                {
                    if (currentFrameg == Rectangle.Empty)
                    {
                        currentFrameg = new Rectangle(frameg.X * 32,
                                                     frameg.Y * 32,
                                                     32,
                                                     32);
                    }
                }

                if (countg >= 2)
                {
                    countg = 0;
                }
            }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //  changing the back buffer size changes the window size (when in windowed mode)
            graphics.PreferredBackBufferWidth = 1300;
            graphics.PreferredBackBufferHeight = 1000;
            graphics.ApplyChanges();

            font = Content.Load<SpriteFont>("Arial");

            // Load a 2D texture sprite
            ghosts = Content.Load<Texture2D>("GhostSprites");
            wall = new clsSprite(Content.Load<Texture2D>("wall"), new Vector2(0f, 0f), new Vector2(32f, 32f));
            mySprite = new clsSprite(Content.Load<Texture2D>("xna_thumbnail"), new Vector2(0f, 0f), new Vector2(64f, 64f));
            pac = new clsSprite(Content.Load<Texture2D>("pacbloc"), new Vector2(416f, 736f), new Vector2(32f, 32f));
            bean = Content.Load<Texture2D>("bean");

            draw_bean();


            soundone.begin();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            //  Free the previously alocated resources
            mySprite.texture.Dispose();
            ghosts.Dispose();
            spriteBatch.Dispose();
        }


        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Right))
            {
                direction = "Right";
                //Check if possible to go there.
                int nextPosX = (int)this.pac.position.X + 1;
                int nextPosY = (int)this.pac.position.Y;

                nextGridPosX = (nextPosX + 31) / 32;
                nextGridPosY = nextPosY / 32;

                if (map[nextGridPosY, nextGridPosX] == 1 && nextPosY % 32 == 0) //We do %32 to make sure we are completly within the square
                {
                    this.pac.position.X++;
                }
            }

            if (keyboard.IsKeyDown(Keys.Left))
            {
                direction = "Left";
                //Check if possible to go there.
                int nextPosX = (int)this.pac.position.X - 1;
                int nextPosY = (int)this.pac.position.Y;

                nextGridPosX = (nextPosX) / 32;
                nextGridPosY = nextPosY / 32;

                if (map[nextGridPosY, nextGridPosX] == 1 && nextPosY % 32 == 0) //We do %32 to make sure we are completly within the square
                {
                    this.pac.position.X--;
                }
            }

            if (keyboard.IsKeyDown(Keys.Down))
            {
                direction = "Down";
                //Check if possible to go there.
                int nextPosX = (int)this.pac.position.X;
                int nextPosY = (int)this.pac.position.Y + 1;

                nextGridPosX = (nextPosX) / 32;
                nextGridPosY = (nextPosY + 31) / 32;

                if (map[nextGridPosY, nextGridPosX] == 1 && nextPosX % 32 == 0) //We do %32 to make sure we are completly within the square
                {
                    this.pac.position.Y++;
                }
            }

            if (keyboard.IsKeyDown(Keys.Up))
            {
                direction = "Up";
                //Check if possible to go there.
                int nextPosX = (int)this.pac.position.X;
                int nextPosY = (int)this.pac.position.Y - 1;

                nextGridPosX = nextPosX / 32;
                nextGridPosY = (nextPosY) / 32;

                if (map[nextGridPosY, nextGridPosX] == 1 && nextPosX % 32 == 0) //We do %32 to make sure we are completly within the square
                {
                    this.pac.position.Y--;
                }
            }
            beancoll();
            ghostcoll();
            picframe(gameTime);
            ghostai(gameTime);

            base.Update(gameTime);
        }

        public void beancoll()
        {
            foreach (Rectangle r in new_beanslist)
            {

                if (r.Intersects(new Rectangle((int)pac.position.X,
                                               (int)pac.position.Y,
                                                32,
                                                32)))
                {
                    new_beanslist.Remove(r);
                    scoreis++;
                    soundone.eat();
                    break;
                }
            }
        }

        public void ghostcoll()
        {
            if (new Rectangle((int)ghostpos.X,
                               (int)ghostpos.Y,
                               32,
                               32)

                .Intersects(new Rectangle((int)pac.position.X,
                                             (int)pac.position.Y,
                                              32,
                                              32)))
            {
                reset();
            }
        }

        public void reset()
        {
            ghostpos = new Vector2(415, 352);
            pac.position = new Vector2(416, 736);
            soundone.dead();

        }

        public void ghostai(GameTime gameTime)
        {
            if (ghostpos == ghostposlast)
            {
                int r = random.Next(4);
                if (r == 1)
                {
                    ghostdir = "right";
                }
                else if (r == 2)
                {
                    ghostdir = "left";
                }
                else if (r == 3)
                {
                    ghostdir = "down";
                }
                else if (r == 4)
                {
                    ghostdir = "up";
                }


            }
            ghostposlast = ghostpos;
            ghostmove(gameTime);

        }

        public string ghostdir;


        public void ghostmove(GameTime gameTime)
        {
            if (ghostdir == "right")
            {

                //Check if possible to go there.
                int nextPosX = (int)ghostpos.X + 1;
                int nextPosY = (int)ghostpos.Y;

                nextGridPosXg = (nextPosX + 31) / 32;
                nextGridPosYg = nextPosY / 32;

                if (map[nextGridPosYg, nextGridPosXg] == 1 && nextPosY % 32 == 0) //We do %32 to make sure we are completly within the square
                {
                    ghostpos.X++;
                }
            }

            if (ghostdir == "left")
            {

                //Check if possible to go there.
                int nextPosX = (int)ghostpos.X - 1;
                int nextPosY = (int)ghostpos.Y;

                nextGridPosXg = (nextPosX) / 32;
                nextGridPosYg = nextPosY / 32;

                if (map[nextGridPosYg, nextGridPosXg] == 1 && nextPosY % 32 == 0) //We do %32 to make sure we are completly within the square
                {
                    ghostpos.X--;
                }
            }

            if (ghostdir == "down")
            {

                //Check if possible to go there.
                int nextPosX = (int)ghostpos.X;
                int nextPosY = (int)ghostpos.Y + 1;

                nextGridPosXg = (nextPosX) / 32;
                nextGridPosYg = (nextPosY + 31) / 32;

                if (map[nextGridPosYg, nextGridPosXg] == 1 && nextPosX % 32 == 0) //We do %32 to make sure we are completly within the square
                {
                    ghostpos.Y++;
                }
            }

            if (ghostdir == "up")
            {

                //Check if possible to go there.
                int nextPosX = (int)ghostpos.X;
                int nextPosY = (int)ghostpos.Y - 1;

                nextGridPosXg = nextPosX / 32;
                nextGridPosYg = (nextPosY) / 32;

                if (map[nextGridPosYg, nextGridPosXg] == 1 && nextPosX % 32 == 0) //We do %32 to make sure we are completly within the square
                {
                    ghostpos.Y--;
                }
            }

            ghostframe(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            // Draw the sprite using Alpha Blend, which uses transparency information if avaliable 
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);

            spriteBatch.DrawString(font, "pac.X: " + this.pac.position.X, new Vector2(900, 400), Color.White);
            spriteBatch.DrawString(font, "pac.Y: " + this.pac.position.Y, new Vector2(1100, 400), Color.White);

            spriteBatch.DrawString(font, "pac.Next.X: " + (this.pac.position.X + 1), new Vector2(950, 440), Color.White);

            spriteBatch.DrawString(font, "pac.nextgrid.x: " + nextGridPosX, new Vector2(900, 480), Color.White);
            spriteBatch.DrawString(font, "pac.nextgrid.y: " + nextGridPosY, new Vector2(1100, 480), Color.White);

            spriteBatch.DrawString(font, "pac.direction: " + direction, new Vector2(900, 520), Color.White);

            spriteBatch.DrawString(font, "Score: " + scoreis, new Vector2(900, 0), Color.White);



            for (int x = 0; x < 31; x++)
            {
                for (int y = 0; y < 28; y++)
                {
                    if (map[x, y] == 0)
                    {
                        int xpos, ypos;
                        xpos = x * 32;
                        ypos = y * 32;
                        Vector2 pos = new Vector2(ypos, xpos);
                        spriteBatch.Draw(wall.texture, pos, Color.White);
                    }
                }
            }

            foreach (Rectangle img in new_beanslist)
            {

                spriteBatch.Draw(bean, img, Color.White);
            }

            spriteBatch.Draw(ghosts, pac.position, currentFrame, Color.White);

            spriteBatch.Draw(ghosts, ghostpos, currentFrameg, Color.White);

            System.Diagnostics.Debug.WriteLine("posx: " + this.pac.position.X);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}


