using System;
using System.Collections.Generic;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

using Xna.Auxiliary.Components;
using Xna.Auxiliary.Collision;
using Xna.Auxiliary.Draws;


namespace Snake
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {

        private enum GameSate
        {
            Menu,
            Loop,
            Credit,
            GameOver,
            Win,
            Exit
        }

        private enum Course
        {
            Left,
            Up,
            Right,
            Down
        }


        const int GridCount = 20;
        const int SpriteSize = 16;
        const int MaxSnakeLen = 20;
        const int MaxHeart = 5;
        

        GraphicsDeviceManager graphics;
        GameSate gamesate = GameSate.Menu;
        Cursor cursor;
        Course LastCourse = Course.Right;
        Course Course1 = Course.Right;
        Course Course2 = Course.Right;
        Point Food = new Point();
        Point Bomb = new Point();
        Random rnd = new Random();
        ArrayList Snake = new ArrayList();
        SpriteBatch spriteBatch;
        KeyboardState kstate;
        KeyboardState last_kstate;
        MouseState mstate;
        Vector3 backcolor;
        Vector2 ScenePos = new Vector2(113, 58);
        PostProcessing post;
        TextureNumber number_font;
        SpriteFont tahoma;

        bool Resume_Visible = false;
        int LastTime = 0;
        int LowSpeed = 130;
        int HighSpeed = 50;
        int RunSpeed;
        int Heart = 5;
        int Level = 1;
        bool Start = false;

        Texture2D btn_newgame;
        Texture2D btn_credit;
        Texture2D btn_exit;
        Texture2D btn_resume;
        Texture2D tex_background;
        Texture2D tex_bomb;
        Texture2D tex_food;
        Texture2D tex_snake;
        Texture2D tex_gameover;
        Texture2D tex_youwin;
        Texture2D tex_credit;

     
        public Game() 
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 480;
            graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
        }

        private bool IsKeyPress(Keys key) 
        {

            if (last_kstate.IsKeyDown(key) && kstate.IsKeyUp(key)) return true;

            return false;

        }

        private void Init_Snake() 
        {

            Start = false;

            Course1 = Course.Right;
            Course2 = Course.Right;

            Point node = new Point();
            Snake.Clear();
            node.X = 5;
            node.Y = 5;
            Snake.Add(node);
            node.X = 6;
            node.Y = 5;
            Snake.Add(node);

            NextFood();
            NextBomb();

        }

        private void NextFood() 
        {

            Point node = new Point();
            bool find;

            while (true)
            {

                find = false;

                Food.X = rnd.Next(GridCount);
                Food.Y = rnd.Next(GridCount);

                for (int i = 0; i < Snake.Count; i++)
                {

                    node = (Point)Snake[i];
                    if (node.X == Food.X && node.Y == Food.Y)
                    {
                        find = true;
                        break;
                    }

                }

                if (Bomb == Food)
                {
                    find = true;
                }

                if (find == false) return;

            }

        }

        private void NextBomb() 
        {

            Point node = new Point();
            bool find;

            while (true)
            {

                find = false;

                Bomb.X = rnd.Next(GridCount);
                Bomb.Y = rnd.Next(GridCount);

                for (int i = 0; i < Snake.Count; i++)
                {

                    node = (Point)Snake[i];
                    if (node.X == Bomb.X && node.Y == Bomb.Y)
                    {
                        find = true;
                        break;
                    }

                }

                if (Bomb == Food)
                {
                    find = true;
                }

                if (find == false) return;

            }

        }

        private void DrawSnake() 
        {

            Point node = new Point();

            spriteBatch.Begin();
            for (int i = 0; i < Snake.Count; i++)
            {

                node = (Point)Snake[i];
                spriteBatch.Draw(tex_snake, ScenePos + new Vector2(node.X * SpriteSize, node.Y * SpriteSize), Color.White);

            }
            spriteBatch.End();

        }

        private void RunSnake() 
        {

            int CurrentTime = Environment.TickCount;

            if (CurrentTime - LastTime > RunSpeed)
            {

                LastTime = CurrentTime;

                Point LastNode = (Point)Snake[Snake.Count - 1];
                Point NewNode = LastNode;


                if (Course2 == Course.Down && LastCourse == Course.Up) Course2 = Course1;
                if (Course2 == Course.Up && LastCourse == Course.Down) Course2 = Course1;
                if (Course2 == Course.Left && LastCourse == Course.Right) Course2 = Course1;
                if (Course2 == Course.Right && LastCourse == Course.Left) Course2 = Course1;


                if (Course2 == Course.Right)
                    NewNode.X++;


                if (Course2 == Course.Left)
                    NewNode.X--;


                if (Course2 == Course.Down)
                    NewNode.Y++;


                if (Course2 == Course.Up)
                    NewNode.Y--;


                Snake.Add(NewNode);

                if (LastNode.X == Bomb.X && LastNode.Y == Bomb.Y)
                {

                    Init_Snake();
                    return;

                }

                if (LastNode.X == Food.X && LastNode.Y == Food.Y)
                {

                    NextFood();

                }
                else
                {
                    Snake.RemoveAt(0);
                }


                LastCourse = Course2;

            }
        }

        private bool CheckFailed() 
        {

            Point node = (Point)Snake[Snake.Count - 1];
            Point current = new Point();

            if (node.X > GridCount - 1 || node.Y > GridCount - 1 || node.X == -1 || node.Y == -1) return true;

            if (node == Bomb) return true;

            for (int i = 0; i < Snake.Count - 1; i++)
            {
                current = (Point)Snake[i];
                if (current.X == node.X && current.Y == node.Y)
                    return true;
            }

            return false;

        }

        private void GameState_Menu() 
        {

            graphics.GraphicsDevice.Clear(new Color(backcolor));

            Color color = Color.Pink;
            Vector4 vec = color.ToVector4();
            vec.W = (float)Math.Abs(Math.Sin(Environment.TickCount / 500.0f));
            color = new Color(vec);


            cursor.Visible = true;
            cursor.Blink = true;


            spriteBatch.Begin();


            //  New Game - Collision
            if (PixelCollision.Vector2(cursor.Position, btn_newgame, new Rectangle(200, 100, 200, 50), 128))
            {
                spriteBatch.Draw(btn_newgame, new Vector2(200, 100), color);
                if (mstate.LeftButton == ButtonState.Pressed)
                {
                    Level = 1;
                    Heart = MaxHeart;
                    Init_Snake();
                    NextFood();
                    gamesate = GameSate.Loop;
                    Resume_Visible = true;
                    post.Start_Level = true;
                }
                cursor.Blink = false;
                
            }
            else
                spriteBatch.Draw(btn_newgame, new Vector2(200, 100), Color.White);



            //  Credit - Collision
            if (PixelCollision.Vector2(cursor.Position, btn_credit, new Rectangle(200, 160, 200, 50), 128))
            {
                spriteBatch.Draw(btn_credit, new Vector2(200, 160), color);
                if (mstate.LeftButton == ButtonState.Pressed) gamesate = GameSate.Credit;
                cursor.Blink = false;
            }
            else
                spriteBatch.Draw(btn_credit, new Vector2(200, 160), Color.White);



            //  Exit - Collision
            if (PixelCollision.Vector2(cursor.Position, btn_exit, new Rectangle(200, 220, 200, 50), 128))
            {
                spriteBatch.Draw(btn_exit, new Vector2(200, 220), color);
                if (mstate.LeftButton == ButtonState.Pressed) gamesate = GameSate.Exit;
                cursor.Blink = false;
            }
            else
                spriteBatch.Draw(btn_exit, new Vector2(200, 220), Color.White);


            if (Resume_Visible)
            {

                //  Resume - Collision
                if (PixelCollision.Vector2(cursor.Position, btn_resume, new Rectangle(200, 280, 200, 50), 128))
                {
                    spriteBatch.Draw(btn_resume, new Vector2(200, 280), color);
                    if (mstate.LeftButton == ButtonState.Pressed)
                        gamesate = GameSate.Loop;
                    
                    cursor.Blink = false;
                }
                else
                    spriteBatch.Draw(btn_resume, new Vector2(200, 280), Color.White);

            }


            spriteBatch.End();

        }

        private void GameState_Exit() 
        {

            this.Exit();

        }

        private void GameState_Loop() 
        {

            // Clear screen
            graphics.GraphicsDevice.Clear(Color.Red);

            // Hide cursor
            cursor.Visible = false;

            // Calc snake run speed
            LowSpeed = 130 - (14 * (Level - 1));

            // Run snake
            if (Start) RunSnake();

            // Check failed
            if (CheckFailed())
            {
                post.Start_Bomb = true;

                Heart--;

                if (Heart == 0)
                {
                    gamesate = GameSate.GameOver;
                    return;
                }

                Init_Snake();

                return;
            }

            // Next level
            if (Snake.Count == MaxSnakeLen)
            {
                if (Level == 9)
                {
                    gamesate = GameSate.Win;
                    return;
                }

                Init_Snake();

                post.Start_Level = true;

                Level++;
            }

            // Draw sprites
            spriteBatch.Begin();
            spriteBatch.Draw(tex_background, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(tex_food, ScenePos + new Vector2(Food.X * SpriteSize, Food.Y * SpriteSize), Color.White);
            spriteBatch.Draw(tex_bomb, ScenePos + new Vector2(Bomb.X * SpriteSize, Bomb.Y * SpriteSize), Color.White);
            spriteBatch.End();

            // Draw snake
            DrawSnake();

           
            // Draw numbers
            number_font.Draw(Level, 525, 106);
            number_font.Draw(Heart, 525, 312);

        }

        private void GameState_GameOver() 
        {

            graphics.GraphicsDevice.Clear(new Color(backcolor));

            cursor.Visible = false;


            spriteBatch.Begin();
            spriteBatch.Draw(tex_gameover, new Vector2(190, 200), Color.White);
            spriteBatch.End();

        }

        private void GameState_YouWin() 
        {

            graphics.GraphicsDevice.Clear(new Color(backcolor));

            cursor.Visible = false;


            spriteBatch.Begin();
            spriteBatch.Draw(tex_youwin, new Vector2(180, 200), Color.White);
            spriteBatch.End();

        }

        private void GameState_Credit() 
        {

            graphics.GraphicsDevice.Clear(new Color(backcolor));

            cursor.Visible = false;


            spriteBatch.Begin();
            spriteBatch.Draw(tex_credit, new Vector2(0, 0), Color.White);
            spriteBatch.End();

        }

        protected override void Initialize() 
        {

            // PostProcessing
            post = new PostProcessing(this);
            this.Components.Add(post);

            // MousePointer
            cursor = new Cursor(this);
            cursor.Blink = true;
            cursor.BlinkSpeed = 1.0f;
            cursor.color = Color.Red;
            cursor.DrawOrder = 1000;
            cursor.Texture = Content.Load<Texture2D>("arrow");
            cursor.RestrictZone = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            this.Components.Add(cursor);

            // Define number font
            number_font = new TextureNumber(this,
                                            Content.Load<Texture2D>(@"Fonts\Number_Font_Impact"),
                                            32, 32);
            number_font.color = Color.Red;
            
            // Define Background Color
            backcolor = new Vector3(0.05f, 0.05f, 0.2f);

            // Snake
            Init_Snake();


            base.Initialize();
        }

        protected override void LoadContent() 
        {

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load buttons
            btn_newgame = Content.Load<Texture2D>(@"Buttons\new game");
            btn_credit = Content.Load<Texture2D>(@"Buttons\credit");
            btn_exit = Content.Load<Texture2D>(@"Buttons\exit");
            btn_resume = Content.Load<Texture2D>(@"Buttons\resume");

            // Load textures
            tex_background = Content.Load<Texture2D>("background");
            tex_bomb = Content.Load<Texture2D>("bomb");
            tex_food = Content.Load<Texture2D>("food");
            tex_snake = Content.Load<Texture2D>("snake");
            tex_gameover = Content.Load<Texture2D>("game over");
            tex_youwin = Content.Load<Texture2D>("you win");
            tex_credit = Content.Load<Texture2D>("Credit");

            // Load Font
            tahoma = Content.Load<SpriteFont>(@"Fonts\Tahoma");

        }

        protected override void UnloadContent() 
        {
        }

        protected override void Update(GameTime gameTime) 
        {


            //  Print Screen
            if (kstate.IsKeyDown(Keys.Enter))
            {

                ResolveTexture2D screen;

                PresentationParameters pp = GraphicsDevice.PresentationParameters;

                int width = pp.BackBufferWidth;
                int height = pp.BackBufferHeight;

                SurfaceFormat format = pp.BackBufferFormat;

                screen = new ResolveTexture2D(GraphicsDevice, width, height, 1, format);

                GraphicsDevice.ResolveBackBuffer(screen);
                screen.Save("Screen.png", ImageFileFormat.Png);

            }


            if (post.Start_Bomb == true || post.Start_Level == true) return;

            // Update Input Devices
            kstate = Keyboard.GetState();
            mstate = Mouse.GetState();


            // Change Game State
            if (gamesate == GameSate.Loop && IsKeyPress(Keys.Escape)) gamesate = GameSate.Menu;


            // Toggle full screen
            if (IsKeyPress(Keys.F1)) graphics.ToggleFullScreen();


            // Game Over - key codes
            if ((IsKeyPress(Keys.Space) || IsKeyPress(Keys.Escape) || IsKeyPress(Keys.Enter)))
            {
                if (gamesate == GameSate.GameOver)
                {
                    Resume_Visible = false;
                    gamesate = GameSate.Menu;
                }
            }

            // You Win - key codes
            if ((IsKeyPress(Keys.Space) || IsKeyPress(Keys.Escape) || IsKeyPress(Keys.Enter)))
            {
                if (gamesate == GameSate.Win)
                {
                    Resume_Visible = false;
                    gamesate = GameSate.Menu;
                }
            }

            // Credit - key codes
            if ((IsKeyPress(Keys.Space) || IsKeyPress(Keys.Escape) || IsKeyPress(Keys.Enter)))
            {
                if (gamesate == GameSate.Credit)
                {
                    gamesate = GameSate.Menu;
                }
            }


            // Space Key Codes
            if (kstate.IsKeyDown(Keys.Space))
            {
                if (HighSpeed < LowSpeed)
                    RunSpeed = HighSpeed;

                Start = true;
            }
            else
                RunSpeed = LowSpeed;


            //  Right Arrow Key Codes
            if (kstate.IsKeyDown(Keys.Right))
            {

                Start = true;

                if (Course2 == Course.Up || Course2 == Course.Down)
                {
                    Course1 = Course2;
                    Course2 = Course.Right;
                }

            }


            //  Left Arrow Key Codes
            if (kstate.IsKeyDown(Keys.Left))
            {

                Start = true;

                if (Course2 == Course.Up || Course2 == Course.Down)
                {
                    Course1 = Course2;
                    Course2 = Course.Left;
                }

            }


            //  Up Arrow Key Codes
            if (kstate.IsKeyDown(Keys.Up))
            {

                Start = true;

                if (Course2 == Course.Right || Course2 == Course.Left)
                {
                    Course1 = Course2;
                    Course2 = Course.Up;
                }

            }


            //  Down Arrow Key Codes
            if (kstate.IsKeyDown(Keys.Down))
            {

                Start = true;

                if (Course2 == Course.Right || Course2 == Course.Left)
                {
                    Course1 = Course2;
                    Course2 = Course.Down;
                }

            }


            // Store last keyboard state
            last_kstate = kstate;


            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime) 
        {

            if (gamesate == GameSate.Menu)
            {
                GameState_Menu();
            }

            if (gamesate == GameSate.Loop)
            {
                GameState_Loop();
            }

            if (gamesate == GameSate.Exit)
            {
                GameState_Exit();
            }
            
            if (gamesate == GameSate.GameOver)
            {
                GameState_GameOver();
            }
            
            if (gamesate == GameSate.Win)
            {
                GameState_YouWin();
            }

            if (gamesate == GameSate.Credit)
            {
                GameState_Credit();
            }


            base.Draw(gameTime);
        }

        static void Main(string[] args) 
        {
            using (Game game = new Game())
            {
                game.Run();
            }
        }

    }
}