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

namespace WindowsGame1
{
    abstract class Sprite
    {
        /// <summary>
        /// 
        /// </summary>
        Texture2D texture;

        /// <summary>
        /// 
        /// </summary>
        Vector2 position;

        /// <summary>
        /// 
        /// </summary>
        Rectangle frame;

        //ANIMATION!!!

        /// <summary>
        /// 
        /// </summary>
        Rectangle destination;

        /// <summary>
        /// 
        /// </summary>
        Rectangle collision;

        /// <summary>
        /// 
        /// </summary>
        Color color;

        /// <summary>
        /// 
        /// </summary>
        float rotation;

        /// <summary>
        /// 
        /// </summary>
        Vector2 pivot;

        /// <summary>
        /// 
        /// </summary>
        Vector2 scale;

        /// <summary>
        /// 
        /// </summary>
        float layer;

        /// <summary>
        /// 
        /// </summary>
        bool right;

        /// <summary>
        /// 
        /// </summary>
        bool up;


        /// <summary>
        /// 
        /// </summary>
        public Sprite()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        abstract public void Update();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteBatch"></param>
        protected void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(
        }


    }
}
