using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddControllers()
	.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
	});
builder.Services.AddOpenApi();



// Получение строки подключения.
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (connectionString == null)
{
	throw new NullReferenceException("connectionString == null");
}

builder.Services.AddDbContext<FinanceDB.DBProvider>(options => options.UseSqlServer(connectionString));

// Swagger.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();

	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors("AllowAll"); 

app.UseAuthorization();

app.MapControllers();

app.Run();
