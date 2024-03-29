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

namespace RPG_The_Game.Cenas
{
    class Cenario
    {
        Texture2D imagem;
        Vector2 quadro;

        public Cenario(Texture2D imagem)
        {
            this.imagem = imagem;
            this.quadro = new Vector2(0, 0);
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(imagem,
               new Rectangle(0, 0,
                   imagem.Width, imagem.Height),
               new Rectangle((int)quadro.X, (int)quadro.Y,
                   imagem.Width, imagem.Height),
                   Color.White);
        }
    }
}
