using Api_web1.Data;
using Api_web1.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure the connection string for SQL Server
var connstring = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<GamestoreContext>(options =>
    options.UseSqlServer(connstring)); // Use SQL Server instead of SQLite

var app = builder.Build();

// Apply pending migrations when the app starts
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<GamestoreContext>();
    dbContext.Database.Migrate();
}

// Map your endpoints
app.MapGamesEndpoints();

app.Run();
