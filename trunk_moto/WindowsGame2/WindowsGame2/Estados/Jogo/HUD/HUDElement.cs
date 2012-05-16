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

namespace MotoGame
{
    class HUDElement:Sprite
    {
        public HUDElement(ContentManager Content)
            :base(Content.Load<Texture2D>("seta"))
        {

        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
           
        }
    }
}
