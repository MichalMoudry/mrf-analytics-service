namespace AnalyticsService.Database.Model.Domain

open System

/// A record representing a dead topic that was meant for this service.
type DeadTopic = {
    Id: Guid
    Endpoint: string
    RequestData: byte[]
    Source: string
    DateAdded: DateTimeOffset
}