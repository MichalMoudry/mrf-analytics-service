namespace AnalyticsService.Service.Model;

/// <summary>
/// A record representing a general statistic about a document batch.
/// </summary>
internal sealed record GeneralBatchStat(
    int AvgNumberOfDocuments,
    float SuccessRate,
    TimeSpan AvgDuration
);