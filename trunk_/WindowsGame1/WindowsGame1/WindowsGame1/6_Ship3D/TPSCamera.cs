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
    class TPSCamera:BasicCamera
    {
        //desafio: lista de cameras com uma camera ativa mudando no tab, usar BasicCamera.list

        //bool retro = false;//argh!

        public Vector3 distanceToPlayer;
        public Vector3 distanceToTarget;

        public TPSCamera():base()
        {
            distanceToPlayer = new Vector3(0, 10, 15);
            distanceToTarget = new Vector3(0, 5, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerRotationAngleDegrees"></param>
        /// <param name="playerPosition"></param>
        public void Update(float playerRotationAngleDegrees, Vector3 playerPosition)
        {
            Matrix rotationMatrix = Matrix.CreateRotationY(playerRotationAngleDegrees);

            Vector3 correctDistance = Vector3.Transform(distanceToPlayer, rotationMatrix);
            Vector3 correctTarget = Vector3.Transform(distanceToTarget, rotationMatrix);

            Position = playerPosition + correctDistance;
            Target = playerPosition + correctTarget;

            ViewMatrix = Matrix.CreateLookAt(Position, Target, Orientation);

            ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(ApertureAngle),
                ScreenRatio,
                NearPlane,
                FarPlane);

            //base.Update();
        }

        public override void Update(GameTime gameTime)
        {
            if(Game1.Instance.keyboardWasPressed(Keys.Up))
            {
                Target = new Vector3(0, 0, -1);
            }
            if(Game1.Instance.keyboardWasPressed(Keys.Down))
            {
                Target = new Vector3(0, 0, 1);
            }
            if(Game1.Instance.keyboardWasPressed(Keys.Left))
            {
                Target = new Vector3(-1, 0, 0);
            }
            if(Game1.Instance.keyboardWasPressed(Keys.Right))
            {
                Target = new Vector3(1, 0, 0);
            }
            base.Update();
        }

        public void Update(GameTime gameTime, bool retro)
        {
            if (Game1.Instance.keyboardWasPressed(Keys.Up))
            {
                Target = new Vector3(0, 0, 1);
            }
            if (Game1.Instance.keyboardWasPressed(Keys.Down))
            {
                Target = new Vector3(0, 0, -1);
            }
            if (Game1.Instance.keyboardWasPressed(Keys.Left))
            {
                Target = new Vector3(1, 0, 0);
            }
            if (Game1.Instance.keyboardWasPressed(Keys.Right))
            {
                Target = new Vector3(-1, 0, 0);
            }
            base.Update();
        }

        //public void Update(GameTime gameTime, KeyboardState ks)
        //{
        //    base.Update();
        //}
    }
}

