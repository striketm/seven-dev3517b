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

namespace MotoGame.Estados.Intro
{
    class Easter : EstadoBase
    {
        int Y = 0;

        public Easter(ContentManager Content, GameWindow Window)
            : base(Content, Window)
        {
            fundo = Content.Load<Texture2D>("Easter/Textures/_CENSO-GAMER");
        }

        public override void Update(GameTime gameTime)
        {
            if (Game1.teclado_atual.IsKeyDown(Keys.E) && Game1.teclado_anterior.IsKeyUp(Keys.E))
            {
                Game1.estado_atual = Game1.Estado.FASE1;
            }

            if (Game1.teclado_atual.IsKeyDown(Keys.Down)&&Y<fundo.Bounds.Height-Window.ClientBounds.Height)
            {
                Y+=5;
            }

            if (Game1.teclado_atual.IsKeyDown(Keys.Up)&&Y>0)
            {
                Y -= 5;
            }

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(fundo, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), new Rectangle(0, Y, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);
        }

    }
}
