using System.Data;
using AnalyticsService.Database;

var builder = WebApplication.CreateBuilder(args);
Console.WriteLine("Hello from analytics service! ʕ•ᴥ•ʔ");

// Configuration
builder.WebHost.ConfigureKestrel(cfg => cfg.AddServerHeader = false);
var connectionString = builder.Environment.IsDevelopment()
    ? builder.Configuration["DbConnection"]
    : Environment.GetEnvironmentVariable("DB_CONN");

// Adding services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddTransient<IDbConnection>(
    _ => new ConnectionBuilder()
        .SetConnectionString(connectionString)
        .Build()
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHealthChecks("/health");

app.Run();
