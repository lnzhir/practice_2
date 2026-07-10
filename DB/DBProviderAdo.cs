using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

public class DBProviderAdo
{
	private string? _connectionString;


	public DBProviderAdo()
	{
		IConfiguration configuration = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json")
			.Build();

		_connectionString = configuration.GetConnectionString("DefaultConnection");

		if (_connectionString == null)
		{
			throw new NullReferenceException("_connectionString == null");
		}
	}


	public List<Reader> GetReaders()
	{
		List<Reader> result = new List<Reader>();

		using (var connection = new SqlConnection(_connectionString))
		using (var command = new SqlCommand("SELECT * FROM Readers", connection))
		{
			connection.Open();
			using (var reader = command.ExecuteReader())
			{
				if (reader.HasRows)
				{
					while (reader.Read())
					{

						result.Add(new Reader
						{
							Id = (Guid)reader.GetValue(0),
							Name = (string)reader.GetValue(1),
							RegistrationDate = (DateTime)reader.GetValue(2)
						});
					}
				}
			}
		}

		return result;
	}

	public Reader GetReader(Guid id)
	{
		using (var connection = new SqlConnection(_connectionString))
		using (var command = new SqlCommand("SELECT * FROM Readers WHERE id = @id", connection))
		{
			command.Parameters.Add(new SqlParameter("@id", id));
			connection.Open();
			using (var reader = command.ExecuteReader())
			{
				if (reader.HasRows && reader.Read())
				{
					return new Reader
					{
						Id = (Guid)reader.GetValue(0),
						Name = (string)reader.GetValue(1),
						RegistrationDate = (DateTime)reader.GetValue(2)
					};
				}
			}
		}

		return null;
	}

	public void CreateReader(string name, int registrationDate)
	{
		using (SqlConnection connection = new SqlConnection(_connectionString))
		{
			connection.Open();

			using (SqlCommand command = new SqlCommand(
				"INSERT INTO Readers (name, registrationDate) VALUES (@name, @registrationDate)"
				, connection))
			{
				command.Parameters.AddWithValue("@name", name);
				command.Parameters.AddWithValue("@registrationDate", registrationDate);

				command.ExecuteNonQuery();
			}
		}
	}

	public void UpdateReader(Reader reader)
	{
		using (SqlConnection connection = new SqlConnection(_connectionString))
		{
			connection.Open();

			using (SqlCommand command = new SqlCommand(
				"UPDATE Readers SET name = @name, registrationDate = @registrationDate WHERE id = @id"
				, connection))
			{
				command.Parameters.AddWithValue("@id", reader.Id);
				command.Parameters.AddWithValue("@name", reader.Name);
				command.Parameters.AddWithValue("@registrationDate", reader.RegistrationDate);

				command.ExecuteNonQuery();
			}
		}
	}

	public void DeleteUser(Guid id)
	{
		using (SqlConnection connection = new SqlConnection(_connectionString))
		{
			connection.Open();

			using (SqlCommand command = new SqlCommand(
				"DELETE FROM Users WHERE id = @id"
				, connection))
			{
				command.Parameters.AddWithValue("@id", id);

				command.ExecuteNonQuery();
			}
		}
	}
}
