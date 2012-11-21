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

namespace WindowsGame1
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        SpriteBatch spriteBatch;

        enum GameState { PONG, BREAKOUT, SPACEINVADERS, RTYPE, QUIZ, SHIP3D, 
        INTRO, MENU, CREDIT, CONFIG, EXIT, HIGHSCORE, PAUSE, GAMEOVER, THEEND}

        GameState presentState = GameState.PONG;

        Pong pong;
        BreakOut breakOut;
        SpaceInvaders spaceInvaders;
        RType rType;
        Quiz quiz;
        Ship3D ship3D;

        string[] menuItems = { "Pong", "Breakout", "Space Invaders", "R-Type", "Quiz", "3D Ship",
                             "", "Back to Intro", "Credits", "Configs", "Highscores", "Exit"};

        Menu menu;

        KeyboardState ks;
        KeyboardState oldks;
        MouseState ms;
        MouseState oldms;
        GamePadState gps;
        GamePadState oldgps;

        public static Game1 game1 = new Game1();//? make it singleton

        //http://gamedev.stackexchange.com/questions/32018/xna-content-load-dependancy
        //http://xboxforums.create.msdn.com/forums/p/28953/164454.aspx
        //http://xboxforums.create.msdn.com/forums/p/54423/432023.aspx
        //http://xboxforums.create.msdn.com/forums/p/44719/266681.aspx
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();

            IsMouseVisible = true;

            Window.Title = "For the greater good of God...";

            Content.RootDirectory = "Content";

            game1 = this;
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
            
            //Components.Add(menu);

        }

        protected override void UnloadContent()
        {
           
        }

        protected override void Update(GameTime gameTime)
        {
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

