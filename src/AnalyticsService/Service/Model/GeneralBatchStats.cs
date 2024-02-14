namespace AnalyticsService.Service.Model;

/// <summary>
/// A record representing a general statistic about a document batch.
/// </summary>
internal sealed record GeneralBatchStat(
    int AvgNumberOfDocuments,
    float SuccessRate,
    TimeSpan AvgDuration
)
{
    public static GeneralBatchStat Default => new(0, 0, TimeSpan.Zero);
};