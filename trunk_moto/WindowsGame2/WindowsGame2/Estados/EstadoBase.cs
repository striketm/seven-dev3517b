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

namespace MotoGame.Estados
{
    abstract class EstadoBase
    {
        public GameWindow Window;
        public Texture2D fundo;
        public string caminho;
        public Song musica_fundo;

        public EstadoBase(ContentManager Content, GameWindow Window)
        {
            this.Window = Window;
        }
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
