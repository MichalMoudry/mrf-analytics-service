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
}

/// A record representing a cloud event (V1) from MQ.
type CloudEventV1<'T> = {
    [<JsonPropertyName("id")>]
    Id: string

    [<JsonPropertyName("data")>]
    Data: 'T

    [<JsonPropertyName("source")>]
    Source: string
}
