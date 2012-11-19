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

namespace WindowsGame1._6_Ship3D
{
    class Ship3D
    {
        SpaceShip3D ship3d;

        BasicCamera camera;

        public Ship3D(ContentManager Content)
        {
            //modelos básicos vindos da unity:

            ship3d = new SpaceShip3D(Content.Load<Model>("6_Ship3D/Space Shooter/Space_Shooter"));

            //ship3d = new SpaceShip3D(Content.Load<Model>("6_Ship3D/Probe/probe"));

            //ship3d = new SpaceShip3D(Content.Load<Model>("6_Ship3D/F1 cars/ferrari"));

            //ship3d = new SpaceShip3D(Content.Load<Model>("6_Ship3D/nave"));

            //ship3d.world = Matrix.CreateScale(0.25f);

            camera = new BasicCamera();
        }

        public void Update()
        {

        }

        public void Draw()
        {
            ship3d.Draw(camera);
            //ship3d.ModelDraw(camera);
        }

    }
}
