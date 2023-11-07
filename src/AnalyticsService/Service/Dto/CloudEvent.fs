namespace AnalyticsService.Service.Dto

open System
open System.Text.Json.Serialization

/// A record representing a cloud event from MQ.
type CloudEvent<'T> = {
    [<JsonPropertyName("id")>]
    Id: Guid

    [<JsonPropertyName("data")>]
    Data: 'T

    [<JsonPropertyName("datacontenttype")>]
    ContentType: string

    [<JsonPropertyName("source")>]
    Source: string
}
