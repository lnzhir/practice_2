




{
	var db = new DBProviderAdo();

	List<Reader> readers = db.GetReaders();

	foreach (Reader reader in readers)
	{
		Console.WriteLine(reader);
	}

	Console.WriteLine(db.GetReader(new Guid("01069544-b668-445a-9af7-5c3d44009620")));
}

using (var db = new DBProviderEF())
{
	Reader? reader = db.Readers.FirstOrDefault();
	Console.WriteLine(reader?.ToString());

	if (reader != null )
	{
		reader.RegistrationDate = new DateTime(14, 2, 1);
		db.UpdateReader(reader);
	}

	foreach (var readers  in db.Readers)
	{
		Console.WriteLine(readers);
	}
}