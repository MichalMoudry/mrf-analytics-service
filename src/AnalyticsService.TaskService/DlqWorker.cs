using System.Data;
using AnalyticsService.Database.Api;

namespace AnalyticsService.TaskService;

internal sealed class DlqWorker : BackgroundService
{
    private readonly ILogger<DlqWorker> _logger;

    private readonly IDbConnection _dbConnection;

    private readonly IDlqRepository _dlqRepository;

    public DlqWorker(
        ILogger<DlqWorker> logger,
        IDbConnection dbConnection,
        IDlqRepository dlqRepository)
    {
        _logger = logger;
        _dbConnection = dbConnection;
        _dlqRepository = dlqRepository;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var dlqItems = await _dlqRepository.GetDlqItems(_dbConnection);
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            foreach (var dlqItem in dlqItems)
            {
                _logger.LogInformation(
                    "DLQ item: {id}\n\t- endpoint: {endpoint}",
                    dlqItem.Id,
                    dlqItem.Endpoint
                );
            }
            await Task.Delay(5_000, stoppingToken);
        }
    }
}
