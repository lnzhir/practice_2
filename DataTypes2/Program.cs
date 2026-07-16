using System.Text;




string Romb(int N)
{
	if (N <= 0 || N % 2 == 0)
    {
    	throw new ArgumentException("Диагональ не положительная или четная");
    }

	var result = new StringBuilder();

	int middle = N / 2;

	for (var i = 0; i < N; i++)
	{
		int index = i > middle ? N - i - 1 : i;

		char[] line = new string(' ', N).ToCharArray();
		line[middle - index] = 'X';
		line[N - middle - 1 + index] = 'X';

		result.AppendLine(new string(line));
	}

	return result.ToString();
}


Console.WriteLine(Romb(11));