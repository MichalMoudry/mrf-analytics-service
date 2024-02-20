namespace AnalyticsService.Database.Model.Dto

open System

/// A data-transfer-object for DLQ processing.
type DeadTopicInfo = {
    Id: Guid
    Endpoit: string
    RequestData: byte[]
}