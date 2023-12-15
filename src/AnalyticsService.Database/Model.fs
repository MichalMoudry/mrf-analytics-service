namespace AnalyticsService.Database.Model

open System

/// Type representing a statistic about a single processed document batch.
type internal BatchStat = {
    Id: Guid
    StartDate: DateTimeOffset
    EndDate: DateTimeOffset
    NumberOfDocuments: int
    RunTime: TimeSpan
    Status: int
    WorkflowId: Guid
    Created: DateTimeOffset
}

/// A record representing a dead topic that was meant for this service.
type internal DeadTopic = {
    Id: Guid
    Endpoint: string
    RequestData: byte[]
    Source: string
    DateAdded: DateTimeOffset
}
