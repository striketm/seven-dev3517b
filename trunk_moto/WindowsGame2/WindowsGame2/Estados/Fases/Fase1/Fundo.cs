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


namespace MotoGame.Estados.Fases.Fase1
{
    class Fundo
    {
        Texture2D texture;
        Rectangle origem;
        Rectangle destino;
        GameWindow Window;

        public Fundo(Texture2D texture, GameWindow Window)
        {
            this.texture = texture;
            this.Window = Window;

            destino = new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height);

            origem = new Rectangle(0, texture.Height - Window.ClientBounds.Height, Window.ClientBounds.Width, Window.ClientBounds.Height);

        }

        public void Update(GameTime gameTime, Vector2 posicao)
        {
            origem.X += (int)posicao.X;
            origem.Y += (int)posicao.Y;

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destino, origem, Color.White);
        }

    }
}
