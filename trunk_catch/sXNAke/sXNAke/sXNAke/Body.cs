using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace sXNAke
{
    class Body : GameObject
    {

       public Direction actual_direction = Direction.RIGHT;


        public Body(Texture2D texture, Direction lastDirection)
            : base()
        {
            this.texture = texture;
            this.actual_direction = lastDirection;
            
            //WARNING: if you change this change it for all
            this.Velocity = new Vector2(4, 4);
            
        }

        public override void Update()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            switch (actual_direction)
            {
                case Direction.LEFT:
                    position.X -= Velocity.X;
                    break;
                case Direction.RIGHT:
                    position.X += Velocity.X;
                    break;
                case Direction.UP:
                    position.Y -= Velocity.Y;
                    break;
                case Direction.DOWN:
                    position.Y += Velocity.Y;
                    break;

            }
        }

    }
}
