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
        }

//REMOVER OS ATRIBUTOS EXCEDENTES DA CLASSE SHIP E CORRIGIR PARA USAR OS ATRIBUTOS HERDADOS DE SPRITE






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

        public void Update(KeyboardState ks)
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

        public void Draw(SpriteBatch spriteBatch)
        {
           
            //spriteBatch.Draw(
            //    texture,
            //    new Rectangle((int)position.X, (int)position.Y, frame.Width, frame.Height),
            //    frame, color);

            spriteBatch.Draw(
                Texture, 
                new Rectangle((int)Position.X, (int)Position.Y, Frame.Width, Frame.Height),
                Frame, 
                Color,
                Rotation,
                new Vector2(0,0), 
                SpriteEffects.None,
                1.0f);

            

            

        }

    }//fim da classe Ship
}//fim do namespace SevenInvaders
