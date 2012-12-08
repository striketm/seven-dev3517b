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

/*
 * int x = (blockRect.X + (blockRect.Width / 2)) - (ballRect.X + (ballRect.Width / 2));
int y = (blockRect.Y + (blockRect.Height / 2)) - (ballRect.Y + (ballRect.Height / 2));
if (Abs(x) > Abs(y))
{
    // reflect horizontally
    ballSpeedVector.X = -ballSpeedVector.X;
}
else
{
    // reflect vertically
    ballSpeedVector.Y = -ballSpeedVector.Y;
}
 */

/*
 * if (bottom1 < top2) return(1);
2
 if (top1 > bottom2) return(2);
3
 
4
 if (right1 < left2) return(3);
5
 if (left1 > right2) return(4);
6
 
7
 return(0);


0 = no collision
1= bottom collides with top
2 = top collides with bottom
3 = right collides with left
4 =left collides with right..
 */

/*
 * sphere-on-sphere collision:

view sourceprint?
1
if(dist(object_a,object_b) <= min(object_a.radius,object_b.radius))
2
  return true;
3
return false;

 */
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