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
            :base()
        {
            this.texture = texture;
            this.Window = Window;
            this.random = new Random();
            this.position = new Vector2(random.Next(Window.ClientBounds.Width - this.texture.Width),
                random.Next(Window.ClientBounds.Height - this.texture.Height));
            this.soundEffect = soundEffect;
        }

        /// <summary>
        /// Construtor sobrecarregado sem o som, q estava com defeito...
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="Window"></param>
        public Pill(Texture2D texture, GameWindow Window)
            : base()
        {
            this.texture = texture;
            this.Window = Window;
            this.random = new Random();
            this.position = new Vector2(random.Next(Window.ClientBounds.Width - this.texture.Width),
                random.Next(Window.ClientBounds.Height - this.texture.Height));
            
        }

        public override void Update()
        {
            
        }

        public void Update(Rectangle snakeCollisionRect)
        {
            if (snakeCollisionRect.Intersects(this.CollisionRect))
            {
                Visible = false;
                //soundEffect.Play();
            }
        }

    }

}
