using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman
{
    class Map //To create a class right click on Pacman and choose > add > class
    {

        // All the members of the class
        #region members

        Game1 game; //Game1 is the main Game class. We will need one of these to be able to reach
                    //functions and members of the Game1 class

        int tileWidth = 32;
        int tileHeight = 32;

        int[,] startscreen = new int[,]
        {
            { 3, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 16, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 4,},
            { 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5,},
            { 5, 0, 3, 16, 4, 0, 3, 16, 16, 4, 0, 5, 0, 3, 16, 16, 4, 0, 3, 16, 4, 0, 5,},
            { 5, 0, 1, 17, 2, 0, 1, 17, 17, 2, 0, 13, 0, 1, 17, 17, 2, 0, 1, 17, 2, 0, 5,},
            { 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, },
            { 5, 0, 14, 6, 15, 0, 12, 0, 14, 6, 6, 16, 6, 6, 15, 0, 12, 0, 14, 6, 15, 0, 5,},
            { 5, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 5, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 5,},
            { 19, 16, 16, 16, 4, 0, 19, 6, 6, 15, 0, 13, 0, 14, 6, 6, 18, 0, 3, 16, 16, 16, 18,},
            { 19, 0, 0, 0, 18, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 19, 0, 0, 0, 18,},
            { 1, 17, 17, 17, 2, 0, 13, 0, 3, 16, 16, 20, 16, 16, 4, 0, 13, 0, 1, 17, 17, 17, 2,},
            { 0, 0, 0, 0, 0, 0, 0, 0, 19, 0, 0, 0, 0, 0, 18, 0, 0, 0, 0, 0, 0, 0, 0,},
            { 3, 16, 16, 16, 4, 0, 12, 0, 1, 17, 17, 17, 17, 17, 2, 0, 12, 0, 3, 16, 16, 16, 4,},
            { 19, 0, 0, 0, 18, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 19, 0, 0, 0, 18,},
            { 19, 17, 17, 17, 2, 0, 13, 0, 14, 6, 6, 16, 6, 6, 15, 0, 13, 0, 1, 17, 17, 17, 18,},
            { 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5,},
            { 5, 0, 14, 6, 4, 0, 14, 6, 6, 15, 0, 13, 0, 14, 6, 6, 15, 0, 3, 6, 15, 0, 5,},
            { 5, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 5,},
            { 19, 6, 15, 0, 13, 0, 12, 0, 14, 6, 6, 16, 6, 6, 15, 0, 12, 0, 13, 0, 14, 6, 18,},
            { 5, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 5, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 5,},
            { 5, 0, 14, 6, 6, 6, 17, 6, 6, 15, 0, 13, 0, 14, 6, 6, 17, 6, 6, 6, 15, 0, 5,},
            { 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5,},
            { 1, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 2,},
             
        };

        List<Texture2D> starttiles = new List<Texture2D>();

        #endregion

        //Functions for initialize the class and load reasources
        #region Initialization

        //This is a constructor.
        //A constructor is a method that is executed when this object is created later.
        //Because we want a copy of the Game1 class so we can reach all it's functions and members
        //so is this the perfect place to do that. 
        //This function wants a game1 wich we saves away in the constructor.
        public Map(Game1 game)
        {
            this.game = game;
        }

        //A load function to load all the graphics this class will need.
        //We will call this function in the LoadContent in the Game1 class later
        public void Load()
        {
            Texture2D texture;

            #region Textures
            texture = game.Content.Load<Texture2D>("starttiles/black");//0
            starttiles.Add(texture);
            texture = game.Content.Load<Texture2D>("starttiles/downleft");//1
            starttiles.Add(texture);
            texture = game.Content.Load<Texture2D>("starttiles/downright");//2
            starttiles.Add(texture);
            texture = game.Content.Load<Texture2D>("starttiles/upleft");//3
            starttiles.Add(texture);
            texture = game.Content.Load<Texture2D>("starttiles/upright");//4
            starttiles.Add(texture);
            texture = game.Content.Load<Texture2D>("starttiles/ud");//5
            starttiles.Add(texture);
            texture = game.Content.Load<Texture2D>("starttiles/lr");//6
            starttiles.Add(texture);
            texture = game.Content.Load<Texture2D>("starttiles/vup");//7
            starttiles.Add(texture);
            texture = game.Content.Load<Texture2D>("starttiles/vdown");//8
            starttiles.Add(texture);
            texture = game.Content.Load<Texture2D>("starttiles/vleft");//9
            starttiles.Add(texture);
            texture = game.Content.Load<Texture2D>("starttiles/vright");//10
            starttiles.Add(texture);
            texture = game.Content.Load<Texture2D>("starttiles/all");//11
            starttiles.Add(texture);
            texture = game.Content.Load<Texture2D>("starttiles/uup");//12
            starttiles.Add(texture);
            texture = game.Content.Load<Texture2D>("starttiles/udown");//13
            starttiles.Add(texture);
            texture = game.Content.Load<Texture2D>("starttiles/uleft");//14
            starttiles.Add(texture);
            texture = game.Content.Load<Texture2D>("starttiles/uright");//15
            starttiles.Add(texture);
            texture = game.Content.Load<Texture2D>("starttiles/sup");//16
            starttiles.Add(texture);
            texture = game.Content.Load<Texture2D>("starttiles/sdown");//17
            starttiles.Add(texture);
            texture = game.Content.Load<Texture2D>("starttiles/sright");//18
            starttiles.Add(texture);
            texture = game.Content.Load<Texture2D>("starttiles/sleft");//19
            starttiles.Add(texture);
            texture = game.Content.Load<Texture2D>("starttiles/exit");//20
            starttiles.Add(texture);
            #endregion
        }

        #endregion

        //Functions for drawing and updating this class.
        #region Update & Draw

        //Draws the map.
        //We will call this in the Draw function in the Game1 class later
        public void Draw(GameTime gameTime)
        {

            for (int y = 0; y < startscreen.GetLength(0); y++)
            {
                for (int x = 0; x < startscreen.GetLength(1); x++)
                {
                    int textureIndex = startscreen[y, x];
                    Texture2D texture = starttiles[textureIndex];

                    game.SpriteBatch.Draw(texture, new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight), Color.White);

                }
            }
        }

        #endregion
    }
}
