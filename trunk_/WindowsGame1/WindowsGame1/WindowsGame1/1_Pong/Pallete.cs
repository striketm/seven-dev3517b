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
    class Pallete:Sprite
    {
        public int player { get; set; }

        bool horizontal;
        //list keys

        public Pallete(Texture2D texture):base(texture)
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (player == 1)
            {
                if (Game1.Instance.keyboardIsPressing(Keys.Left))
                {
                    PositionX -= Velocity.X;
                }
                if (Game1.Instance.keyboardIsPressing(Keys.Right))
                {
                    PositionX += Velocity.X;
                }
            }

            if (player == 2)
            {
                if (Game1.Instance.keyboardIsPressing(Keys.A))
                {
                    PositionX -= Velocity.X;
                }
                if (Game1.Instance.keyboardIsPressing(Keys.D))
                {
                    PositionX += Velocity.X;
                }
            }

            if (PositionX < 0)
            {
                PositionX = 0;
            }

            if (PositionX > Game1.Instance.Window.ClientBounds.Width - Collision.Width)
            {
                PositionX = Game1.Instance.Window.ClientBounds.Width - Collision.Width;
            }
        }
    }
}
