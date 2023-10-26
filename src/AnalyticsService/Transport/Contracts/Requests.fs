namespace AnalyticsService.Transport.Contracts

open System
open System.Text.Json.Serialization
open AnalyticsService.Database.Domain

/// A record representing a request for adding a new document batch statistic.
type BatchStatRequest = {
    [<JsonPropertyName("start_date")>]
    StartDate: DateTime

    [<JsonPropertyName("end_date")>]
    EndDate: DateTime

    [<JsonPropertyName("number_of_documents")>]
    NumberOfDocuments: int

    [<JsonPropertyName("status")>]
    Status: BatchStatus

    [<JsonPropertyName("workflow_id")>]
    WorkflowId: Guid
}

/// A record representing a request for obtaining stats for a specific time period.
type BatchPeriodStatsRequest = {
    [<JsonPropertyName("workflow_id")>]
    WorkflowId: Guid

    [<JsonPropertyName("start_date")>]
    StartDate: DateTime

    [<JsonPropertyName("end_date")>]
    EndDate: DateTime
}
