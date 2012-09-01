using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace sXNAke
{
    class Pill:GameObject
    {

        SoundEffect soundEffect;
       

        public Pill(Texture2D texture, SoundEffect soundEffect, GameWindow Window)
        {
            this.texture = texture;
            this.Window = Window;
            this.random = new Random();
            this.position = new Vector2(random.Next(Window.ClientBounds.Width - this.texture.Width),
                random.Next(Window.ClientBounds.Height - this.texture.Height));
            this.soundEffect = soundEffect;
            this.myColor = Color.Blue;

            foreach (Body body in Snake.bodies)
            {
                if (this.CollisionRect.Intersects(body.CollisionRect))
                {
                    this.Visible = false;
                }
            }

            PillManager.pills.Add(this);
        }

        public override void Update()
        {
            
        }

        public void Update(Snake snake)
        {
            
            if (snake.CollisionRect.Intersects(this.CollisionRect)&& Visible)
            {
                Visible = false;
                soundEffect.Play();
                snake.Grow();
            }
        }

        
    }

}
