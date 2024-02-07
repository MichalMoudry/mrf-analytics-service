using System.Data;
using AnalyticsService;
using AnalyticsService.Database.Api;
using AnalyticsService.Transport;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(cfg => cfg.AddServerHeader = false);
var connectionString = builder.Environment.IsDevelopment()
    ? builder.Configuration["DbConnection"]
    : Environment.GetEnvironmentVariable("DB_CONN");

// Adding services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
//builder.Services.AddRepositories();
builder.Services.AddTransient(_ => Connector.GetConnection(connectionString));
builder.Services
    .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly))
    .AddValidators();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHealthChecks("/health");

Handler.Initialize(app);

app.Logger.LogInformation("Hello from analytics service! ʕ•ᴥ•ʔ");
app.Run();
