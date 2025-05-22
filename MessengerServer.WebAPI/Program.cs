using Microsoft.EntityFrameworkCore;
using MessengerServer.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

var environment = builder.Environment;
var configuration = builder.Configuration;
string? connectionString = configuration.GetConnectionString("Default");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("Missing DB connection string");
}
if (environment.IsDevelopment())
{
    var dataDirectory = AppDomain.CurrentDomain.GetData("DataDirectory")?.ToString()
        ?? Path.Combine(Directory.GetCurrentDirectory(), "App_Data");

    if (!Directory.Exists(dataDirectory))
    {
        Directory.CreateDirectory(dataDirectory);
    }

    builder.Services.AddDbContext<AppSqliteDbContext>(options => 
        options.UseSqlite(connectionString));

    builder.Services.AddScoped<AppDbContextBase>(provider =>
        provider.GetRequiredService<AppSqliteDbContext>());
}
else
{
    builder.Services.AddDbContext<AppSqlServerDbContext>(options =>
        options.UseSqlServer(connectionString));

	builder.Services.AddScoped<AppDbContextBase>(provider =>
		provider.GetRequiredService<AppSqlServerDbContext>());
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<Program>>();

        try
        {
            var dbContext = services.GetRequiredService<AppDbContextBase>();
            dbContext.Database.Migrate();

            logger.LogInformation("Database migrated successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while migrating the database.");
            throw;
        }
    }
}

// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
