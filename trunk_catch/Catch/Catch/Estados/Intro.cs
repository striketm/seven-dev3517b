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
using CapturarObjetos.Nucleo;
using CapturarObjetos.Objetos;

namespace CapturarObjetos.Estados
{
    class Intro
    {
        GameWindow Window;
        ContentManager Content;

        SpriteFont arial14;

        public Intro(GameWindow Window, ContentManager Content)
        {
            this.Window = Window;
            this.Content = Content;

            arial14 = Content.Load<SpriteFont>("arial14");
        }

        public void Update(GameTime gameTime,
            KeyboardState teclado_atual, KeyboardState teclado_anterior,
            MouseState mouse_atual, MouseState mouse_anterior,
            GamePadState gamepad_atual, GamePadState gamepad_anterior)
        {
            if (teclado_atual.IsKeyDown(Keys.Enter) && !teclado_anterior.IsKeyDown(Keys.Enter))
            {
                Game1.estado_atual = Game1.Estado.JOGO;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(arial14, "Intro by Cleber Tavares Jr.\nPress Enter to go",
                new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2),
                Color.White);

            spriteBatch.End();
        }
    }
}
