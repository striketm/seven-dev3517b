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

namespace SammyTheSnake
{
	/// <summary>
	/// Sets the state of the SnakeGame.
	/// </summary>
	public enum GameState
	{
		Title,
		InGame,
		GameOver
	}

	/// <summary>
	/// The game class for Sammy the Snake.
	/// </summary>
	public class SammyTheSnakeGame : Game
	{
		// some strings we'll be using in our game
		private const string gameTitle = "Sammy the Snake!";
		private const string playInstructions = "Press Play to Begin";
		private const string scoreFormat = "Oranges Eaten: {0}";
		private const string quitInsructions = "Press Back to Quit";
		private const string gameOver = "Game Over!";
		private const string gameOverInstructions = "Press Play to Continue";

		// the GraphicsDeviceManager for our game
		private GraphicsDeviceManager graphics;

		// a SpriteBatch for rendering our game
		private SpriteBatch spriteBatch;

		// the current state of the game
		private GameState state = GameState.InGame;

		// the snake itself
		private Snake snake = new Snake();

		// we use a single orange for the whole game
		private Orange orange = new Orange();

		// the score for the game
		private int score;

		// the three fonts our game uses
		private SpriteFont titleFont;
		private SpriteFont mediumFont;
		private SpriteFont miniFont;

		// the current frame's GamePadState
		private GamePadState gamePadState;

		// we need to have the last frame's GamePadState so
		// we can determine if a button press was new this frame
		private GamePadState lastGamePadState;

		public SammyTheSnakeGame()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			// these lines let us run the game on Windows with the
			// same size as the Zune screen's resolution
			graphics.PreferredBackBufferWidth = 240;
			graphics.PreferredBackBufferHeight = 320;
			graphics.PreferMultiSampling = true;
		}

		protected override void LoadContent()
		{
			// create a new SpriteBatch for the game
			spriteBatch = new SpriteBatch(GraphicsDevice);

			// load the snake and orange graphics
			snake.Load(Content);
			orange.Load(Content);

			// load in our fonts
			titleFont = Content.Load<SpriteFont>("Fonts/TitleFont");
			mediumFont = Content.Load<SpriteFont>("Fonts/MediumFont");
			miniFont = Content.Load<SpriteFont>("Fonts/MiniFont");
		}

		private bool IsNewButtonPress(Buttons button)
		{
			// test to see if the button was up last frame and down this frame
			return (gamePadState.IsButtonDown(button) && lastGamePadState.IsButtonUp(button));
		}

		protected override void Update(GameTime gameTime)
		{
			// get this frame's GamePadState
			gamePadState = GamePad.GetState(PlayerIndex.One);

			// check the state so that we can have different code run for
			// our different states.

			if (state == GameState.Title)
				UpdateTitleScreen();
			else if (state == GameState.InGame)
				UpdateInGame(gameTime);
			else if (state == GameState.GameOver)
				UpdateGameOver();

			// save this frame's GamePadState for use next frame
			lastGamePadState = gamePadState;

			base.Update(gameTime);
		}

		private void UpdateTitleScreen()
		{
			// check for the Back button to exit the game
			if (IsNewButtonPress(Buttons.Back))
				Exit();

			// check for the B button (Play on the Zune) to move in to the game
			if (IsNewButtonPress(Buttons.B))
			{
				// reset the snake
				snake.Reset();

				// reset the score
				score = 0;

				// reposition the orange
				orange.Reposition(snake);

				// change game states
				state = GameState.InGame;
			}
		}

		private void UpdateInGame(GameTime gameTime)
		{
			// check for the Back button to move back to the title screen
			if (IsNewButtonPress(Buttons.Back))
				state = GameState.Title;

			// update the snake
			snake.Update(gameTime);

			// if the snake's head is on the orange...
			if (snake.IsHeadAtPosition(orange.Position))
			{
				// extend the snake
				snake.Extend();

				// increase the score
				score++;

				// reposition the orange
				orange.Reposition(snake);
			}

			// if the snake has looped around into itself, the game is over
			if (snake.IsLooped())
				state = GameState.GameOver;

			// if the snake left the screen, the game is over
			if (snake.IsHeadOffScreen())
				state = GameState.GameOver;
		}

		private void UpdateGameOver()
		{
			// check for the B button (Play on the Zune) to move back to the title screen
			if (IsNewButtonPress(Buttons.B))
				state = GameState.Title;
		}

		protected override void Draw(GameTime gameTime)
		{
			// clear the screen to Gray
			GraphicsDevice.Clear(Color.Gray);

			// check the state so that we can have different code run for
			// our different states.

			if (state == GameState.Title)
				DrawTitleScreen();
			else if (state == GameState.InGame)
				DrawInGame();
			else if (state == GameState.GameOver)
				DrawGameOver();

			base.Draw(gameTime);
		}

		private void DrawTitleScreen()
		{
			// draw the game title and our two lines of instructions
			DrawText(titleFont, gameTitle, new Vector2(120f, 25f));
			DrawText(mediumFont, playInstructions, new Vector2(120f, 200f));
			DrawText(mediumFont, quitInsructions, new Vector2(120f, 225f));
		}

		private void DrawInGame()
		{
			// draw the orange and the snake
			orange.Draw(spriteBatch);
			snake.Draw(spriteBatch);

			// draw the score
			DrawText(miniFont, string.Format(scoreFormat, score), new Vector2(120f, 5f));
		}

		private void DrawGameOver()
		{
			// draw the orange and the snake
			orange.Draw(spriteBatch);
			snake.Draw(spriteBatch);

			DrawText(titleFont, gameOver, new Vector2(120f, 25f));
			DrawText(mediumFont, string.Format(scoreFormat, score), new Vector2(120f, 200f));
			DrawText(mediumFont, gameOverInstructions, new Vector2(120f, 225f));
		}

		// draws some text for our game
		private void DrawText(SpriteFont font, string text, Vector2 position)
		{
			// calculate half of the size of our text
			Vector2 halfSize = font.MeasureString(text) / 2f;

			// calculate the real position as the position minus our half size
			position = position - halfSize;

			// we round to integers to prevent blurring of the text
			position.X = (int)position.X;
			position.Y = (int)position.Y;

			// and now we draw using SpriteBatch
			spriteBatch.Begin();
			spriteBatch.DrawString(
				font,
				text,
				position,
				Color.White);
			spriteBatch.End();
		}
	}
}
