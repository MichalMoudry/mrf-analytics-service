using AnalyticsService.Database.Api;

namespace AnalyticsService.TaskService;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        var connectionString = builder.Environment.IsDevelopment()
            ? builder.Configuration["DbConnection"]
            : Environment.GetEnvironmentVariable("DB_CONN");

        builder.Services.AddTransient(_ => Connector.GetConnection(connectionString));
        builder.Services.AddRepositories();
        builder.Services.AddHostedService<DlqWorker>();

        var host = builder.Build();
        host.Run();
    }
}