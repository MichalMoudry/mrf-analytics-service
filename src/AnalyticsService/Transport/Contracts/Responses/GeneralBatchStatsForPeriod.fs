namespace AnalyticsService.Transport.Contracts.Requests

open System
open System.Text.Json.Serialization

/// A record representing a request for obtaining stats for a specific time period.
type GeneralBatchStatsForPeriod = {
    [<JsonPropertyName("workflow_id")>]
    WorkflowId: Guid

    [<JsonPropertyName("start_date")>]
    StartDate: DateTimeOffset

    [<JsonPropertyName("end_date")>]
    EndDate: DateTimeOffset
}