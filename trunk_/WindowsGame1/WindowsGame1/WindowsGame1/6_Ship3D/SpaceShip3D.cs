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
using WindowsGame1._Core;

namespace WindowsGame1._6_Ship3D
{
    class SpaceShip3D:GameObject3D
    {
        public SpaceShip3D(Model model):base(model)
        {

        }

        public override void Update(GameTime gameTime)
        {

            base.UpdateWorldAndCollisions();
        }
    }
}
