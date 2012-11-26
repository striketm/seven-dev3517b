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

using WindowsGame1._1_Pong;
using WindowsGame1._2_BreakOut;
using WindowsGame1._3_SpaceInvaders;
using WindowsGame1._4_RType;
using WindowsGame1._5_Quiz;
using WindowsGame1._6_Ship3D;

using WindowsGame1._Screens;//?

namespace WindowsGame1
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        SpriteBatch spriteBatch;

        enum GameState { PONG, BREAKOUT, SPACEINVADERS, RTYPE, QUIZ, SHIP3D, 
        INTRO, MENU, CREDIT, CONFIG, EXIT, HIGHSCORE, PAUSE, GAMEOVER, THEEND}

        GameState presentState = GameState.SHIP3D;//not use = 1

        Pong pong;
        BreakOut breakOut;
        SpaceInvaders spaceInvaders;
        RType rType;
        Quiz quiz;
        Ship3D ship3D;

        string[] menuItems = { "Pong", "Breakout", "Space Invaders", "R-Type", "Quiz", "3D Ship","", "Back to Intro", "Credits", "Configs", "Highscores", "F12 to full screen", "P to pause", "Escape to exit"};

        Menu menu;//dont mess up the component menu with the state menu!²

        Intro intro;
        Credit credit;
        GameScreen activeScreen;//ok?

        //using WindowsGame1._Screens;//?
        PopUp quitScreen;//this should appears in front of the others,
        //and be removed without memory leaks
                
        KeyboardState ks;
        KeyboardState oldks;
        MouseState ms;
        MouseState oldms;
        GamePadState gps;
        GamePadState oldgps;

        public SpriteFont fontArial24;

        //make a fps... not a game, a frame counter... to do 

        //to do bool DEBUG and a trace method with two string parameters using the bool... 
        //trace on the window title? on the console? change the project type?

        static Game1 singleton;//singleton... or just a static Game1 Instance assigned to this...
        public static Game1 Instance { get { return singleton; } }//public access        
        static Game1()//private static constructor to ensure one single point of access
        {
            singleton = new Game1();
            Console.WriteLine("Hello World from console");
        }  
             
        private Game1()
        {
            //Instance = this;

            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();

            IsMouseVisible = true;

            Window.Title = "For the greater good of God...";

            Content.RootDirectory = "Content";

        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //to do refactor

            pong = new Pong(Content);
            breakOut = new BreakOut(Content);
            spaceInvaders = new SpaceInvaders(Content);
            rType = new RType(Content);
            quiz = new Quiz(Content);        
            ship3D = new Ship3D(Content);

            menu = new Menu(this,spriteBatch,Content.Load<SpriteFont>("Arial24"),menuItems);

            fontArial24 = Content.Load<SpriteFont>("Arial24");
            
            //Components.Add(menu);//dont mess up the component menu with the state menu!

            intro = new Intro(this, spriteBatch);
            //Components.Add(intro);//auto?
            //intro.Hide();//auto?

            credit = new Credit(this, spriteBatch);
            //Components.Add(credit);//auto?
            //credit.Hide();//auto?

            quitScreen = new PopUp(this,spriteBatch,Content.Load<SpriteFont>("Arial24"),
Content.Load<Texture2D>("quitscreen"));
            //Components.Add(quitScreen);
            //quitScreen.Hide();

            activeScreen = intro;
            //activeScreen.Show();

        }

        protected override void UnloadContent()
        {
           
        }

        public bool keyboardIsPressing(Keys key)
        {
            return ks.IsKeyDown(key);
        }

        public bool keyboardWasPressed(Keys key)
        {
            return ks.IsKeyDown(key) && !oldks.IsKeyDown(key);
        }

        public bool keyboardWasReleased(Keys key)
        {
            return ks.IsKeyUp(key) && !oldks.IsKeyUp(key);
        }

        private bool CheckButton(Buttons button)
        {
            return gps.IsButtonUp(button) && oldgps.IsButtonDown(button);
        }
        private void HandleStartScreen()//put in the class!
        {
            if (keyboardWasPressed(Keys.Enter))// || CheckButton(Buttons.A))
            {
                if (intro.SelectedIndex == 0)
                {
                    activeScreen.Hide();
                    activeScreen = credit;
                    activeScreen.Show();
                }
                //if (intro.SelectedIndex == 1)
                //{
                //    this.Exit();
                //}
            }
        }

        private void HandleQuitScreen()//put in the class!
        {
            if (keyboardWasPressed(Keys.Enter))// || CheckButton(Buttons.A))
            {
                if (quitScreen.SelectedIndex == 0)
                {
                    activeScreen.Hide();
                    activeScreen = intro;
                    activeScreen.Show();
                }
                if (quitScreen.SelectedIndex == 1)
                {
                    activeScreen.Hide();
                    activeScreen = credit;//?
                    activeScreen.Show();
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            //Console.WriteLine("fuu!");

            oldks = ks;
            oldms = ms;
            oldgps = gps;
            ks = Keyboard.GetState();
            ms = Mouse.GetState();
            gps = GamePad.GetState(PlayerIndex.One);

            if (keyboardWasPressed(Keys.Escape))
            {
                Exit();//
                activeScreen.Hide();
                activeScreen = quitScreen;
                activeScreen.Show();
            }

            if (keyboardWasPressed(Keys.F12))
            {
                graphics.ToggleFullScreen();
            }

            //i did not call the game screens updates and draws...
            //did have to remember to add them to game components, since game 1 is singleton...

            //if (activeScreen == intro)//make this inside the class?
            //    //to do so the screens should be available global, manager...
            //{
            //    if (keyboardWasPressed(Keys.Enter))
            //    {
            //        if (intro.SelectedIndex == 0)
            //        {
            //            activeScreen.Hide();
            //            activeScreen = credit;
            //            activeScreen.Show();
            //        }
                    
            //    }
            //}

            if (activeScreen == intro)
            {
                HandleStartScreen();
            }
            else if (activeScreen == quitScreen)
            {
                HandleQuitScreen();
            }

            switch (presentState)
            {
                case GameState.PONG:

                    pong.Update(gameTime);

                    break;

                case GameState.BREAKOUT:

                    breakOut.Update(gameTime);

                    break;

                case GameState.SPACEINVADERS:

                    spaceInvaders.Update(gameTime);

                    break;

                case GameState.RTYPE:

                    rType.Update(gameTime);

                    break;

                case GameState.QUIZ:

                    quiz.Update(gameTime);

                    break;

                case GameState.SHIP3D:

                    ship3D.Update(gameTime);

                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            switch (presentState)
            {
                case GameState.PONG:

                    pong.Draw(gameTime, spriteBatch);

                    break;

                case GameState.BREAKOUT:

                    breakOut.Draw(gameTime, spriteBatch);

                    break;

                case GameState.SPACEINVADERS:

                    spaceInvaders.Draw(gameTime, spriteBatch);

                    break;

                case GameState.RTYPE:

                    rType.Draw(gameTime, spriteBatch);

                    break;

                case GameState.QUIZ:

                    quiz.Draw(gameTime, spriteBatch);

                    break;

                case GameState.SHIP3D:

                    ship3D.Draw(gameTime, spriteBatch);

                    break;
            }
            
            //Leave the base.Draw inside the spriteBatch Begin End for the calls to the components

            base.Draw(gameTime);

            spriteBatch.End();
        }
    }
}

