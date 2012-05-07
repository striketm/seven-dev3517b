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

namespace MotoGame
{
    class Moto
    {
        Texture2D textura;//_moto;

        Vector2 posicao;//_moto;

        GameWindow Window;

        bool visivel;//one button...

        public Moto(ContentManager Content, GameWindow Window)
        {
            this.Window = Window;

            textura = Content.Load<Texture2D>("moto");

            posicao = new Vector2(200, 300);
        }

        public void Update(GameTime gameTime, KeyboardState teclado_atual, MouseState mouse_atual, GamePadState joystick_atual)
        {
            if (mouse_atual.LeftButton == ButtonState.Pressed)
            {
                posicao.X = mouse_atual.X;
                posicao.Y = mouse_atual.Y;
            }

            //if(joystick_atual.Buttons.A), B, X, Y, Left/Right Stick, Left/Right Shoulder, Start, 
            //if(joystick_atual.DPad.Down, Right, Left, Up
            if (joystick_atual.ThumbSticks.Right.X == 1)
            {
                posicao = Vector2.Zero;
            }

            if (teclado_atual.IsKeyDown(Keys.Right))
            {
                posicao.X += 5;
            }
            if (teclado_atual.IsKeyDown(Keys.Left))
            {
                posicao.X -= 5;
            }
            if (teclado_atual.IsKeyDown(Keys.Up))
            {
                posicao.Y -= 5;
            }
            if (teclado_atual.IsKeyDown(Keys.Down))
            {
                posicao.Y += 5;
            }

            if (posicao.X < 0)
            {
                posicao.X = 0;
            }

            if (posicao.X > Window.ClientBounds.Width - textura.Width)
            {
                posicao.X = Window.ClientBounds.Width - textura.Width;
            }

            if (posicao.Y < 0)
            {
                posicao.Y = 0;
            }

            if (posicao.Y > Window.ClientBounds.Height - textura.Height)
            {
                posicao.Y = Window.ClientBounds.Height - textura.Height;
            }
        }
        
        internal void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura, new Rectangle((int)posicao.X, (int)posicao.Y, 67, 47), new Rectangle(0, 0, 67, 47), Color.White);
        }
    }
}
