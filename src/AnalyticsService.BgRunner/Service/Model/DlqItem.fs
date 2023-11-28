namespace AnalyticsService.BgRunner.Service.Model

open System

/// A record representing a single item in a DLQ.
type internal DlqItem = {
    Id: Guid
    Endpoint: string
    RequestData: byte[]
    DateAdded: DateTime
}
