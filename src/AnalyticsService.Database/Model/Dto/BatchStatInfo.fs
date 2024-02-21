namespace AnalyticsService.Database.Model.Dto

open System

/// A data-transfer-object representing a single result of document batch.
type BatchStatInfo =
  { Id: Guid
    StartDate: DateTimeOffset
    EndDate: DateTimeOffset
    NumberOfDocuments: int
    Status: int }