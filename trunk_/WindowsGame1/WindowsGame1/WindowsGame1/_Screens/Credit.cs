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
    class Credit : GameScreen
    {
        Menu menu;

        public int SelectedIndex
        {
            get { return menu.SelectedIndex; }
            set { menu.SelectedIndex = value; }
        }

        public Credit(Game1 game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            string[] menuItems = { "Cleber Tavares Jr." };

            menu = new Menu(game, spriteBatch, Game1.Instance.fontArial24, menuItems);

            Components.Add(menu);
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
