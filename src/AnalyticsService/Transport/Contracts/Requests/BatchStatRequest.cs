using System.Text.Json.Serialization;
using AnalyticsService.Database.Model.Domain;

namespace AnalyticsService.Transport.Contracts.Requests;

/// <summary>
/// A record representing a request for adding a new document batch statistic.
/// </summary>
internal sealed record BatchStatRequest(
    [property: JsonPropertyName("start_date")] DateTime StartDate,
    [property: JsonPropertyName("end_date")] DateTime EndDate,
    [property: JsonPropertyName("number_of_documents")] int NumberOfDocuments,
    [property: JsonPropertyName("status")] BatchStatus Status,
    [property: JsonPropertyName("workflow_id")] Guid WorkflowId
);