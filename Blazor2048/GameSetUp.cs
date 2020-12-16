using System;

namespace Blazor2048
{
	class GameSetUp
	{
		private static readonly Random random = new Random();
		public static string[,] components;

		public static void SetUpGame(int num)
		{
			components = new string[num, num];
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num; j++)
				{
					components[i, j] = null;
				}
			}
			AddComponent();
			AddComponent();
		}

		private static void AddComponent()
		{
			int[] randomPosition = GetEmptySpace();
			components[randomPosition[0], randomPosition[1]] = "2";
		}

		private static int[] GetEmptySpace()
		{
			if (!CheckIfFull())
			{
				int[] randomPosition = GetRandomPosition();
				while (!string.IsNullOrEmpty(components[randomPosition[0], randomPosition[1]]))
				{
					randomPosition = GetRandomPosition();
					continue;
				}
				return randomPosition;
			}
			else
			{
				throw new GameOverException();
			}
		}

		private static int[] GetRandomPosition()
		{

			int randomX = random.Next(4);
			int randomY = random.Next(4);
			int[] randomPosition = new int[2] { randomX, randomY };
			return randomPosition;
		}

		private static bool CheckIfFull()
		{
			int count = 0;
			for (int i = 0; i < components.GetLength(0); i++)
			{
				for (int j = 0; j < components.GetLength(1); j++)
				{
					if (!string.IsNullOrEmpty(components[i, j]))
					{
						count++;
					}
				}
			}
			if (count == 16) return true;
			else return false;
		}



		private static bool BoardChanged(string[,] old, string[,] modified)
		{
			for (int i = 0; i < old.GetLength(0); i++)
			{
				for (int j = 0; j < old.GetLength(1); j++)
				{
					Console.WriteLine(old[i, j]);
					Console.WriteLine(modified[i, j]);
					Console.WriteLine(old[i, j] != modified[i, j]);
					if (old[i, j] != modified[i, j]) return true;
					else continue;
				}
			}
			return false;
		}




		public static void OnMoveLeft()
		{
			string[,] tempComponents = components;
			// Loop over all rows
			for (int i = 0; i < components.GetLength(0); i++)
			{
				// Counter indicates where the first empty space is in the current [i] row
				int counter = 0;
				// In each row loop over all columns
				for (int j = 0; j < components.GetLength(1); j++)
				{
					// Reset temp
					string temp;
					// If the current component is empty, continue without doing anything
					if (string.IsNullOrEmpty(components[i, j]))
					{
						continue;
					}
					// If the component is not empty, AND is't the first component on the row that has a value
					else if (!string.IsNullOrEmpty(components[i, j]) && counter == 0)
					{
						// temp is set to be the value of the fisrt component in the row (counter)
						temp = components[i, counter];
						//The value of the current component is moved to the first empty component
						components[i, counter] = components[i, j];
						// The value of temp is moved to the current component
						components[i, j] = temp;
						// Move up the empty spot with one
						counter++;
					}
					// If the component is not empty, AND the first component is already full
					else if (!string.IsNullOrEmpty(components[i, j]) && counter > 0)
					{
						// Check if the first component's value is the same as the current value
						if (components[i, j] == components[i, counter - 1])
						{
							// If yes, merge the 2 values
							components[i, counter - 1] = (int.Parse(components[i, j]) + int.Parse(components[i, counter - 1])).ToString();
							// And set the current value to null
							components[i, j] = string.Empty;
						}
						else
						{
							// If it's not equal, swap the values of counter and the current
							temp = components[i, counter];
							components[i, counter] = components[i, j];
							components[i, j] = temp;
							// And move up the empty spot
							counter++;
						}
					}
				}
			}
			DisplayStuff(ref components);
			AddComponent();
			DisplayStuff(ref components);
		}

		public static void OnMoveRight()
		{
			// Loop over all rows
			for (int i = 0; i < components.GetLength(0); i++)
			{
				// Counter indicates where the first empty component is in this row
				int counter = components.GetLength(0) - 1;
				// In each row loop over all columns
				for (int j = components.GetLength(1) - 1; j >= 0; j--)
				{
					// Reset temp
					string temp;
					// If the current component is empty, continue without doing anything
					if (components[i, j] == null)
					{
						continue;
					}
					// If the component is not empty, AND is't the first component on the row that has a value
					else if (components[i, j] != null && counter == (components.GetLength(0) - 1))
					{
						// temp is set to be the value of the fisrt component in the row (counter)
						temp = components[i, counter];
						//The value of the current component is moved to the first empty component
						components[i, counter] = components[i, j];
						// The value of temp is moved to the current component
						components[i, j] = temp;
						// Move up the empty spot with one
						counter--;
					}
					// If the component is not empty, AND the first component is already full
					else if (components[i, j] != null && counter < (components.GetLength(0) - 1))
					{
						// Check if the first component's value is the same as the current value
						if (components[i, j] == components[i, counter])
						{
							// If yes, merge the 2 values
							components[i, counter] = (int.Parse(components[i, j]) + int.Parse(components[i, counter])).ToString();
							// And set the current value to null
							components[i, j] = null;
						}
						else
						{
							// If it's not equal, swap the values of counter and the current
							temp = components[i, counter];
							components[i, counter] = components[i, j];
							components[i, j] = temp;
							// And move up the empty spot
							counter--;
						}
					}
				}
			}
			AddComponent();
		}


		private static void DisplayStuff(ref string[,] toDisplay)
		{
			for (int i = 0; i < toDisplay.GetLength(0); i++)
			{
				Console.WriteLine($"|{toDisplay[i, 0]}| {toDisplay[i, 1]}| {toDisplay[i, 2]}| {toDisplay[i, 3]}|");
			}
			Console.WriteLine("");
		}

	}
}



//TODO figure out how to copy a reference type