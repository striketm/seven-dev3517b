using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;

namespace sXNAke.GameStates
{
    class Menu
    {
        public Menu(ContentManager Content, GameWindow Window)
        {  
        }
       public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                Game1.present_State = Game1.States.LEVEL_01;
            }
        }

       public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        { 
        }
    }
}
