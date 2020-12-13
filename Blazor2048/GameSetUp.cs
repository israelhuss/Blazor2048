using System;

namespace Blazor2048
{
	class GameSetUp
	{
		private static Component[] componentsRow1 = new Component[4] { new Component(0, 0), new Component(0, 1), new Component(0, 2), new Component(0, 3) };
		private static Component[] componentsRow2 = new Component[4] { new Component(1, 0), new Component(1, 1), new Component(1, 2), new Component(1, 3) };
		private static Component[] componentsRow3 = new Component[4] { new Component(2, 0), new Component(2, 1), new Component(2, 2), new Component(2, 3) };
		private static Component[] componentsRow4 = new Component[4] { new Component(3, 0), new Component(3, 1), new Component(3, 2), new Component(3, 3) };
		public static Component[][] components = new Component[4][] { componentsRow1, componentsRow2, componentsRow3, componentsRow4 };
		private static readonly Random random = new Random();

		private static int[] GetRandomPosition()
		{

			int randomX = random.Next(4);
			int randomY = random.Next(4);
			int[] randomPosition = new int[2] { randomX, randomY };
			return randomPosition;
		}


		private static int[] GetEmptySpace()
		{
			int[] randomPosition = GetRandomPosition();
			while (components[randomPosition[0]][randomPosition[1]].Value != null)
			{
				randomPosition = GetRandomPosition();
				continue;
			}
			return randomPosition;
		}



		public static void SetUpGame()
		{
			AddComponent();
			AddComponent();
		}

		public static void AddComponent()
		{
			int[] randomPosition = GetEmptySpace();
			components[randomPosition[0]][randomPosition[1]].Value = "2";
		}

		public static void OnLeftMove()
		{
			// Loop over all rows
			for (int i = 0; i < components.Length; i++)
			{
				// Counter indicates where the first empty component is in this row
				int counter = 0;
				// In each row loop over all columns
				for (int j = 0; j < components[i].Length; j++)
				{
					// Reset temp
					string temp;
					// If the current component is empty, continue without doing anything
					if (components[i][j].Value == null)
					{
						continue;
					}
					// If the component is not empty, AND is't the first component on the row that has a value
					else if (components[i][j].Value != null && counter == 0)
					{
						// temp is set to be the value of the fisrt component in the row (counter)
						temp = components[i][counter].Value;
						//The value of the current component is moved to the first empty component
						components[i][counter].Value = components[i][j].Value;
						// The value of temp is moved to the current component
						components[i][j].Value = temp;
						// Move up the empty spot with one
						counter++;
					}
					// If the component is not empty, AND the first component is already full
					else if (components[i][j].Value != null && counter > 0)
					{
						// Check if the first component's value is the same as the current value
						if (components[i][j].Value == components[i][counter - 1].Value)
						{
							// If yes, merge the 2 values
							components[i][counter - 1].Value = (int.Parse(components[i][j].Value) + int.Parse(components[i][counter - 1].Value)).ToString();
							// And set the current value to null
							components[i][j].Value = null;
						}
						else
						{
							// If it's not equal, swap the values of counter and the current
							temp = components[i][counter].Value;
							components[i][counter].Value = components[i][j].Value;
							components[i][j].Value = temp;
							// And move up the empty spot
							counter++;
						}
					}
				}
			}
			AddComponent();
		}

	}
}
