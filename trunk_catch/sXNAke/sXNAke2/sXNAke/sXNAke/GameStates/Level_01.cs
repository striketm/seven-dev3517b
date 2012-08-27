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
using sXNAke.GameStates;

namespace sXNAke.GameStates
{
    class Level_01
    {
        
        Snake ivan;
        Snake victor;

        Pill pill;

        Song bg_Music;

        public Level_01(ContentManager Content, GameWindow Window)
        {
            ivan = new Snake(Content.Load<Texture2D>("snake"),
               Window, "1");

            victor = new Snake(Content.Load<Texture2D>("snake"),
                Window, "2");

            pill = new Pill(
                Content.Load<Texture2D>("snake"),
                Content.Load<SoundEffect>("pillsound"),
                Window);

            bg_Music = Content.Load<Song>("nomedamusica");

            MediaPlayer.Play(bg_Music);
        }

        public void Update(KeyboardState keyboardState)
        {
            ivan.Update(keyboardState);
            victor.Update(keyboardState);//TODO E para fazer o segundo jogador responder à teclas diferentes?

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
