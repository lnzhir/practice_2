using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class DBProviderEF : DbContext
{
	public DbSet<Reader> Readers { get; set; } = null!;

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		IConfiguration configuration = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json")
			.Build();

		optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
	}

	public List<Reader> GetReaders() => Readers.ToList();

	public void AddReader(Reader reader)
	{
		Readers.Add(reader);
		SaveChanges();
	}

	public void DeleteReader(Guid id)
	{
		Reader? reader = Readers.Where(e => e.Id == id).First();
		if (reader != null)
		{
			Readers.Remove(reader);
			SaveChanges();
		}
	}

	public void DeleteReader(Reader reader)
	{
		Readers.Remove(reader);
		SaveChanges();
	}

	public void UpdateReader(Reader reader)
	{
		Readers.Update(reader);
		SaveChanges();
	}
}