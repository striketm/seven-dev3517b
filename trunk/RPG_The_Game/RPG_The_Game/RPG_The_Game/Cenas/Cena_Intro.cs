using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RPG_The_Game.Cenas
{

    public class Cena_Intro
    {

        Texture2D fundo;
        GameWindow Window;

        public Cena_Intro(ContentManager Content, GameWindow Window)
        {
            this.Window = Window;
            fundo = Content.Load<Texture2D>("Personagens/circuloTrigonometrico");
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(fundo, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);
        }

    }

}