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

namespace WindowsGame1._Screens//theres a difference in namespace here in relation to other classes in the folder!!! this one was created inside it already
{
    class PopUp : GameScreen//like that one... seven student...
    {
        Menu menuComponent;
        Texture2D image;
        Rectangle imageRectangle;
        public int SelectedIndex
        {
            get { return menuComponent.SelectedIndex; }
            set { menuComponent.SelectedIndex = value; }
        }
        public PopUp(Game game, SpriteBatch spriteBatch, SpriteFont
spriteFont, Texture2D image)
            : base(game, spriteBatch)
        {
            string[] menuItems = { "Yes", "No" };
            menuComponent = new Menu(game,
            spriteBatch,
            spriteFont,
            menuItems);
            Components.Add(menuComponent);
            this.image = image;
            imageRectangle = new Rectangle(
            (Game.Window.ClientBounds.Width - this.image.Width) / 2,
            (Game.Window.ClientBounds.Height - this.image.Height) / 2,
            this.image.Width,
            this.image.Height);
            menuComponent.Position = new Vector2(
            (imageRectangle.Width - menuComponent.Width) / 2,
            imageRectangle.Bottom - menuComponent.Height - 10);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(image, imageRectangle, Color.White);
            base.Draw(gameTime);
        }
    }
}
