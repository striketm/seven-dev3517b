using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SammyTheSnake
{
	/// <summary>
	/// Defines a direction in which the snake can move.
	/// </summary>
	public enum Direction
	{
		Up,
		Down,
		Left,
		Right
	}

	/// <summary>
	/// Sammy the snake.
	/// </summary>
	public class Snake
	{
		/// <summary>
		/// Set the number of seconds it takes the snake to
		/// move one space.
		/// </summary>
		public const float MoveSpeed = .2f;

		// keeps track of elapsed time so we can match the speed
		private float moveTimer;

		// our four textures
		private Texture2D head, straight, angle, tail;

		// a list of points for the snake's body
		private List<Point> bodyPoints = new List<Point>();

		// the current direction in which the snake is moving
		private Direction currentDirection = Direction.Right;

		// the next direction in which the snake will move
		private Direction nextDirection = Direction.Right;

		// is the snake gaining a new body part this frame?
		private bool extending;

		public Snake()
		{
			// reset the snake to position him in the to-left corner
			Reset();
		}

		/// <summary>
		/// Resets the snake to the top-left corner of the screen.
		/// </summary>
		public void Reset()
		{
			// clear out the body points
			bodyPoints.Clear();

			// generate the starting snake in the top-left corner of the screen
			bodyPoints.Add(new Point(2, 0));
			bodyPoints.Add(new Point(1, 0));
			bodyPoints.Add(new Point(0, 0));

			// reset the direction to left
			currentDirection = Direction.Right;
			nextDirection = Direction.Right;
		}

		/// <summary>
		/// Loads all of the required sprites.
		/// </summary>
		/// <param name="content">The game's ContentManager.</param>
		public void Load(ContentManager content)
		{
			// load our sprites
			head = content.Load<Texture2D>("Sprites/Head");
			straight = content.Load<Texture2D>("Sprites/Straight");
			angle = content.Load<Texture2D>("Sprites/Angle");
			tail = content.Load<Texture2D>("Sprites/Tail");
		}

		/// <summary>
		/// Determines if a given point is on the snake's body.
		/// </summary>
		/// <param name="p">The point in question.</param>
		/// <returns>True if the point lies on the body; false otherwise.</returns>
		public bool IsBodyOnPoint(Point p)
		{
			// utilize the List<T>.Contains method
			return bodyPoints.Contains(p);
		}

		/// <summary>
		/// Determines if the snake's head is at a given point.
		/// </summary>
		/// <param name="p">The point to test.</param>
		/// <returns>True if the head is at the position; false otherwise.</returns>
		public bool IsHeadAtPosition(Point p)
		{
			return (bodyPoints[0] == p);
		}

		/// <summary>
		/// Determine if the snake has formed a loop and crashed into itself.
		/// </summary>
		/// <returns>True if the snake crashed into itself; false otherwise.</returns>
		public bool IsLooped()
		{
			// if any of the positions (besides index 0) equal the head's position,
			// that means our head has crashed into the body somewhere.
			for (int i = 1; i < bodyPoints.Count; i++)
			{
				if (bodyPoints[i] == bodyPoints[0])
				{
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Determines if the head is off the screen.
		/// </summary>
		/// <returns>True if the head is off the screen; false otherwise.</returns>
		public bool IsHeadOffScreen()
		{
			Point h = bodyPoints[0];

			return (h.X < 0 || h.Y < 0 || h.X >= Grid.MaxColumn || h.Y >= Grid.MaxRow);
		}

		/// <summary>
		/// Instructs the snake to extend next time it moves
		/// </summary>
		public void Extend()
		{
			extending = true;
		}

		/// <summary>
		/// Updates the snake.
		/// </summary>
		/// <param name="gameTime">The current game time stamp.</param>
		public void Update(GameTime gameTime)
		{
			// handle any input
			HandleInput();

			// increase the timer by the elapsed time
			moveTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

			// if we have not reached the MoveSpeed, we are done updating here
			if (moveTimer < MoveSpeed)
			{
				return;
			}

			// reset the timer
			moveTimer = 0f;

			// set the current direction to the next direction desired
			currentDirection = nextDirection;

			// move the snake along its path
			MoveSnake();
		}

		private void HandleInput()
		{
			// get the state of the GamePad (or Zune in our case)
			GamePadState gps = GamePad.GetState(PlayerIndex.One);

            KeyboardState kbs = Keyboard.GetState();

			// figure out which direction the user wants to go and if
			// the snake isn't heading in the opposite direction, set
			// that to be our next direction.
			if ((gps.IsButtonDown(Buttons.DPadDown)||kbs.IsKeyDown(Keys.Down)) && currentDirection != Direction.Up)
			{
				nextDirection = Direction.Down;
			}
			if ((gps.IsButtonDown(Buttons.DPadUp)||kbs.IsKeyDown(Keys.Up)) && currentDirection != Direction.Down)
			{
				nextDirection = Direction.Up;
			}
			if ((gps.IsButtonDown(Buttons.DPadLeft)||kbs.IsKeyDown(Keys.Left)) && currentDirection != Direction.Right)
			{
				nextDirection = Direction.Left;
			}
			if ((gps.IsButtonDown(Buttons.DPadRight)||kbs.IsKeyDown(Keys.Right)) && currentDirection != Direction.Left)
			{
				nextDirection = Direction.Right;
			}
		}

		private void MoveSnake()
		{
			// we need to store the original head position for moving the snake later
			Point p1 = bodyPoints[0];

			// determine which direction the snake is heading in and use
			// that to move the position of the head of the snake
			switch (currentDirection)
			{
				case Direction.Up:
					bodyPoints[0] = new Point(p1.X, p1.Y - 1);
					break;
				case Direction.Down:
					bodyPoints[0] = new Point(p1.X, p1.Y + 1);
					break;
				case Direction.Left:
					bodyPoints[0] = new Point(p1.X - 1, p1.Y);
					break;
				case Direction.Right:
					bodyPoints[0] = new Point(p1.X + 1, p1.Y);
					break;
			}

			// if the snake is extending this frame...
			if (extending)
			{
				// insert a new body part right behind the head
				bodyPoints.Insert(1, p1);

				// we no longer need to extend
				extending = false;

				// exit our Update method
				return;
			}

			// now we move the snake along by looping through all the body parts
			for (int i = 1; i < bodyPoints.Count; i++)
			{
				// store the body part's current position
				Point p2 = bodyPoints[i];

				// set the body part's position to the position of the body
				// part ahead of it
				bodyPoints[i] = p1;

				// save the body part's old position for updating the next body part
				p1 = p2;
			}
		}

		/// <summary>
		/// Draws the snake.
		/// </summary>
		/// <param name="spriteBatch">The game's SpriteBatch.</param>
		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();

			// draw the body
			for (int i = 1; i < bodyPoints.Count - 1; i++)
			{
				DrawBody(spriteBatch, bodyPoints[i], bodyPoints[i - 1], bodyPoints[i + 1]);
			}

			// draw the tail
			DrawTail(spriteBatch);

			// draw the head last so that it will always be on top in the case
			// of the snake intersecting itself
			DrawHead(spriteBatch);

			spriteBatch.End();
		}

		/// <summary>
		/// Draw's the snake's head.
		/// </summary>
		/// <param name="spriteBatch">The SpriteBatch to use when rendering.</param>
		private void DrawHead(SpriteBatch spriteBatch)
		{
			// get the head's point
			Point headPoint = bodyPoints[0];

			// get the point of the next body part
			Point nextBody = bodyPoints[1];

			// figure out the rotation for the head based on the positions
			// of the head and the next body part
			float rotation;
			if (headPoint.Y == nextBody.Y - 1)
			{
				rotation = -MathHelper.PiOver2;
			}
			else if (headPoint.Y == nextBody.Y + 1)
			{
				rotation = MathHelper.PiOver2;
			}
			else if (headPoint.X == nextBody.X - 1)
			{
				rotation = MathHelper.Pi;
			}
			else
			{
				rotation = 0f;
			}

			// draw the head using the global DrawSprite method.
			Grid.DrawSprite(spriteBatch, head, headPoint, rotation);
		}

		/// <summary>
		/// Draw's a piece of the snake's body.
		/// </summary>
		/// <param name="spriteBatch">The SpriteBatch to use when rendering.</param>
		/// <param name="current">The current body point.</param>
		/// <param name="last">The previous body point.</param>
		/// <param name="next">The next body point.</param>
		private void DrawBody(SpriteBatch spriteBatch, Point current, Point last, Point next)
		{
			// if the current, last, and next points create a 90 degree angle, we must
			// draw an angle piece.
			if ((current.X == last.X && current.X != next.X && current.Y != last.Y) ||
				(current.X == next.X && current.X != last.X && current.Y != next.Y))
			{
				// draw the sprite using the GetAngleRotation method to calculate the rotation
				Grid.DrawSprite(spriteBatch, angle, current, GetAngleRotation(current, last, next));
			}

			// otherwise if the current and last points are in different columns, we
			// draw the straight body piece with no rotation
			else if (current.X != last.X)
			{
				Grid.DrawSprite(spriteBatch, straight, current, 0f);
			}

			// otherwise if the current and last points are in different rows, we
			// draw the straight body piece rotated by PiOver2 (90 degrees)
			else if (current.Y != last.Y)
			{
				Grid.DrawSprite(spriteBatch, straight, current, MathHelper.PiOver2);
			}
		}

		/// <summary>
		/// Draw's the snake's tail
		/// </summary>
		/// <param name="spriteBatch">The SpriteBatch to use when rendering.</param>
		private void DrawTail(SpriteBatch spriteBatch)
		{
			// get the point of the tail
			Point tailPoint = bodyPoints[bodyPoints.Count - 1];

			// get the point of the previous body part
			Point lastBody = bodyPoints[bodyPoints.Count - 2];

			// figure out the rotation for the tail based on the positions
			// of the tail and the next body part
			float rotation;
			if (tailPoint.Y == lastBody.Y - 1)
			{
				rotation = MathHelper.PiOver2;
			}
			else if (tailPoint.Y == lastBody.Y + 1)
			{
				rotation = -MathHelper.PiOver2;
			}
			else if (tailPoint.X == lastBody.X + 1)
			{
				rotation = MathHelper.Pi;
			}
			else
			{
				rotation = 0f;
			}

			// draw the tail using the global DrawSprite method.
			Grid.DrawSprite(spriteBatch, tail, tailPoint, rotation);
		}

		/// <summary>
		/// Figures out the rotation for an angled body piece.
		/// </summary>
		/// <param name="current">The angle body piece's position.</param>
		/// <param name="last">The previous body piece's position.</param>
		/// <param name="next">The next body piece's position.</param>
		/// <returns>The rotation (in radians) for the body piece.</returns>
		private float GetAngleRotation(Point current, Point last, Point next)
		{
			// first we calculate the offsets from the last and next 
			// points that determine the various angles of rotation.

			// these two are used to signal that we need -MathHelper.PiOver2
			Point negPiOver2 = new Point(next.X + 1, last.Y - 1);
			Point negPiOver22 = new Point(last.X + 1, next.Y - 1);

			// these two are used to signal that we need MathHelper.Pi
			Point pi = new Point(next.X - 1, last.Y - 1);
			Point pi2 = new Point(last.X - 1, next.Y - 1);

			// and these two are used to signal that we need MathHelper.PiOver2
			Point piOver2 = new Point(next.X - 1, last.Y + 1);
			Point piOver22 = new Point(last.X - 1, next.Y + 1);

			// now we compare our current point to the values above to see what
			// rotation we need. if none of the above match, we return 0f for the rotation.
			if (current == negPiOver2 || current == negPiOver22)
			{
				return -MathHelper.PiOver2;
			}
			else if (current == pi || current == pi2)
			{
				return MathHelper.Pi;
			}
			else if (current == piOver2 || current == piOver22)
			{
				return MathHelper.PiOver2;
			}
			else
			{
				return 0f;
			}
		}
	}
}
