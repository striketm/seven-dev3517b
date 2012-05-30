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

namespace MotoGame.Estados.Jogo
{
    class Fase1:EstadoBase
    {
        Moto moto1;
        
        Song musica;
        
        SpriteFont arial;
        
        GameWindow Window;

        bool firstTimeMusic = true;

        Texture2D cenario;
        Texture2D montanhas;
        Texture2D nuvens;
        Texture2D arbustos;

        public Fase1(ContentManager Content, GameWindow Window)
            :base(Content, Window)
        {
            this.Window = Window;

            moto1 = new Moto(Content, this.Window);

            musica = Content.Load<Song>("Sounds/Musics/music");

            arial = Content.Load<SpriteFont>("arial");
        }

        public override void Update(GameTime gameTime)
        {
            if (firstTimeMusic)
            {
                //MediaPlayer.Play(musica);
                firstTimeMusic = false;
            }

            moto1.Update(gameTime, Game1.teclado_atual, Game1.mouse_atual, Game1.gamepad_atual);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            moto1.Draw(gameTime, spriteBatch);
            
            spriteBatch.DrawString(arial, "Pontos: " + moto1.pontos, new Vector2(10, 10), Color.Red);

        }

    }
}
