

public class Reader
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public DateTime RegistrationDate { get; set; }

	public override string ToString()
	{
		return $"{Id} {Name} {RegistrationDate}";
	}
}