using System.Text;




string YearsPercentage(float initial_deposit, uint years, float interest_rate)
{
	var result = new StringBuilder();

	for (var i = 0; i < years; i++)
	{
		initial_deposit += initial_deposit * interest_rate / 100;

		result.Append($"Год {i+1}: {initial_deposit:F2} руб.\n");
	}

	return result.ToString();
}


Console.WriteLine(YearsPercentage(1000, 3, 10));