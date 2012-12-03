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
    class Ship3D
    {
        SpaceShip3D spaceShooter;
        SpaceShip3D probe;
        SpaceShip3D starFox;
        SpaceShip3D ferrari;

        SpaceShip3D player;
        //SpaceShip3D floor;

        TPSCamera camera;

        Viewport viewportPadrao;
        Viewport viewportRetrovisor;
        //TPSCamera cameraRetrovisor;

        public Ship3D(ContentManager Content)
        {
            //modelos básicos vindos da unity:

            spaceShooter = new SpaceShip3D(Content.Load<Model>("6_Ship3D/Space Shooter/Space_Shooter"));
            probe = new SpaceShip3D(Content.Load<Model>("6_Ship3D/Probe/probe"));
            starFox = new SpaceShip3D(Content.Load<Model>("6_Ship3D/starfox"));
            ferrari = new SpaceShip3D(Content.Load<Model>("6_Ship3D/F1 cars/ferrari"));

            player = new SpaceShip3D(Content.Load<Model>("6_Ship3D/jogador"));
            //floor = new SpaceShip3D(Content.Load<Model>("6_Ship3D/chao"));

            starFox.Scale = 0.5f;

            ferrari.Scale = 4.0f;
            ferrari.RotationY = -90.0f;
            //ferrari.RotationX = -90.0f;

            spaceShooter.Position = new Vector3(0, 0, 20);//trás
            probe.Position = new Vector3(20, 0, 0);//direita
            starFox.Position = new Vector3(-20, 0, 0);//esquerda
            ferrari.Position = new Vector3(0, 0, -20);//frente

            player.Position = new Vector3(0, 0, 0);//frente
            player.Scale = 0.1f;

            //floor.Position = new Vector3(0, -10, 0);//frente

            //ship3d = new SpaceShip3D(Content.Load<Model>("6_Ship3D/Probe/probe"));

            //ship3d = new SpaceShip3D(Content.Load<Model>("6_Ship3D/F1 cars/ferrari"));

            //ship3d = new SpaceShip3D(Content.Load<Model>("6_Ship3D/nave"));

            //ship3d.world = Matrix.CreateScale(0.25f);

            camera = new TPSCamera();

            camera.Position = new Vector3(0, 15, 20);
            camera.Target = player.Position;

            viewportPadrao = Game1.Instance.GraphicsDevice.Viewport;
            viewportRetrovisor = new Viewport(0, 0, 160, 120);

            //cameraRetrovisor = new TPSCamera();
            //cameraRetrovisor.Position = camera.Position;//?
            //cameraRetrovisor.ViewMatrix = camera.ViewMatrix;//?
            //cameraRetrovisor.ProjectionMatrix = camera.ProjectionMatrix;//?
            //cameraRetrovisor.Target = new Vector3(0, 0, 1);

        }

        public void Update(GameTime gameTime)
        {
            //spaceShooter.Update(gameTime);
            //probe.Update(gameTime);
            //starFox.Update(gameTime);
            //ferrari.Update(gameTime);

            foreach (GameObject3D gO3D in GameObject3D.list)
            {
                gO3D.Update(gameTime);
            }

            //camera.Update(gameTime);
            //cameraRetrovisor.Update(gameTime, true);

            camera.Update(player.RotationY, player.Position);

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //spaceShooter.Draw(camera);
            //probe.Draw(camera);
            //starFox.Draw(camera);
            //ferrari.Draw(camera);

            RasterizerState wireFrame = new RasterizerState();
            wireFrame.FillMode = FillMode.WireFrame;
            RasterizerState solidFrame = new RasterizerState();
            solidFrame.FillMode =  FillMode.Solid;
            RasterizerState noCulling = new RasterizerState();
            noCulling.CullMode = CullMode.None;
            RasterizerState normalCulling = new RasterizerState();
            normalCulling.CullMode = CullMode.CullCounterClockwiseFace;
            RasterizerState wireFrameNoCulling = new RasterizerState();
            wireFrameNoCulling.FillMode = FillMode.WireFrame;
            wireFrameNoCulling.CullMode = CullMode.None;
            RasterizerState wireFrameNormalCulling = new RasterizerState();
            wireFrameNormalCulling.FillMode = FillMode.WireFrame;
            wireFrameNormalCulling.CullMode = CullMode.CullCounterClockwiseFace;

            //Game1.Instance.GraphicsDevice.RasterizerState = wireFrame;

            Game1.Instance.GraphicsDevice.Viewport = viewportPadrao;

            //TO DO HUD spriteBatch

            //spriteBatch.Begin();

                spriteBatch.Draw(Game1.Instance.Content.Load<Texture2D>("GameThumbnail"), new Vector2(300, 0), Color.White);

            //spriteBatch.End();

            foreach(GameObject3D gO3D in GameObject3D.list)
            {
                gO3D.Draw(camera);
            }

            Game1.Instance.GraphicsDevice.Viewport = viewportRetrovisor;

            foreach (GameObject3D gO3D in GameObject3D.list)
            {
                //gO3D.Draw(cameraRetrovisor);
            }

            //ship3d.ModelDraw(camera);

            Game1.Instance.GraphicsDevice.Viewport = viewportPadrao;
        }

    }
}

