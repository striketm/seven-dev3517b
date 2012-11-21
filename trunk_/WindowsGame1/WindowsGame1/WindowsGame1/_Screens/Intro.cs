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

namespace WindowsGame1
{
    class Intro:GameScreen
    {
        //webcam capture
        //http://xboxforums.create.msdn.com/forums/p/45450/271562.aspx

        //video formats
        //http://msdn.microsoft.com/en-us/library/dd254869.aspx

        //play video on a surface
        //http://msdn.microsoft.com/en-us/library/dd904199.aspx

        //remember to add the dll in some instalations!
        Video video;

        //other name for this class/component text?
        Menu menu;
        Menu menu2notactive;

        public int SelectedIndex
        {
            get { return menu.SelectedIndex; }
            set { menu.SelectedIndex = value; }
        }

        string[] menuItems = { "Skip Intro" };
        string[] menuItems2notactive = { "Press Enter" };

        public Intro(Game1 game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            menu = new Menu(game, spriteBatch, Game1.Instance.fontArial24, menuItems);
            menu2notactive = new Menu(game, spriteBatch, Game1.Instance.fontArial24, menuItems2notactive);

            menu2notactive.offSet = new Vector2(0, 100);
            menu2notactive.active = false;

            Components.Add(menu);
            Components.Add(menu2notactive);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
