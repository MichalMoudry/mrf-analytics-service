using System.Text.Json.Serialization;
using AnalyticsService.Service.Model;

namespace AnalyticsService.Transport.Contracts.Responses;

/// <summary>
/// A class representing a contract of this API related to document batch statistic.
/// </summary>
/// <param name="generalBatchStat"></param>
internal sealed class StatisticResponse(GeneralBatchStat generalBatchStat)
{
    [JsonPropertyName("avg_doc_number")]
    public int AverageNumberOfDocs => generalBatchStat.AvgNumberOfDocuments;

    [JsonPropertyName("success_rate")]
    public float SuccessRate => generalBatchStat.SuccessRate;

    [JsonPropertyName("avg_duration")]
    public TimeSpan AverageDuration => generalBatchStat.AvgDuration;
}