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
using Pacman.GameObjects;

namespace Pacman.Screens
{
    class GameScreen : Screen
    {
        GameCharacter pacman;

        float frameTimer = 0f;

        public GameScreen(ContentManager manager, EventHandler gameEvent, GraphicsDeviceManager graphics)
            : base(gameEvent)
        {
            pacman = new GameCharacter(
                manager.Load<Texture2D>("graphics\\pac_anim"), 4, 0, 0);
        }

        public override void Update(GameTime gametime)
        {
            UpdateCharacter(gametime);

#if !XBOX

            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                pacman.MoveRight();
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                pacman.MoveLeft();
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                pacman.MoveUp();
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                pacman.MoveDown();
            }

#endif

            base.Update(gametime);
        }

        protected void UpdateCharacter(GameTime gametime)
        {
            frameTimer += (float)gametime.ElapsedGameTime.TotalSeconds;

            if (frameTimer >= pacman.GetFrameLength())
            {
                frameTimer = 0f;
                pacman.SetCurrentFrameNumber();
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            
            batch.Draw(pacman.GetTexture(),
                pacman.GetPosition(),
                pacman.GetCurrentFrame(),
                Color.White,
                pacman.GetRotation(),
                pacman.GetCenter(),
                1.0f,
                SpriteEffects.None,
                0);

            base.Draw(batch);
        }
    }
}
