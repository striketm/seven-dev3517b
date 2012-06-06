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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

  int frameatual = 0;
        //Lista de Explosões
        Texture2D explosao;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
           
            

            
			
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            explosao = Content.Load<Texture2D>(@"Explosao");

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

        if (frameatual <= 6 )
        {
      //  System.Threading.Thread.Sleep(100);
            spriteBatch.Draw(explosao, Vector2.Zero, new Rectangle(frameatual *
                       explosao.Width / 7, 0, explosao.Width / 7,
                       explosao.Height), Color.White);


            //spriteBatch.Draw(explosao, Vector2.Zero, new Rectangle(65,0, explosao.Width / 7,
                      // explosao.Height), Color.White);

           // spriteBatch.Draw(explosao, Vector2.Zero, null, Color.White);

           

          //  spriteBatch.Draw(sprite, vetor, new Rectangle(spriteAtual.X * sprite.Width / 8, spriteAtual.Y * sprite.Height / 2, sprite.Width / 8, sprite.Height / 2), Color.White);


            ++frameatual;
        }
        else
        {frameatual = 0 ; }
          //  spriteBatch.Draw(explosao,Vector2.Zero, Color.White);

            spriteBatch.End();

           
            base.Draw(gameTime);
        }
    }
}
