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

        public TPSCamera():base()
        {

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
