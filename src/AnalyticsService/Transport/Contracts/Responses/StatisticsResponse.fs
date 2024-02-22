namespace AnalyticsService.Transport.Contracts.Responses

open System.Text.Json.Serialization
open AnalyticsService.Service.Model

/// A class representing a contract of this API related to document batch statistic.
[<Sealed>]
type internal StatisticsResponse(batchStats: GeneralBatchStats) =
    [<JsonPropertyName("avg_doc_number")>]
    member this.AverageNumberOfDocs = batchStats.AvgNumberOfDocs
    [<JsonPropertyName("success_rate")>]
    member this.SuccessRate = batchStats.SuccessRate
    [<JsonPropertyName("avg_duration")>]
    member this.AverageDuration = batchStats.AvgDuration