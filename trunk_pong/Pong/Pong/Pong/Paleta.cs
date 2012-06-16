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

namespace Pong
{
    class Paleta
    {
        /// <summary>
        /// Imagem que sera utilizada nas barras
        /// </summary>
        Texture2D imagem;

        Vector2 posicao;
        Rectangle colisao;
        KeyboardState teclado_atual, teclado_anterior;
        GamePadState gamePad_atual, gamePad_anterior;

        GameWindow Window;

        int pontos;
        public int Pontos{get{return pontos;}set{pontos = value;}}

        int jogador;
        //TODO fazer enum

        SpriteFont fonte;

        public Paleta(ContentManager Content, GameWindow Window, int jogador)
        {
            this.Window = Window;
            this.pontos = 0;
            this.jogador = jogador;
            
            imagem = Content.Load<Texture2D>("paleta");
            fonte = Content.Load<SpriteFont>("Arial");

            if (jogador == 1)
            {
                 posicao = new Vector2(0, Window.ClientBounds.Height / 2 - imagem.Height / 2);
            }
            else
            {
                posicao = new Vector2(Window.ClientBounds.Width - imagem.Width, Window.ClientBounds.Height / 2 - imagem.Height / 2);
            }
            colisao = new Rectangle((int)posicao.X, (int)posicao.Y, imagem.Width, imagem.Height);

        }

        public void Update(int jogador)
        {
            teclado_atual = Keyboard.GetState();
            gamePad_atual = GamePad.GetState(PlayerIndex.One);

            if (jogador == 1)
            {
                if (teclado_atual.IsKeyDown(Keys.W) 
                    || gamePad_atual.ThumbSticks.Right.Y > 0.1f
                    || gamePad_atual.ThumbSticks.Left.Y ==1
                    || gamePad_atual.DPad.Up==ButtonState.Pressed
                    || gamePad_atual.Buttons.Y==ButtonState.Pressed
                    //|| gamePad_atual.Triggers.Left==1
                    || gamePad_atual.Buttons.LeftShoulder==ButtonState.Pressed
                    //|| gamePad_atual.Buttons.LeftStick==ButtonState.Pressed
                    //|| gamePad_atual.Triggers.Right == 1
                    || gamePad_atual.Buttons.RightShoulder == ButtonState.Pressed
                    //|| gamePad_atual.Buttons.RightStick == ButtonState.Pressed
                    )
                {
                    if (gamePad_atual.ThumbSticks.Right.Y > 0.1f)
                        posicao.Y -= 5 * gamePad_atual.ThumbSticks.Right.Y;
                    else posicao.Y -= 5;
                }
                if (teclado_atual.IsKeyDown(Keys.S))
                {
                    posicao.Y += 5;
                }
            }
            else
            {
                if (teclado_atual.IsKeyDown(Keys.Up))
                {
                    posicao.Y -= 5;
                }
                if (teclado_atual.IsKeyDown(Keys.Down))
                {
                    posicao.Y += 5;
                }
            }

            if (posicao.Y < 0)
            {
                posicao.Y = 0;
            }
            if (posicao.Y > Window.ClientBounds.Height - imagem.Height)
            {
                posicao.Y = Window.ClientBounds.Height - imagem.Height;
            }
            
            if (posicao.Y < 0)
            {
                posicao.Y = 0;
            }
            if (posicao.Y > Window.ClientBounds.Height - imagem.Height)
            {
                posicao.Y = Window.ClientBounds.Height - imagem.Height;
            }

            colisao.X = (int)posicao.X;
            colisao.Y = (int)posicao.Y;
            colisao.X = (int)posicao.X;
            colisao.Y = (int)posicao.Y;

            teclado_anterior = teclado_atual;
            gamePad_anterior = gamePad_atual;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Color cor)
        {
            spriteBatch.Draw(this.imagem, this.posicao, cor);

            if (jogador == 1)
            {
                spriteBatch.DrawString(fonte, "Pontos: " + pontos, Vector2.Zero, cor);
            }
            else
            {
                spriteBatch.DrawString(fonte, "Pontos: " + pontos, new Vector2(Window.ClientBounds.Width - (fonte.MeasureString("Pontos: " + pontos).X), 0), cor);
            }
        }

        public Rectangle Colisao
        {
            get
            {
                return new Rectangle((int)posicao.X, (int)posicao.Y, imagem.Width, imagem.Height);
            }
            set
            {
                colisao = value;
            }
        }

    }
}
