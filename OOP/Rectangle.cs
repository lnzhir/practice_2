using System.Drawing;

namespace OOP
{
	public class Rectangle
	{
		public Point Position { get; set; } = new Point(0, 0);

		private int _width;
		private int _height;

		public Rectangle(int width, int height)
		{
			Width = width;
			Height = height;
		}

		public int Width
		{
			get => _width;
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("Ширина отрицательная"
						, nameof(value));
				}
				_width = value;
			}
		}


		public int Height
		{
			get => _height;
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("Высота отрицательная"
						, nameof(value));
				}
				_height = value;
			}
		}

		public int Perimeter
		{
			get => 2 * (Width + Height);
		}

		public int Area
		{
			get => Width * Height;
		}
	}

}
