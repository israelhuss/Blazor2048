namespace Blazor2048
{
	class Component
	{
		public int X { get; set; }
		public int Y { get; set; }
		public string Value { get; set; }

		public Component(int xPosition, int yPosition)
		{
			x = xPosition;
			y = yPosition;
			Value = null;
		}

		public Component(int xPosition, int yPosition, string value)
		{
			x = xPosition;
			y = yPosition;
			Value = value;
		}

		private int x;
		private int y;
	}
}
