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

        enum GameState { PONG, BREAKOUT, SPACEINVADERS, RTYPE, QUIZ, SHIP3D }

        GameState presentState = GameState.SPACEINVADERS;

        Pong pong;
        BreakOut breakOut;
        SpaceInvaders spaceInvaders;
        RType rType;
        Quiz quiz;
        Ship3D ship3D;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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

        }

        protected override void UnloadContent()
        {
           
        }

        protected override void Update(GameTime gameTime)
        {
            switch (presentState)
            {
                case GameState.PONG:

                    pong.Update();

                    break;

                case GameState.BREAKOUT:

                    breakOut.Update();

                    break;

                case GameState.SPACEINVADERS:

                    spaceInvaders.Update();

                    break;

                case GameState.RTYPE:

                    rType.Update();

                    break;

                case GameState.QUIZ:

                    quiz.Update();

                    break;

                case GameState.SHIP3D:

                    ship3D.Update();

                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            switch (presentState)
            {
                case GameState.PONG:

                    pong.Draw();

                    break;

                case GameState.BREAKOUT:

                    breakOut.Draw();

                    break;

                case GameState.SPACEINVADERS:

                    spaceInvaders.Draw();

                    break;

                case GameState.RTYPE:

                    rType.Draw();

                    break;

                case GameState.QUIZ:

                    quiz.Draw();

                    break;

                case GameState.SHIP3D:
                    
                        ship3D.Draw();

                    break;
            }

            base.Draw(gameTime);
        }
    }
}
