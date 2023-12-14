module internal AnalyticsService.Database.Model

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

/// A constructor function for creating an instance of BatchStat record.
let internal NewBatchStat startDate endDate docNumber status workflowId =
    if startDate >= endDate then
        None
    else
        Some({
            Id = Guid.NewGuid()
            StartDate = startDate
            EndDate = endDate
            NumberOfDocuments = docNumber
            RunTime = endDate - startDate
            Status = status
            WorkflowId = workflowId
            Created = DateTimeOffset.Now
        })

/// A record representing a dead topic that was meant for this service.
type internal DeadTopic = {
    Id: Guid
    Endpoint: string
    RequestData: byte[]
    Source: string
    DateAdded: DateTimeOffset
}

/// A constructor function for the DeadTopic record.
let internal NewDeadTopic endpoint data source = {
    Id = Guid.NewGuid()
    Endpoint = endpoint
    RequestData = data
    Source = source
    DateAdded = DateTimeOffset.Now
}
