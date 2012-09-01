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
       public void Update(GameTime gameTime)
        {
            if (Game1.keyboardState.IsKeyDown(Keys.Enter) &&
                !Game1.oldKeyboardState.IsKeyDown(Keys.Enter)||
                (Game1.mouseState.LeftButton == ButtonState.Pressed &&
                Game1.oldMouseState.LeftButton != ButtonState.Pressed))
            {
                Game1.present_State = Game1.States.LEVEL_01;
            }
        }

       public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.arial20, Game1.playInstructions, new Vector2(320, 200), Color.Red);
        }
    }
}
