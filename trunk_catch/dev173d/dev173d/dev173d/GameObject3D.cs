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

namespace dev173d
{
    class GameObject3D
    {
        GraphicsDevice graphicsDevice;

        Model model;

        BasicEffect basicEffect;

        Matrix world;

        float rotationX;
        float rotationY;
        float rotationZ;
        float scaleX;
        float scaleY;
        float scaleZ;
        float positionX;
        float positionY;
        float positionZ;

        Matrix orientation;

        public GameObject3D(GraphicsDevice graphicsDevice)
        {
            this.basicEffect = new BasicEffect(graphicsDevice);

            this.world = Matrix.Identity;

            UpdateWorld();
        }

        public void Update(GameTime gameTime)
        {
            UpdateWorld();
        }

        void UpdateWorld()
        {
            this.world =
                Matrix.CreateRotationX(MathHelper.ToRadians(rotationX)) *
                Matrix.CreateRotationY(MathHelper.ToRadians(rotationY)) *
                Matrix.CreateRotationZ(MathHelper.ToRadians(rotationZ)) *
                Matrix.CreateScale(scaleX, scaleY, scaleZ) *
                Matrix.CreateTranslation(positionX, positionY, positionZ);
        }

        public void Draw(GameTime gameTime, BasicCamera camera)
        {
            model.Draw(this.world, camera.ViewMatrix, camera.ProjectionMatrix);
        }

    }
}
