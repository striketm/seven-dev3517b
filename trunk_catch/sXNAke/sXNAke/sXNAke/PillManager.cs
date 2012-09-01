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

namespace sXNAke
{

    class PillManager
    {
        Texture2D pillTexture;
        SoundEffect pillSound;
        GameWindow Window;
        
        public static List<Pill> pills = new List<Pill>();

        const int MAX_PILLS = 50;

        public PillManager(ContentManager Content, GameWindow Window)
        {

            this.pillTexture = Content.Load<Texture2D>("snake");
            this.pillSound = Content.Load<SoundEffect>("pillSound");
            this.Window = Window;
        }
        public void Update(GameTime gameTime, Snake snake)
        {
            foreach (Pill pill in pills)
            {
                pill.Update(snake);
            }

            if (pills.Count <= MAX_PILLS)
            {
                pills.Add(new Pill(pillTexture,pillSound,Window));
            }
            for (int k = 0; k < pills.Count; k++)
            {
                if(pills[k].Visible == false)
                {
                    pills.RemoveAt(k);
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Pill pill in pills)
            {
                pill.Draw(gameTime, spriteBatch);
            }
 
        }
    }
    
}
