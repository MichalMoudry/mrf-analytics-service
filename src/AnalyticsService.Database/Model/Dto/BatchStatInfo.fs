namespace AnalyticsService.Database.Model.Dto

open System
open AnalyticsService.Database.Model.Domain

/// A data-transfer-object representing a single result of document batch.
type BatchStatInfo =
  { Id: Guid
    StartDate: DateTimeOffset
    EndDate: DateTimeOffset
    RunTime: TimeSpan
    NumberOfDocuments: int
    Status: int }
  member this.BatchStatus with get() = BatchStatus.Completed
  /// Information if statistic is connected to a successful document batch completion.
  member this.IsSuccess with get() = this.BatchStatus = BatchStatus.Completed