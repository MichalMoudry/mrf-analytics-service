namespace AnalyticsService.Service.Model.Dto

open System
open System.Text.Json.Serialization

/// A record representing general statistics of the system.
type GeneralBatchStats = {
    [<JsonPropertyName("avg_processing_time")>]
    AvgProcessingTime: TimeSpan

    [<JsonPropertyName("success_rate")>]
    SuccessRate: float

    [<JsonPropertyName("avg_number_of_documents")>]
    AvgNumberOfDocs: float
}
