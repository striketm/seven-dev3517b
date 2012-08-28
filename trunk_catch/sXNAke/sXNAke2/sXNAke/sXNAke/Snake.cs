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
    class Snake : GameObject
    {
        class ChangeDirection
        {
            public Direction direction;
            public Rectangle rect;
            public bool active;
            /// <summary>
            /// Acerto por causa da distancia entre a cabeça e o corpo
            /// </summary>
            public bool canRemove = false;
        }

        private List<ChangeDirection> changeDirections;

        private List<Body> bodies;

        Direction actual_direction = Direction.RIGHT;

        int points;

        int lifes;

        public Vector2 hudPosition;

        public Color hudColor;

        String player;

        private Texture2D textureBody;
        private Texture2D textureTail;
        /*
         * TODO: O movimento do corpo só podem ocorrer dentro de um grid. 
         * Implementar o GRID (movimentacao discreta).
         */
        public Snake(Texture2D textureHead, Texture2D textureBody, Texture2D textureTail, GameWindow Window, String player)
        {
            this.texture = textureHead;
            this.Window = Window;
            this.Position = new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2);
            this.Velocity = new Vector2(4, 4);
            this.points = 0;
            this.lifes = 2;
            this.player = player;
            this.bodies = new List<Body>();
            this.changeDirections = new List<ChangeDirection>();
            this.textureBody = textureBody;
            this.textureTail = textureTail;

            //make function add initial body or discover why i cant have none...
            Body body = new Body(textureBody, this.actual_direction);
            body.Position = new Vector2(this.position.X - this.texture.Width,
                this.position.Y);
            bodies.Add(body);


            if (player == "1")
            {
                actual_direction = Direction.RIGHT;
            }

            if (player == "2")
            {
                actual_direction = Direction.LEFT;
                this.position = new Vector2(800, 0);

            }
        }

        public void Grow()
        {
            //make points with grow!

            points++;

            Body body = new Body(textureBody, this.actual_direction);
            body.Position = this.position;

            switch (actual_direction)
            {
                case Direction.LEFT:
                    body.Position = new Vector2(
                        this.bodies.Last().CollisionRect.X + this.bodies.Last().CollisionRect.Width,
                        this.position.Y);
                    break;
                case Direction.RIGHT:
                    body.Position = new Vector2(
                        this.bodies.Last().CollisionRect.X - this.bodies.Last().CollisionRect.Width,
                        this.position.Y);
                    break;

                case Direction.UP:
                    body.Position = new Vector2(
                        this.position.X,
                        this.bodies.Last().CollisionRect.Y + this.bodies.Last().CollisionRect.Height);
                    break;
                case Direction.DOWN:
                    body.Position = new Vector2(this.position.X,
                        this.bodies.Last().CollisionRect.Y - this.bodies.Last().CollisionRect.Height);
                    break;

            }

            this.bodies.Add(body);
        }

        public override void Update()
        {

        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            //for (int k = 0; k < bodies.Count; k++)
            //{
            //    bodies[k].Update(gameTime);
            //}
            /*
             * TODO: Acertar a criação dos corpos, de modo que eles não fiquem sendo criados
             * fora da tela e nem na fora da posição ideal
             */
            foreach (Body body in bodies)
            {
                body.Update(gameTime);

                #region corpo fora da tela, teste

                bool bodyCollidedWithBorders = false;

                if (body.Position.X > Window.ClientBounds.Width)
                {
                    bodyCollidedWithBorders = true;
                }
                if (body.Position.X < 0)
                {
                    bodyCollidedWithBorders = true;
                }
                if (body.Position.Y < 0)
                {
                    bodyCollidedWithBorders = true;
                }
                if (body.Position.Y > Window.ClientBounds.Height)
                {
                    bodyCollidedWithBorders = true;
                }

                if (bodyCollidedWithBorders)
                    body.Visible = false;

                #endregion

                foreach (ChangeDirection changeDirection in changeDirections)
                {
                    bool removeThisChangeDirection = false;

                    if (body.CollisionRect.Contains(changeDirection.rect))
                    {
                        body.actual_direction = changeDirection.direction;
                        changeDirection.canRemove = true;
                        removeThisChangeDirection = true;
                    }
                    else
                    {
                        //removeThisChangeDirection = true;
                    }
                    if (removeThisChangeDirection) changeDirection.active = false;
                }
            }

            Window.Title = "" + bodies.Count;

            for (int k = 0; k < changeDirections.Count; k++)
            {
                if ((changeDirections[k].active == false) && (changeDirections[k].canRemove == true))
                changeDirections.RemoveAt(k);
            }

            for (int k = 0; k < bodies.Count; k++)
            {
                if ((bodies[k].Visible == false))
                    bodies.RemoveAt(k);
            }

            /* TODO: Criar um metodo para setar quais
             * teclas cada jogador responde.
            */
            if (player == "1")
            {
                if (Game1.keyboardState.IsKeyDown(Keys.G) &&
                !Game1.oldKeyboardState.IsKeyDown(Keys.G))
                {
                    Grow();
                }

                if (Game1.keyboardState.IsKeyDown(Keys.Right) &&
                     !Game1.oldKeyboardState.IsKeyDown(Keys.Right) &&
                     actual_direction != Direction.LEFT)
                {
                    if (actual_direction != Direction.RIGHT)
                    {
                        actual_direction = Direction.RIGHT;
                        ChangeDirection changeDirection = new ChangeDirection();
                        changeDirection.direction = actual_direction;
                        changeDirection.rect = new Rectangle((int)this.position.X,
                            (int)this.position.Y, this.CollisionRect.Width, this.CollisionRect.Height);
                        changeDirection.active = true;
                        this.changeDirections.Add(changeDirection);
                    }
                }
                else if (Game1.keyboardState.IsKeyDown(Keys.Left) &&
                            !Game1.oldKeyboardState.IsKeyDown(Keys.Left) &&
                            actual_direction != Direction.RIGHT)
                {
                    if (actual_direction != Direction.LEFT)
                    {
                        actual_direction = Direction.LEFT;
                        ChangeDirection changeDirection = new ChangeDirection();
                        changeDirection.direction = actual_direction;
                        changeDirection.rect = new Rectangle((int)this.position.X,
                            (int)this.position.Y, this.CollisionRect.Width, this.CollisionRect.Height);
                        changeDirection.active = true;
                        this.changeDirections.Add(changeDirection);
                    }
                }
                else if (Game1.keyboardState.IsKeyDown(Keys.Down) &&
                            !Game1.oldKeyboardState.IsKeyDown(Keys.Down) &&
                            actual_direction != Direction.UP)
                {
                    if (actual_direction != Direction.DOWN)
                    {
                        actual_direction = Direction.DOWN;
                        ChangeDirection changeDirection = new ChangeDirection();
                        changeDirection.direction = actual_direction;
                        changeDirection.rect = new Rectangle((int)this.position.X,
                            (int)this.position.Y, this.CollisionRect.Width, this.CollisionRect.Height);
                        changeDirection.active = true;
                        this.changeDirections.Add(changeDirection);
                    }
                }
                else if (Game1.keyboardState.IsKeyDown(Keys.Up) &&
                            !Game1.oldKeyboardState.IsKeyDown(Keys.Up) &&
                            actual_direction != Direction.DOWN)
                {
                    if (actual_direction != Direction.UP)
                    {
                        actual_direction = Direction.UP;
                        ChangeDirection changeDirection = new ChangeDirection();
                        changeDirection.direction = actual_direction;
                        changeDirection.rect = new Rectangle((int)this.position.X,
                            (int)this.position.Y, this.CollisionRect.Width, this.CollisionRect.Height);
                        changeDirection.active = true;
                        this.changeDirections.Add(changeDirection);
                    }
                }
            }

            if (player == "2")
            {
                if (keyboardState.IsKeyDown(Keys.D) && actual_direction != Direction.LEFT)
                {
                    actual_direction = Direction.RIGHT;
                }
                else
                    if (keyboardState.IsKeyDown(Keys.A) && actual_direction != Direction.RIGHT)
                    {
                        actual_direction = Direction.LEFT;
                    }
                    else
                        if (keyboardState.IsKeyDown(Keys.S) && actual_direction != Direction.UP)
                        {
                            actual_direction = Direction.DOWN;
                        }
                        else
                            if (keyboardState.IsKeyDown(Keys.W) && actual_direction != Direction.DOWN)
                            {
                                actual_direction = Direction.UP;
                            }
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

            #region collision with borders

            bool collidedWithBorders = false;

            if (position.X > Window.ClientBounds.Width - texture.Width)
            {
                collidedWithBorders = true;
                position.X = Window.ClientBounds.Width - texture.Width;
            }
            if (position.X < 0)
            {
                collidedWithBorders = true;
                position.X = 0;
            }
            if (position.Y < 0)
            {
                collidedWithBorders = true;
                position.Y = 0;
            }
            if (position.Y > Window.ClientBounds.Height - texture.Height)
            {
                collidedWithBorders = true;
                position.Y = Window.ClientBounds.Height - texture.Height;
            }

            if (collidedWithBorders)
                Die();

            #endregion

            CollisionRect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

        }

        public void Die()
        {
            lifes--;
            if (lifes < 0)
            {
                Game1.present_State = Game1.States.INTRO;
                points = 0;
            }
            changeDirections.Clear();
            bodies.Clear();
            Position = Vector2.Zero;
            actual_direction = Direction.RIGHT;
            //i need that, why?:
            Body body = new Body(textureBody, this.actual_direction);
            body.Position = new Vector2(this.position.X - this.texture.Width,
                this.position.Y);
            bodies.Add(body);
        }

        public new void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.arial20, "Vidas: " + lifes + "\nPontos: " + points, hudPosition, hudColor);

            foreach (Body body in bodies)
            {
                body.Draw(gameTime, spriteBatch);
            }

            foreach (ChangeDirection cd in changeDirections)
            {
                spriteBatch.Draw(textureBody, cd.rect, Color.Black);
            }

            base.Draw(gameTime, spriteBatch);
        }
    }
}