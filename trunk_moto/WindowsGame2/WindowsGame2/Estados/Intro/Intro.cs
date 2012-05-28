﻿using System;
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
    class Intro:EstadoBase
    {

        public Intro(ContentManager Content, GameWindow Window)
            :base(Content, Window)
        {
            this.Window = Window;
            this.caminho = "Intro/";
            this.fundo = Content.Load<Texture2D>(caminho+"Textures/intro");
        }

        public override void Update(GameTime gameTime)
        {
            if (Game1.teclado_atual.IsKeyDown(Keys.Enter)) 
            {
                Game1.estado_atual = Game1.Estado.MENU;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(fundo, new Rectangle(0,0,Window.ClientBounds.Width,Window.ClientBounds.Height), Color.White);
        }

    }
}
