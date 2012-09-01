using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework; //Must alway be used if we are to reach the basic xna functions
using Microsoft.Xna.Framework.Graphics; //Since we are going to implement graphics for the player we must use graphic
using Microsoft.Xna.Framework.Input; //And we want to move the player so will we need to use Input.

namespace Pacman
{
    class Player
    {
        //Our class members
        #region Members

        Game1 game; //Our game1. Needed if we want to reach functions and members in the Game1 class

        Texture2D playerTex; //The graphic for our player

        KeyboardState currentKeyboard; //The current keyboard will be use to read wich buttons are pressed on the keyboard
        KeyboardState previousKeyboard; //The prevoius keyboard state will check wich keys was pressed last update call

        Vector2 Position; //This vector 2 will contain the players position;
        Point frameSize = new Point(32, 32);
        Point currentFrame = new Point(0, 0);
        Point sheetSize = new Point(3, 1);
        //frameratecontrol
        int timeSinceLastFrame = 0;
        int millisecondsPerFrame = 50;
        #endregion

        //All the functions that will be used for initializing our player class
        #region Initialization

        //Our constructor
        //It will save away a copy of our game1 class.
        //It will also take in a Vector2 of what position our player shall start on
        //
        //Remember: That a constructor is suppous to have the same name as the class.
        //
        //A constructor is a function executet when we create the class.
        public Player(Game1 game, Vector2 pos)
        {
            //We save the game we take in to the class game member
            this.game = game;

            //We set our Position to the Vector2 value we get from pos
            Position = pos;
        }


        //Initialize
        //This function will set the basic values and load the graphic for our player.
        //Should be called when created.
        public void Initialize()
        { 
            //Load the texture. To that we us our "game" member to reach its content manager
            playerTex = game.Content.Load<Texture2D>("sprites/pacstill");

            //We new the currentKeyboard & previousKeyboard
            currentKeyboard = new KeyboardState();
            previousKeyboard = new KeyboardState();
        }

        #endregion

        //Here is our function for updating the player and drawing it
        #region Update & Draw

        public void Update(GameTime gameTime)
        {
            //framerate control

            //animation
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;
                ++currentFrame.X;
                if (currentFrame.X >= sheetSize.X)
                {
                    currentFrame.X = 0;
                    ++currentFrame.Y;
                    if (currentFrame.Y >= sheetSize.Y)
                        currentFrame.Y = 0;
                }
                //We get a new keyboard state for currentKeyboard so we can check if a key has been pressed
                currentKeyboard = Keyboard.GetState();

                //Check if a key has been pressed
                if (currentKeyboard.IsKeyDown(Keys.Right))
                {
                    //If the right arrow been pressed we move the player to the right
                    Position.X += 5;
                }
                else if (currentKeyboard.IsKeyDown(Keys.Left))
                {
                    //If the left arrow been pressed we move the player to the left
                    Position.X -= 5;
                }
                else if (currentKeyboard.IsKeyDown(Keys.Up))
                {
                    //If the up arrow been pressed we move the player up
                    Position.Y -= 5;
                }
                else if (currentKeyboard.IsKeyDown(Keys.Down))
                {
                    //If the down arrow been pressed we move the player down
                    Position.Y += 5;
                }

                //We set the previousKeyboard to the current.
                //We don't use the previous keyboard anything in this class yet.
                //
                //It is used for example if you have a fire key and you press it and don't
                //want the player to fire as long as the key is down.So can you write like this and it will 
                //just fire once per key press:
                //if (currentKeyboard.IsKeyDown(Keys.Space) && previousKeyboard.IsKeyUp(Keys.Space))
                //{
                //  fire
                //}
                previousKeyboard = currentKeyboard;

            }
        }
        
        //Our draw function that will draw our player
        public void Draw(GameTime gameTime)
        { 
            //We get the games spriteBatch and store it away
            SpriteBatch spriteBatch = game.SpriteBatch;

            //Draw the player
            spriteBatch.Draw(playerTex, Position,
            new Rectangle(currentFrame.X * frameSize.X,
            currentFrame.Y * frameSize.Y,
            frameSize.X,
            frameSize.Y),
            Color.White, 0, Vector2.Zero,
            1, SpriteEffects.None, 0);
        }

        #endregion
    }
}
