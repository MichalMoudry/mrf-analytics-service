using System.Text.Json.Serialization;

namespace AnalyticsService.Transport.Contracts.Requests;

/// <summary>
/// A record representing a request for adding a new document batch statistic.
/// </summary>
internal sealed record BatchStatRequest(
    [property: JsonPropertyName("start_date")] DateTimeOffset StartDate,
    [property: JsonPropertyName("end_date")] DateTimeOffset EndDate,
    [property: JsonPropertyName("number_of_documents")] int NumberOfDocuments,
    [property: JsonPropertyName("status")] int Status,
    [property: JsonPropertyName("workflow_id")] Guid WorkflowId
);