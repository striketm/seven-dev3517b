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

        //9/5
        Vector2 velocidade;

        GameWindow Window;

        bool visivel;//one button...

        private bool tocado;	// para o toque

        Color colorOverlay;

        #region leitores e acessores, gets and sets
        public Color ColorOverlay
        {
            set
            {
                colorOverlay = value;
            }
        }

        public Vector2 Posicao
        {
            set { posicao = value; }
        }

        public Vector2 Velocidade { set { velocidade = value; } get { return velocidade; } }

        #endregion


        public Moto(ContentManager Content, GameWindow Window)
        {
            this.Window = Window;

            textura = Content.Load<Texture2D>("moto");

            posicao = new Vector2(200, 300);

            velocidade = new Vector2(5, 5);

            colorOverlay = Color.White;
        }

        //public override void Update() { }

        public void Update(GameTime gametime, List<Keys> teclas)
        {
            foreach (Keys tecla in teclas)
            {
                if (Keyboard.GetState().IsKeyDown(tecla) && (tecla == Keys.Up || tecla == Keys.W))
                {
                    posicao.Y -= velocidade.Y;
                }

                if (Keyboard.GetState().IsKeyDown(tecla) && (tecla == Keys.Down || tecla == Keys.S))
                {
                    posicao.Y += velocidade.Y;
                }

                if (Keyboard.GetState().IsKeyDown(tecla) && (tecla == Keys.Left||tecla == Keys.A))
                {
                    posicao.X -= velocidade.X;
                }

                if (Keyboard.GetState().IsKeyDown(tecla) && (tecla == Keys.Right||tecla == Keys.D))
                {
                    posicao.X += velocidade.X;
                }
            }

            // atualiza a posicao do rect de colisao da moto de acordo com a posicao
            //this.size.X = (int)position.X;
            //this.size.Y = (int)position.Y;
        }
        public void Update(GameTime gameTime, KeyboardState teclado_atual, MouseState mouse_atual, GamePadState joystick_atual,
                                                                    KeyboardState teclado_anterior, MouseState mouse_anterior, GamePadState joystick_anterior)
        {
            if ((mouse_atual.LeftButton == ButtonState.Pressed)&&
                (mouse_anterior.LeftButton != ButtonState.Pressed))
            {
                if ((mouse_atual.X >= (posicao.X))
               && (mouse_atual.X <= (posicao.X + textura.Width/3))
               && (mouse_atual.Y >= (posicao.Y))
               && (mouse_atual.Y <= (posicao.Y + textura.Height)))
                //if(new Rectangle((int)posicao.X, (int)posicao.Y, 
                //    67, 47).Contains(mouse_atual.X, mouse_atual.Y))
                {
                    tocado = true;
                }
                else
                {
                    //tocado = false;
                }
            }
            if (mouse_atual.LeftButton == ButtonState.Released)
            {
                tocado = false;
            }

            if (tocado)
            {
                posicao.X = mouse_atual.X;
                posicao.Y = mouse_atual.Y;

            }

            #region Drag


            #endregion

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

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura, new Rectangle((int)posicao.X, (int)posicao.Y, 67, 47), new Rectangle(0, 0, 67, 47), colorOverlay);
        }
    }
}
