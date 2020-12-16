using System;
using System.Runtime.Serialization;

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