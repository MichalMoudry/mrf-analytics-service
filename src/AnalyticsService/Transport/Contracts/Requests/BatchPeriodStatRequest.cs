using System.Text.Json.Serialization;

namespace AnalyticsService.Transport.Contracts.Requests;

/// <summary>
/// A record representing a request for obtaining stats for a specific time period.
/// </summary>
internal sealed record BatchPeriodStatRequest(
    [property: JsonPropertyName("workflow_id")]
    Guid WorkflowId,
    [property: JsonPropertyName("start_date")]
    DateTime StartDate,
    [property: JsonPropertyName("end_date")]
    DateTime EndDate
);