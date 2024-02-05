using System.Data;
using AnalyticsService;
using AnalyticsService.Database;
using AnalyticsService.Transport.Contracts.Requests;
using Dapr;
using FluentValidation;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

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

app.MapPost("/dapr", async (IValidator<BatchStatRequest> validator, IMediator mediator, CloudEvent<BatchStatRequest> request) =>
{
    var validationResult = await validator.ValidateAsync(request.Data);
    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }
    return Results.Ok(request);
});

app.Logger.LogInformation("Hello from analytics service! ʕ•ᴥ•ʔ");
app.Run();
