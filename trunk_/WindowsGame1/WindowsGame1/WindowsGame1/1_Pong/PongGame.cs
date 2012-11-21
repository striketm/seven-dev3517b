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

namespace WindowsGame1._1_Pong
{
    class Pong
    {
        Pallete pallete1;
        Pallete pallete2;

        string path = "1_Pong/";

        public Pong(ContentManager Content)
        {
            pallete1 = new Pallete(Content.Load<Texture2D>(path+"pallete1"));
            pallete1.Position = new Vector2(10, 0);
            //pallete1.Position.X

            pallete2 = new Pallete(Content.Load<Texture2D>(path + "pallete2"));
            pallete2.Position = new Vector2(100, 0);
            //pallete2.Position.Y
         
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            pallete1.Draw(gameTime, spriteBatch);
            pallete2.Draw(gameTime, spriteBatch);
        }

    }
}