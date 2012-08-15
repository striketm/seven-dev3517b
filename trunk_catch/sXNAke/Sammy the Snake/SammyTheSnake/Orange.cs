using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace SammyTheSnake
{
	/// <summary>
	/// Represents the elusive orange.
	/// </summary>
	public class Orange
	{
		// the position of the orange
		public Point Position;

		// the orange's texture
		private Texture2D texture;

		// random number generator for positioning the orange.
		private Random rand = new Random();

		/// <summary>
		/// Creates a new Orange.
		/// </summary>
		public Orange()
		{
			Position = new Point(Grid.MaxColumn / 2, Grid.MaxRow / 2);
		}

		/// <summary>
		/// Moves the orange to a space not occupied by the snake.
		/// </summary>
		/// <param name="snake">The snake instance.</param>
		public void Reposition(Snake snake)
		{
			// we use a do-while loop to randomly generate points until
			// we find one that isn't on the snake.
			do
			{
				Position = new Point(rand.Next(Grid.MaxColumn), rand.Next(Grid.MaxRow));
			} while (snake.IsBodyOnPoint(Position));
		}

		/// <summary>
		/// Loads the orange sprite.
		/// </summary>
		/// <param name="content">The game's ContentManager.</param>
		public void Load(ContentManager content)
		{
			// load in the texture.
			texture = content.Load<Texture2D>("Sprites/Orange");
		}

		/// <summary>
		/// Draws the orange.
		/// </summary>
		/// <param name="spriteBatch">The SpriteBatch to use for rendering</param>
		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();
			Grid.DrawSprite(spriteBatch, texture, Position, 0f);
			spriteBatch.End();
		}
	}
}
