namespace AnalyticsService.Service.Api.Dto

open System
open AnalyticsService.Database.Domain

/// An interface for a DTO that is used by the service layer.
type IBatchStatDto =
    abstract member StartDate: DateTime
    abstract member EndDate: DateTime
    abstract member NumberOfDocuments: int
    abstract member Status: BatchStatus
