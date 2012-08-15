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
    //uma snake vai ser formada por uma head, q tem uma direção...
    //e uma lista de body, cada um com uma direção...
    //qd apertar um botao, muda a direção da head...
    //qd as outras body forem chegando, muda a direção desta...?

    class Snake:GameObject
    {
        enum Direction { LEFT, RIGHT, UP, DOWN, }

        Direction current_direction = Direction.RIGHT;

        //Direction next_direction = Direction.RIGHT;//?

        //Texture2D head, straight, angle, tail;//?

        //List<Point> bodyPoints = new List<Point>();//?

        //bool extending;//?

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

            //Reset()//?

            //bodyPoints.Clear();

            //bodyPoints.Add(new Point(2, 0));
            //bodyPoints.Add(new Point(1, 0));
            //bodyPoints.Add(new Point(0, 0));

            //head = content.Load<Texture2D>("Sprites/Head");
            //straight = content.Load<Texture2D>("Sprites/Straight");
            //angle = content.Load<Texture2D>("Sprites/Angle");
            //tail = content.Load<Texture2D>("Sprites/Tail");
            
        }

        public override void Update()
        {

        }

        public void Update(KeyboardState keyboardState)
        {
            
            if (keyboardState.IsKeyDown(Keys.Right) && current_direction != Direction.LEFT)
            {
                
                current_direction = Direction.RIGHT;
            }
            else
                if (keyboardState.IsKeyDown(Keys.Left) && current_direction != Direction.RIGHT)
                {
                   
                    current_direction = Direction.LEFT;
                }
                else
                    if (keyboardState.IsKeyDown(Keys.Down)&& current_direction != Direction.UP)
                    {
                      

                        current_direction = Direction.DOWN;
                    }
                    else
                        if (keyboardState.IsKeyDown(Keys.Up)&& current_direction != Direction.DOWN)
                        {
                            

                            current_direction = Direction.UP;
                        }

            switch (current_direction)
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

        /*

        /// <summary>
        /// Determines if a given point is on the snake's body.
        /// </summary>
        /// <param name="p">The point in question.</param>
        /// <returns>True if the point lies on the body; false otherwise.</returns>
        public bool IsBodyOnPoint(Point p)
        {
            // utilize the List<T>.Contains method
            return bodyPoints.Contains(p);
        }

        /// <summary>
        /// Determines if the snake's head is at a given point.
        /// </summary>
        /// <param name="p">The point to test.</param>
        /// <returns>True if the head is at the position; false otherwise.</returns>
        public bool IsHeadAtPosition(Point p)
        {
            return (bodyPoints[0] == p);
        }

        /// <summary>
        /// Determine if the snake has formed a loop and crashed into itself.
        /// </summary>
        /// <returns>True if the snake crashed into itself; false otherwise.</returns>
        public bool IsLooped()
        {
            // if any of the positions (besides index 0) equal the head's position,
            // that means our head has crashed into the body somewhere.
            for (int i = 1; i < bodyPoints.Count; i++)
            {
                if (bodyPoints[i] == bodyPoints[0])
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines if the head is off the screen.
        /// </summary>
        /// <returns>True if the head is off the screen; false otherwise.</returns>
        public bool IsHeadOffScreen()
        {
            Point h = bodyPoints[0];

            return (h.X < 0 || h.Y < 0 || h.X >= Grid.MaxColumn || h.Y >= Grid.MaxRow);
        }

        /// <summary>
        /// Instructs the snake to extend next time it moves
        /// </summary>
        public void Extend()
        {
            extending = true;
        }


        private void MoveSnake()
        {
            // we need to store the original head position for moving the snake later
            Point p1 = bodyPoints[0];

            // determine which direction the snake is heading in and use
            // that to move the position of the head of the snake
            switch (currentDirection)
            {
                case Direction.Up:
                    bodyPoints[0] = new Point(p1.X, p1.Y - 1);
                    break;
                case Direction.Down:
                    bodyPoints[0] = new Point(p1.X, p1.Y + 1);
                    break;
                case Direction.Left:
                    bodyPoints[0] = new Point(p1.X - 1, p1.Y);
                    break;
                case Direction.Right:
                    bodyPoints[0] = new Point(p1.X + 1, p1.Y);
                    break;
            }

            // if the snake is extending this frame...
            if (extending)
            {
                // insert a new body part right behind the head
                bodyPoints.Insert(1, p1);

                // we no longer need to extend
                extending = false;

                // exit our Update method
                return;
            }

            // now we move the snake along by looping through all the body parts
            for (int i = 1; i < bodyPoints.Count; i++)
            {
                // store the body part's current position
                Point p2 = bodyPoints[i];

                // set the body part's position to the position of the body
                // part ahead of it
                bodyPoints[i] = p1;

                // save the body part's old position for updating the next body part
                p1 = p2;
            }
        }

        /// <summary>
        /// Draws the snake.
        /// </summary>
        /// <param name="spriteBatch">The game's SpriteBatch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            // draw the body
            for (int i = 1; i < bodyPoints.Count - 1; i++)
            {
                DrawBody(spriteBatch, bodyPoints[i], bodyPoints[i - 1], bodyPoints[i + 1]);
            }

            // draw the tail
            DrawTail(spriteBatch);

            // draw the head last so that it will always be on top in the case
            // of the snake intersecting itself
            DrawHead(spriteBatch);

            spriteBatch.End();
        }

        /// <summary>
        /// Draw's the snake's head.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to use when rendering.</param>
        private void DrawHead(SpriteBatch spriteBatch)
        {
            // get the head's point
            Point headPoint = bodyPoints[0];

            // get the point of the next body part
            Point nextBody = bodyPoints[1];

            // figure out the rotation for the head based on the positions
            // of the head and the next body part
            float rotation;
            if (headPoint.Y == nextBody.Y - 1)
            {
                rotation = -MathHelper.PiOver2;
            }
            else if (headPoint.Y == nextBody.Y + 1)
            {
                rotation = MathHelper.PiOver2;
            }
            else if (headPoint.X == nextBody.X - 1)
            {
                rotation = MathHelper.Pi;
            }
            else
            {
                rotation = 0f;
            }

            // draw the head using the global DrawSprite method.
            Grid.DrawSprite(spriteBatch, head, headPoint, rotation);
        }

        /// <summary>
        /// Draw's a piece of the snake's body.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to use when rendering.</param>
        /// <param name="current">The current body point.</param>
        /// <param name="last">The previous body point.</param>
        /// <param name="next">The next body point.</param>
        private void DrawBody(SpriteBatch spriteBatch, Point current, Point last, Point next)
        {
            // if the current, last, and next points create a 90 degree angle, we must
            // draw an angle piece.
            if ((current.X == last.X && current.X != next.X && current.Y != last.Y) ||
                (current.X == next.X && current.X != last.X && current.Y != next.Y))
            {
                // draw the sprite using the GetAngleRotation method to calculate the rotation
                Grid.DrawSprite(spriteBatch, angle, current, GetAngleRotation(current, last, next));
            }

            // otherwise if the current and last points are in different columns, we
            // draw the straight body piece with no rotation
            else if (current.X != last.X)
            {
                Grid.DrawSprite(spriteBatch, straight, current, 0f);
            }

            // otherwise if the current and last points are in different rows, we
            // draw the straight body piece rotated by PiOver2 (90 degrees)
            else if (current.Y != last.Y)
            {
                Grid.DrawSprite(spriteBatch, straight, current, MathHelper.PiOver2);
            }
        }

        /// <summary>
        /// Draw's the snake's tail
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to use when rendering.</param>
        private void DrawTail(SpriteBatch spriteBatch)
        {
            // get the point of the tail
            Point tailPoint = bodyPoints[bodyPoints.Count - 1];

            // get the point of the previous body part
            Point lastBody = bodyPoints[bodyPoints.Count - 2];

            // figure out the rotation for the tail based on the positions
            // of the tail and the next body part
            float rotation;
            if (tailPoint.Y == lastBody.Y - 1)
            {
                rotation = MathHelper.PiOver2;
            }
            else if (tailPoint.Y == lastBody.Y + 1)
            {
                rotation = -MathHelper.PiOver2;
            }
            else if (tailPoint.X == lastBody.X + 1)
            {
                rotation = MathHelper.Pi;
            }
            else
            {
                rotation = 0f;
            }

            // draw the tail using the global DrawSprite method.
            Grid.DrawSprite(spriteBatch, tail, tailPoint, rotation);
        }

        /// <summary>
        /// Figures out the rotation for an angled body piece.
        /// </summary>
        /// <param name="current">The angle body piece's position.</param>
        /// <param name="last">The previous body piece's position.</param>
        /// <param name="next">The next body piece's position.</param>
        /// <returns>The rotation (in radians) for the body piece.</returns>
        private float GetAngleRotation(Point current, Point last, Point next)
        {
            // first we calculate the offsets from the last and next 
            // points that determine the various angles of rotation.

            // these two are used to signal that we need -MathHelper.PiOver2
            Point negPiOver2 = new Point(next.X + 1, last.Y - 1);
            Point negPiOver22 = new Point(last.X + 1, next.Y - 1);

            // these two are used to signal that we need MathHelper.Pi
            Point pi = new Point(next.X - 1, last.Y - 1);
            Point pi2 = new Point(last.X - 1, next.Y - 1);

            // and these two are used to signal that we need MathHelper.PiOver2
            Point piOver2 = new Point(next.X - 1, last.Y + 1);
            Point piOver22 = new Point(last.X - 1, next.Y + 1);

            // now we compare our current point to the values above to see what
            // rotation we need. if none of the above match, we return 0f for the rotation.
            if (current == negPiOver2 || current == negPiOver22)
            {
                return -MathHelper.PiOver2;
            }
            else if (current == pi || current == pi2)
            {
                return MathHelper.Pi;
            }
            else if (current == piOver2 || current == piOver22)
            {
                return MathHelper.PiOver2;
            }
            else
            {
                return 0f;
            }
        }
         * */
    }
}
