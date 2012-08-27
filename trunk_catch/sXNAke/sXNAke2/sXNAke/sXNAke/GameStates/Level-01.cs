using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace sXNAke.GameStates
{
    class Level_01
    {
        #region Atributos

        Snake snake_1;

        //Snake snake_2;
        //Snake snake_3;
        //Snake snake_4;

        Song bg_Music;

        Pill pill;

        #endregion
        public Level_01(ContentManager Content, GameWindow Window)
        {
            snake_1 = new Snake(Content.Load<Texture2D>("snake"), Content.Load<Texture2D>("snake"),
              Content.Load<Texture2D>("snake"), Window, "1");
            snake_1.hudPosition = Vector2.Zero;
            snake_1.hudColor = Color.Red;

            //snake_2 = new Snake(Content.Load<Texture2D>("snake"), Content.Load<Texture2D>("snake"),
            //  Content.Load<Texture2D>("snake"), Window, "2");
            //snake_2.hudPosition = new Vector2(200,0);
            //snake_2.hudColor = Color.Green;

            pill = new Pill(Content.Load<Texture2D>("snake"),
                Content.Load<SoundEffect>("pillSound"),
                Window);

            bg_Music = Content.Load<Song>("BG_01");

            MediaPlayer.Play(bg_Music);
            MediaPlayer.Volume = 0.2f;

        }
        public void Update(GameTime gameTime)
        {
            snake_1.Update(gameTime, Game1.keyboardState);
            
            //snake_2.Update(gameTime, Game1.keyboardState);
            
            pill.Update(snake_1.CollisionRect);
            
            //pill.Update(snake_2.CollisionRect);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {            
            snake_1.Draw(gameTime, spriteBatch);
            
            //snake_2.Draw(gameTime, spriteBatch);
            
            pill.Draw(gameTime, spriteBatch);
        }
    }
}
