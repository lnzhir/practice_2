using System.Drawing;




var rectangle = new Rectangle
{
	Position = new Point(0, 0),
	Width = 3,
	Height = 15
};

Console.WriteLine(rectangle.Perimeter);
Console.WriteLine(rectangle.Area);


public class Rectangle
{
	public Point Position { get; set; }

	private int _width;
	public int Width {
		get => _width;
		set => _width = value >= 0 ? value : 0;
	}

	private int _height;
	public int Height { 
		get => _height; 
		set => _height = value >= 0 ? value : 0; 
	}

	public int Perimeter
	{
		get
		{
			return 2 * (Width + Height);
		}
	}

	public int Area
	{
		get
		{
			return Width * Height;
		}
	}
}



