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
    class Player : Sprite
    {
        #region Atributos

        int player;
        int life;

        animation animation1;
                
        # endregion

        public Player(
            Texture2D texture,
            Vector2 position,
            int player, 
            GameWindow gameWindow
            ):base (texture)
        {
            this.Position = position;

            this.player = player;
          
            this.Velocity = new Vector2(10, 0);

            this.Window = gameWindow;

            animation1 = new animation();
            animation1.frame.X = 0;
            animation1.frame.Y = 0;
            animation1.frame.Width = 94;
            animation1.frame.Height = 123;

            animation1.frames = 3;
            animation1.fps = 1;

            current_animation = animation1;
        }
        
          public Player(
            Texture2D texture,
            Vector2 position,
            int player, 
            float rotation,
            GameWindow gameWindow
            ):base(texture)
        {
            //this.Texture = texture;
            this.Position = position;
            this.player = player;
            this.Rotation = rotation;
            this.Velocity = new Vector2(10, 0);
            this.Window = gameWindow;
        }

          public override void Update(GameTime gameTime)
          {

          }

        public void Update(GameTime gameTime, KeyboardState ks)
        {
            #region teclado 


            if (player == 1)
            {
                if (ks.IsKeyDown(Keys.Right))
                {
                    PositionX += Velocity.X;
                    //Position = new Vector2(Position.X + Velocity.X, Position.Y);
                }
                else if (ks.IsKeyDown(Keys.Left))
                {
                    PositionX -= Velocity.X;
                }
                else if (ks.IsKeyDown(Keys.Down))
                {
                    PositionY += Velocity.Y;
                }
                else if (ks.IsKeyDown(Keys.Up))
                {
                    PositionY -= Velocity.Y;
                }
            }
            if (player == 2)
            {
                if (ks.IsKeyDown(Keys.D))
                {
                    PositionX += Velocity.X;
                }
                else if (ks.IsKeyDown(Keys.A))
                {
                    PositionX -= Velocity.X;
                }
                else if (ks.IsKeyDown(Keys.S))
                {
                    PositionY += Velocity.Y;
                }
                else if (ks.IsKeyDown(Keys.W))
                {
                    PositionY -= Velocity.Y;
                }
            }

            #endregion

            //se a nave sair pela esquerda nao deixar
            if (Position.X < 0)
            {
                PositionX = 0;
            }

            if (PositionX > Window.ClientBounds.Width - Frame.Width)
            {
               PositionX = Window.ClientBounds.Width - Frame.Width;
            }
        }
    }//fim da classe Ship
}//fim do namespace SevenInvaders
