using System;

namespace Blazor2048
{
	[Serializable]
	internal class GameOverException : Exception
	{
		public GameOverException()
		{
		}
	}
}