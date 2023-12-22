namespace AnalyticsService.Database.Api

open System

/// Record representing all the information need for DLQ item processing.
type DlqItemProcessingInfo = {
    Id: Guid
    Endpoint: string
    RequestData: byte[]
}

/// Record representing all the 
type BatchStatInfo = {
    Id: Guid
    StartDate: DateTimeOffset
    EndDate: DateTimeOffset
    NumberOfDocuments: int
    RunTime: TimeSpan
    Status: int
    WorkflowId: Guid
}