﻿using System;
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
    class Menu : DrawableGameComponent
    {
        string[] menuItems;
        int selectedIndex;
        Color normal = Color.White;
        Color hilite = Color.Yellow;
        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        Vector2 position;
        float width = 0f;
        float height = 0f;

        public Vector2 offSet;
        public bool active;

        public Menu(Game game,
                            SpriteBatch spriteBatch,
                            SpriteFont spriteFont,
                            string[] menuItems)
            : base(game)
        {
            this.active = true;

            this.spriteBatch = spriteBatch;
            this.spriteFont = spriteFont;
            this.menuItems = menuItems;

            MeasureMenu();
        }

        private void MeasureMenu()
        {
            height = 0;
            width = 0;
            foreach (string item in menuItems)
            {
                Vector2 size = spriteFont.MeasureString(item);
                if (size.X > width)
                    width = size.X;
                height += spriteFont.LineSpacing + 5;
            }
            position = new Vector2(
            (Game.Window.ClientBounds.Width - width) / 2,
            (Game.Window.ClientBounds.Height - height) / 2);           
        }

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                if (selectedIndex < 0)
                    selectedIndex = 0;
                if (selectedIndex >= menuItems.Length)
                    selectedIndex = menuItems.Length - 1;
            }
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        private bool CheckKey(Keys theKey)//use the one in game 1...
        {
            return keyboardState.IsKeyUp(theKey) &&
            oldKeyboardState.IsKeyDown(theKey);
        }

        public override void Update(GameTime gameTime)
        {
            if (!active) return;//

            keyboardState = Keyboard.GetState();//use the one in game 1...

            if (CheckKey(Keys.Down))
            {
                selectedIndex++;
                if (selectedIndex == menuItems.Length)
                    selectedIndex = 0;
            }

            if (CheckKey(Keys.Up))
            {
                selectedIndex--;
                if (selectedIndex < 0)
                    selectedIndex = menuItems.Length - 1;
            }

            base.Update(gameTime);//makes difference the order?

            oldKeyboardState = keyboardState;//use the one in game 1...
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);//makes difference the order???????????????????????

            Vector2 location = position + offSet;//why here? move the menu?

            Color tint;

            for (int i = 0; i < menuItems.Length; i++)
            {
                if ((i == selectedIndex)&&(active))
                    tint = hilite;
                else
                    tint = normal;

                spriteBatch.DrawString(
                spriteFont,
                menuItems[i],
                location,
                tint);
                location.Y += spriteFont.LineSpacing + 5;
            }
        }

    }
}
