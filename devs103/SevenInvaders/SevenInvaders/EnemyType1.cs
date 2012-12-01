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

namespace SevenInvaders
{
    class EnemyType1:Enemy
    {
        animation animation1;

        public EnemyType1(Texture2D texture)
            : base(texture)
        {
            animation1 = new animation();
            //animation1.frameX = 0;
            //animation1.frameY = 0;
            //animation1.frameWidth = 101;
            //animation1.frameHeight = 111;
            animation1.frame.X = 0;
            animation1.frame.Y = 0;
            animation1.frame.Width = 151;
            animation1.frame.Height = 111;

            animation1.frames = 2;
            animation1.fps = 2;

            current_animation = animation1;


        }

        public override void Update(GameTime gameTime)
        {
         
        }
    }
}
