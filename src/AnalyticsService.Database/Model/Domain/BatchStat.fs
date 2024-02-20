namespace AnalyticsService.Database.Model.Domain

open System

/// Type representing a statistic about a single processed document batch.
type BatchStat =
  { Id: Guid
    StartDate: DateTimeOffset
    EndDate: DateTimeOffset
    NumberOfDocuments: int
    Status: int
    WorkflowId: Guid
    DateAdded: DateTimeOffset }
    member this.RunTime with get() = this.EndDate - this.StartDate