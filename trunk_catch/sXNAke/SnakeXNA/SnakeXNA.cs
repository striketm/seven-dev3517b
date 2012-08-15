using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace SnakeXNA
{
    static class Program
    {
        static void Main(string[] args)
        {
            using (SnakeXNA game = new SnakeXNA())
            {
                game.IsFixedTimeStep = true;
                game.TargetElapsedTime = new TimeSpan(500000);
                game.Run();
            }
        }
    }

    public class SnakeXNA : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private static Random myRnd = new Random();
        private Point apple = new Point(myRnd.Next(64 - 1) * 10, myRnd.Next(48 - 1) * 10);
        private Point dir = new Point(0, 10);
        private LinkedList<Point> snake = new LinkedList<Point>(); //LinkedList doesn't have add...
        private Texture2D mySquare; //In XNA we need a texture to draw stuff with
        private Boolean isRunning = true;

        public SnakeXNA()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            snake.AddFirst(new Point(10, 10));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mySquare = Content.Load<Texture2D>("GameThumbnail");  
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState myState = Keyboard.GetState();
            
            if (myState.IsKeyDown(Keys.Left)) { dir = new Point(-10, 0); }
            if (myState.IsKeyDown(Keys.Right)) { dir = new Point(10, 0); }
            if (myState.IsKeyDown(Keys.Up)) { dir = new Point(0, -10); }
            if (myState.IsKeyDown(Keys.Down)) { dir = new Point(0, 10); }

            if (isRunning)
            {
                snake.AddFirst(new Point(snake.First.Value.X + dir.X, snake.First.Value.Y + dir.Y));
                snake.RemoveLast();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
            spriteBatch.Draw(mySquare, new Rectangle(apple.X + 2, apple.Y + 2, 5, 5), Color.Green); //Draw apple.

            int i = 0;
            foreach (Point cur in snake)  //Draw snake and check self collision
            {
                spriteBatch.Draw(mySquare, new Rectangle(cur.X , cur.Y , 10, 10), Color.White);
                if (i != 0 && snake.First.Value.X == cur.X && snake.First.Value.Y == cur.Y) { isRunning = false ; }
                i++;
            }

            if (snake.First.Value.X == apple.X && snake.First.Value.Y == apple.Y) //Collision apple
            {
                apple = new Point((myRnd.Next(64 - 1)) * 10, (myRnd.Next(48 - 1)) * 10);
                snake.AddLast(snake.Last.Value);
            }
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}