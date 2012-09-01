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

namespace Pacman.GameObjects
{
    class GameCharacter
    {
        Texture2D texture;
        Vector2 position;
        Vector2 center;
        float speed;
        float rotation;

        //set up the frames for the animation
        Rectangle[] frames;
        float frameLength = 1f / 10f; //5 frames a second
        int currentFrame = 0;

        public GameCharacter(Texture2D t, int numFrames, int xOffset, int yOffset)
        {
            texture = t;
            frames = new Rectangle[numFrames];
            int frameWidth = texture.Width / numFrames;
            for (int i = 0; i < numFrames; i++)
            {
                frames[i] = new Rectangle(xOffset + (frameWidth * i),
                    yOffset,
                    frameWidth,
                    texture.Height);
            }
            speed = 8.0f;
            rotation = 0.0f;
            center = new Vector2(frameWidth / 2, texture.Height / 2);
            position = new Vector2(100.0f, 100.0f);
        }

        //accessor  methods
        public Vector2 GetCenter()
        {
            return center;
        }

        public Rectangle GetCurrentFrame()
        {
            return frames[currentFrame];
        }

        public int GetCurrentFrameNumber()
        {
            return currentFrame;
        }

        public float GetFrameLength()
        {
            return frameLength;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public float GetRotation()
        {
            return rotation;
        }

        public Texture2D GetTexture()
        {
            return texture;
        }

        //modifier methods
        public void MoveDown()
        {
            position.Y = position.Y + speed;
            //also change rotation
            rotation = MathHelper.PiOver2;
        }

        public void MoveLeft()
        {
            position.X = position.X - speed;
            rotation = MathHelper.Pi;
        }

        public void MoveRight()
        {
            System.Diagnostics.Debug.WriteLine("Right Key");
            position.X += speed;
            rotation = 0.0f;
        }

        public void MoveUp()
        {
            position.Y = position.Y - speed;
            rotation = MathHelper.PiOver2 * 3;
        }

        public void SetCurrentFrameNumber()
        {
            currentFrame = (currentFrame + 1) % frames.Length;
        }

        public void SetPosition(Vector2 pos)
        {
            position = pos;
        }
    }
}
