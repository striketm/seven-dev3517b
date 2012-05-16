using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;//
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MotoGame
{
    class Moto:Sprite
    {
        SoundEffect ronco;

        animacao andando;
        animacao correndo;
        animacao animacao_atual;

        public Moto(ContentManager Content, GameWindow Window)
            :base(Content.Load<Texture2D>("moto"))
        {
            this.Window = Window;

            textura = Content.Load<Texture2D>("moto");

            posicao = new Vector2(200, 300);

            ronco = Content.Load<SoundEffect>("Sounds/SoundEffects/sound_effect");

            andando = new animacao();
            andando.quadro_X = 67;
            andando.quadro_Y = 47;
            andando.qtd_quadros = 3;
            andando.quadros_seg = 3;
            andando.Y = 0;

            correndo = new animacao();
            correndo.quadro_X = 67;
            correndo.quadro_Y = 47;
            correndo.qtd_quadros = 3;
            correndo.quadros_seg = 9;
            correndo.Y = 47;

            animacao_atual = correndo;

        }

        public override void Update(GameTime gameTime)
        {}

        public void Update(GameTime gameTime, KeyboardState teclado_atual, MouseState mouse_atual, GamePadState joystick_atual)
        {
            if (mouse_atual.LeftButton == ButtonState.Pressed)
            {
                posicao.X = mouse_atual.X;
                posicao.Y = mouse_atual.Y;
                ronco.Play();
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

            #region manter na tela
            if (posicao.X > Window.ClientBounds.Width - textura.Width/3)
            {
                posicao.X = Window.ClientBounds.Width - textura.Width/3;
            }

            if (posicao.Y < 0)
            {
                posicao.Y = 0;
            }

            if (posicao.Y > Window.ClientBounds.Height - textura.Height/2)
            {
                posicao.Y = Window.ClientBounds.Height - textura.Height/2;
            }
            #endregion
        }

        /*
          Namespaces, interfaces e membros de enumeração têm acesso implicitamente “public” e não pode ser modificado;
Tipos (incluindo classes) podem ser “public” ou “internal”, o padrão é “internal”, quase igual a public, visivel dentro do emsmo assembly;
Membros de classes podem ser de todos os tipos, o padrão é “private”;
Membros de estruturas podem ser “public”, “internal” ou “private”, o padrão é “private”;
         */
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch, animacao_atual);

            //spriteBatch.Draw(textura, new Rectangle((int)posicao.X, (int)posicao.Y, 67, 47), new Rectangle(0, 0, 67, 47), Color.White);
        }
    }
}
