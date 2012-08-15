using System;

namespace SammyTheSnake
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main(string[] args)
		{
			using (SammyTheSnakeGame game = new SammyTheSnakeGame())
			{
				game.Run();
			}
		}
	}
}

