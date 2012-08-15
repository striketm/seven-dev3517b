using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SammyTheSnake
{
	/// <summary>
	/// Contains values and methods used by the game to render sprites to our grid.
	/// </summary>
	public static class Grid
	{
		// the size of the grid
		public const int Scale = 16;

		// half of the size of the grid
		public const int HalfScale = Scale / 2;

		// the largest coordinate values that can be used
		public const int MaxColumn = 240 / Scale;
		public const int MaxRow = 320 / Scale;

		/// <summary>
		/// Converts a point in our grid to a Vector2 based on the global scale.
		/// </summary>
		/// <param name="p">The point to convert.</param>
		/// <returns>A Vector2 representing that point in the global scale.</returns>
		public static Vector2 PointToVector2(Point p)
		{
			// multiply the point by our scale and then add half of the scale
			return new Vector2(
				p.X * Scale + HalfScale, 
				p.Y * Scale + HalfScale);
		}

		/// <summary>
		/// Draws a sprite.
		/// </summary>
		/// <param name="spriteBatch">The SpriteBatch with which to draw the sprite.</param>
		/// <param name="texture">The texture to draw.</param>
		/// <param name="point">The cell in which to draw the sprite.</param>
		/// <param name="rotation">The rotation of the sprite.</param>
		public static void DrawSprite(SpriteBatch spriteBatch, Texture2D texture, Point point, float rotation)
		{
			// determine the largest dimension of the texture
			float spriteSize = Math.Max(texture.Width, texture.Height);

			// call the spriteBatch.Draw method using the parameters and calculated values.
			// by rendering sprites like this, we make sure that each one is scaled and positioned
			// based on our global scale.
			spriteBatch.Draw(
				texture,
				PointToVector2(point),
				null,
				Color.White,
				rotation,
				new Vector2(texture.Width / 2f, texture.Height / 2f),
				Scale / spriteSize,
				SpriteEffects.None,
				0);
		}
	}
}
