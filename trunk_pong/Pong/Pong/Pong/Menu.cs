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
    class Menu
    {
        ContentManager Content;
        GameWindow Window;

        Texture2D fundo;

        Botao comecar;

        public Menu(ContentManager Content, GameWindow Window)
        {
            this.Content = Content;
            this.Window = Window;

            this.fundo = Content.Load<Texture2D>("Menu/menu_fundo");
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(fundo, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);
        }
    }
}
