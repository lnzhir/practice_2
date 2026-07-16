



Dictionary<int, float> YearsPercentage(float initial_deposit, uint years, float interest_rate)
{
	if (initial_deposit <= 0) {
		throw new ArgumentException("Начальный депозит должен быть положительным", 
			nameof(initial_deposit));
	}

	if (interest_rate <= 0) {
		throw new ArgumentException("Процентная ставка должна быть положительной", 
			nameof(interest_rate));
	}

	var result = new Dictionary<int, float>();

	for (var i = 0; i < years; i++)
	{
		initial_deposit += initial_deposit * interest_rate / 100;

		result[i+1] = initial_deposit;
	}

	return result;
}

var moneys = YearsPercentage(1000, 3, 10);

foreach (var (key, val) in moneys)
{
    Console.WriteLine($"Год {key}: {val:F2} руб.");
}