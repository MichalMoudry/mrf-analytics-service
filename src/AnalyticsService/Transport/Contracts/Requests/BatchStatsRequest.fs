namespace AnalyticsService.Transport.Contracts.Requests

open System
open System.Text.Json.Serialization

/// A record representing a request for adding a new document batch statistic.
type internal BatchStatsRequest = {
    [<JsonPropertyName("start_date")>]
    StartDate: DateTimeOffset
    [<JsonPropertyName("end_date")>]
    EndDate: DateTimeOffset
    [<JsonPropertyName("number_of_docs")>]
    NumberOfDocs: int
    [<JsonPropertyName("status")>]
    Status: int
    [<JsonPropertyName("workflow_id")>]
    WorkflowId: Guid
}