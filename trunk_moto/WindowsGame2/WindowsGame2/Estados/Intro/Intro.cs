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

namespace MotoGame.Estados.Intro
{
    class Intro:EstadoBase
    {
        KeyboardState teclado_atual;
        Video video;
        VideoPlayer videoPlayer;

        public Intro(ContentManager Content)
            :base(Content)
        {
            //video = Content.Load<Video>("LunarLander3D");
            //http://bit.ly/seven000018

            videoPlayer = new VideoPlayer();
            //videoPlayer.Play(video);
        }

        public override void Update(GameTime gameTime)
        {
            teclado_atual = Keyboard.GetState();

            if (teclado_atual.IsKeyDown(Keys.Enter)) 
            {
                videoPlayer.Stop();
                Game1.estado_atual = Game1.Estado.JOGO;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(videoPlayer.GetTexture(), new Rectangle(0,0,800,480), Color.White);
        }

    }
}
