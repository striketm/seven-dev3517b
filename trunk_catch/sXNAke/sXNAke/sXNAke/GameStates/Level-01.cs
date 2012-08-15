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

        Snake ivan;

        Snake victor;

        Song bg_Music;

        Pill pill;

        #endregion
        public Level_01(ContentManager Content, GameWindow Window)
        {
            ivan = new Snake(Content.Load<Texture2D>("snake"),
              Window);

            victor = new Snake(Content.Load<Texture2D>("snake"),
                Window);


            pill = new Pill(
                Content.Load<Texture2D>("snake"),
                Content.Load<SoundEffect>("pillSound"),
                Window);

            bg_Music = Content.Load<Song>("BG_01");


            MediaPlayer.Play(bg_Music);
            MediaPlayer.Volume = 0.2f;

        }
        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            ivan.Update(keyboardState);
            //victor.Update(keyboardState);//TODO E para fazer o segundo jogador responder à teclas diferentes?

            pill.Update(ivan.CollisionRect);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {            
            ivan.Draw(spriteBatch);
            victor.Draw(spriteBatch);
            pill.Draw(spriteBatch);
        }
    }
}
