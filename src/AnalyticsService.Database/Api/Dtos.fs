namespace AnalyticsService.Database.Api

open System

/// Record representing all the information need for DLQ item processing.
type DlqItemProcessingInfo = {
    Id: Guid
    Endpoint: string
    RequestData: byte[]
}