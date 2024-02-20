namespace AnalyticsService.Web.Api.Transport.Contracts.Responses

open System.Text.Json.Serialization
open AnalyticsService.Web.Api.Service.Model

/// A class representing a contract of this API related to document batch statistic.
[<Sealed>]
type StatisticResponse(stat: GeneralBatchStat) =
    [<JsonPropertyName("avg_doc_number")>]
    member this.AverageNumberOfDocs with get() = stat.AvgNumberOfDocs
    [<JsonPropertyName("success_rate")>]
    member this.SuccessRate with get() = stat.SuccessRate
    [<JsonPropertyName("avg_duration")>]
    member this.AverageDuration with get() = stat.AvgDuration