namespace AnalyticsService.Web.Api.Transport.Contracts.Requests

open System

/// A record representing a request for adding a new document batch statistic.
type BatchStatRequest = {
    StartDate: DateTimeOffset
    EndDate: DateTimeOffset
    NumberOfDocuments: int
    Status: int
    WorkflowId: Guid
}