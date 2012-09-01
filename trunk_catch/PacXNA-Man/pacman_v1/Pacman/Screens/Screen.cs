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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Pacman.Screens
{
    class Screen
    {
        protected EventHandler screenEvent;

        public Screen(EventHandler sEvent)
        {
            screenEvent = sEvent;
        }

        public virtual void Update(GameTime gametime)
        {

        }

        public virtual void Draw(SpriteBatch batch)
        {

        }
    }
}
