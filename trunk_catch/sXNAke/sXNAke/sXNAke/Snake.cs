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
    class Snake:GameObject
    {
        enum Direction { LEFT, RIGHT, UP, DOWN, }

        Direction actual_direction = Direction.RIGHT;

        int points;

        int lifes;

        public Snake(Texture2D texture, GameWindow Window)
            :base()
        {
            this.texture = texture;
            this.Window = Window;
            this.Velocity = new Vector2(6, 6);
            this.points = 0;
            this.lifes = 5;
           
        }
        public override void Update()
        {

        }

        public void Update(KeyboardState keyboardState)
        {
            
            if (keyboardState.IsKeyDown(Keys.Right) && actual_direction != Direction.LEFT)
            {
                
                actual_direction = Direction.RIGHT;
            }
            else
                if (keyboardState.IsKeyDown(Keys.Left) && actual_direction != Direction.RIGHT)
                {
                   
                    actual_direction = Direction.LEFT;
                }
                else
                    if (keyboardState.IsKeyDown(Keys.Down)&& actual_direction != Direction.UP)
                    {
                      

                        actual_direction = Direction.DOWN;
                    }
                    else
                        if (keyboardState.IsKeyDown(Keys.Up)&& actual_direction != Direction.DOWN)
                        {
                            

                            actual_direction = Direction.UP;
                        }

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


            if (position.X >= Window.ClientBounds.Width - texture.Width)
            {
                position.X = Window.ClientBounds.Width - texture.Width;
            }
            if (position.X <= 0)
            {
                position.X = 0;
            }
            if (position.Y <= 0)
            {
                position.Y = 0;
            }
            if (position.Y >= Window.ClientBounds.Height - texture.Height)
            {
                position.Y = Window.ClientBounds.Height - texture.Height;
            }

            CollisionRect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

        }
       
    }
}
