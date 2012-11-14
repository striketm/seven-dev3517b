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

namespace Primitiva3D
{
    /// <summary>
    /// 
    /// </summary>
    class Camera
    {
        /// <summary>
        /// 
        /// </summary>
        Matrix view;

        /// <summary>
        /// 
        /// </summary>
        Matrix projection;

        /// <summary>
        /// 
        /// </summary>
        Vector3 position;

        /// <summary>
        /// 
        /// </summary>
        Vector3 lookAt;

        /// <summary>
        /// 
        /// </summary>
        Vector3 orientation;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="lookAt"></param>
        /// <param name="orientation"></param>
        public Camera(Vector3 position, Vector3 lookAt, Vector3 orientation, GraphicsDeviceManager gd)
        {
            this.position = position;
            this.lookAt = lookAt;
            this.orientation = orientation;
            
            projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(90.0f),
                gd.GraphicsDevice.Viewport.AspectRatio,
                1.0f,
                1000.0f);

            Update();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Update()
        {
            //se mexer...
            view = Matrix.CreateLookAt(
                position,
                lookAt,
                orientation);
        }
    }
}
