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
    class Jogo
    {
        GameWindow Window;
        ContentManager Content;

        public Jogo(GameWindow Window, ContentManager Content)
        {
            this.Window = Window;
            this.Content = Content;

        }

        public void Update(GameTime gameTime,
            KeyboardState teclado_atual, KeyboardState teclado_anterior,
            MouseState mouse_atual, MouseState mouse_anterior,
            GamePadState gamepad_atual, GamePadState gamepad_anterior)
        {

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }
    }
}
