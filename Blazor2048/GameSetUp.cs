using System;
using System.Collections.Generic;

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
			try
			{
				int[] randomPosition = GetEmptySpace();
				components[randomPosition[0], randomPosition[1]] = "2";
			}
			catch (GameOverException)
			{
				SetUpGame(4);
			}

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

		private static string[,] CopyString(string[,] componentString)
		{
			string[,] newString = new string[componentString.GetLength(0), componentString.GetLength(1)];
			for (int i = 0; i < componentString.GetLength(0); i++)
			{
				for (int j = 0; j < componentString.GetLength(1); j++)
				{
					newString[i, j] = componentString[i, j];
				}
			}
			return newString;
		}

		private static bool BoardChanged(string[,] old, string[,] modified)
		{
			for (int i = 0; i < old.GetLength(0); i++)
			{
				for (int j = 0; j < old.GetLength(1); j++)
				{
					if (old[i, j] != modified[i, j]) return true;
					else continue;
				}
			}
			return false;
		}
		public static void OnMoveLeft()
		{
			string[,] tempComponents = CopyString(components);
			for (int i = 0; i < components.GetLength(0); i++)
			{
				List<string> currentRow = new List<string>();
				for (int j = 0; j < components.GetLength(1); j++)
				{
					if (!string.IsNullOrEmpty(components[i, j]))
					{
						currentRow.Add(components[i, j]);
					}
				}
				List<string> rearrangedRow = MoveAndMerge(currentRow);
				for (int k = 0; k < rearrangedRow.Count; k++)
				{
					components[i, k] = rearrangedRow[k];
				}
				if (components.GetLength(1) > rearrangedRow.Count)
				{
					for (int l = rearrangedRow.Count; l < components.GetLength(1); l++)
					{
						components[i, l] = null;
					}
				}
			}

			if (BoardChanged(tempComponents, components))
			{
				AddComponent();
			}
		}

		public static void OnMoveRight()
		{
			string[,] tempComponents = CopyString(components);
			for (int i = 0; i < components.GetLength(0); i++)
			{
				List<string> currentRow = new List<string>();
				for (int j = components.GetLength(1) - 1; j >= 0; j--)
				{
					if (!string.IsNullOrEmpty(components[i, j]))
					{
						currentRow.Add(components[i, j]);
					}
				}
				List<string> rearrangedRow = MoveAndMerge(currentRow);

				int countDown = components.GetLength(1) - 1;
				for (int k = 0; k < rearrangedRow.Count; k++)
				{
					components[i, countDown] = rearrangedRow[k];
					countDown--;
				}
				for (int l = countDown; l >= 0; l--)
				{
					components[i, l] = null;
				}
			}

			if (BoardChanged(tempComponents, components))
			{
				AddComponent();
			}
		}

		public static void OnMoveUp()
		{
			string[,] tempComponents = CopyString(components);
			for (int i = 0; i < components.GetLength(1); i++)
			{
				List<string> currentRow = new List<string>();
				for (int j = 0; j < components.GetLength(0); j++)
				{
					if (!string.IsNullOrEmpty(components[j, i]))
					{
						currentRow.Add(components[j, i]);
					}
				}

				List<string> rearrangedRow = MoveAndMerge(currentRow);
				for (int k = 0; k < rearrangedRow.Count; k++)
				{
					components[k, i] = rearrangedRow[k];
				}
				if (components.GetLength(0) > rearrangedRow.Count)
				{
					for (int l = rearrangedRow.Count; l < components.GetLength(1); l++)
					{
						components[l, i] = null;
					}
				}
			}

			if (BoardChanged(tempComponents, components))
			{
				AddComponent();
			}
		}

		public static void OnMoveDown()
		{
			string[,] tempComponents = CopyString(components);
			for (int i = 0; i < components.GetLength(0); i++)
			{
				List<string> currentRow = new List<string>();
				for (int j = components.GetLength(1) - 1; j >= 0; j--)
				{
					if (!string.IsNullOrEmpty(components[j, i]))
					{
						currentRow.Add(components[j, i]);
					}
				}
				List<string> rearrangedRow = MoveAndMerge(currentRow);

				int countDown = components.GetLength(1) - 1;
				for (int k = 0; k < rearrangedRow.Count; k++)
				{
					components[countDown, i] = rearrangedRow[k];
					countDown--;
				}
				for (int l = countDown; l >= 0; l--)
				{
					components[l, i] = null;
				}
			}

			if (BoardChanged(tempComponents, components))
			{
				AddComponent();
			}

		}


		private static List<string> MoveAndMerge(List<string> currentRow)
		{
			List<string> toReturn = new List<string>();
			while (currentRow.Count > 1)
			{
				if (currentRow[0] == currentRow[1])
				{
					int merged = int.Parse(currentRow[0]) + int.Parse(currentRow[1]);
					toReturn.Add(merged.ToString());
					currentRow.RemoveRange(0, 2);
				}
				else if (currentRow[0] != currentRow[1])
				{
					toReturn.Add(currentRow[0]);
					currentRow.RemoveAt(0);
				}
			}

			if (currentRow.Count == 1)
			{
				toReturn.Add(currentRow[0]);
			}

			return toReturn;
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
