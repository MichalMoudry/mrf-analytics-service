namespace AnalyticsService.Service.Model;

/// <summary>
/// A record representing a general statistic about a document batch.
/// </summary>
internal sealed record GeneralBatchStats(
    int AvgNumberOfDocuments,
    float SuccessRate,
    TimeSpan AvgDuration
);