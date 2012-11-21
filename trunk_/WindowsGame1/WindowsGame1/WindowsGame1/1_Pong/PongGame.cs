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
        Ball ball;

        string path = "1_Pong/";

        public Pong(ContentManager Content)
        {
            pallete1 = new Pallete(Content.Load<Texture2D>(path+"pallete1"));
            pallete1.Position = new Vector2(
                (Game1.Instance.Window.ClientBounds.Width-pallete1.Texture.Width)/2,
                0);
            //pallete1.Position.X
            pallete1.player = 1;

            pallete2 = new Pallete(Content.Load<Texture2D>(path + "pallete2"));
            pallete2.Position = new Vector2(
                (Game1.Instance.Window.ClientBounds.Width - pallete2.Texture.Width) / 2,
                (Game1.Instance.Window.ClientBounds.Height - pallete2.Texture.Height));
            //pallete2.Position.Y
            pallete2.player = 2;

            ball = new Ball(Game1.Instance.Content.Load<Texture2D>(path+"ball"));
            ball.Position = new Vector2(
                (Game1.Instance.Window.ClientBounds.Width - ball.Texture.Width) / 2,
                (Game1.Instance.Window.ClientBounds.Height - ball.Texture.Height)/ 2);
         
        }

        public void Update(GameTime gameTime)
        {
            pallete1.Update(gameTime);
            pallete2.Update(gameTime);
            ball.Update(gameTime);

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            pallete1.Draw(gameTime, spriteBatch);
            pallete2.Draw(gameTime, spriteBatch);
            ball.Draw(gameTime, spriteBatch);
        }

    }
}