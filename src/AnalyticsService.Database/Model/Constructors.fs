/// Module containing constructor functions for domain objects.
module AnalyticsService.Database.Model.Constructors

open System
open AnalyticsService.Database.Model.Domain

let NewDeadTopic endPoint requestData source =
    {
        Id = Guid.NewGuid()
        Endpoint = endPoint
        RequestData = requestData
        Source = source
        DateAdded = DateTimeOffset.Now
    }

let NewBatchStat startDate endDate docNumber status workflowId =
    {
        Id = Guid.NewGuid()
        StartDate = startDate
        EndDate = endDate
        NumberOfDocuments = docNumber
        Status = status
        WorkflowId = workflowId
        DateAdded = DateTimeOffset.Now
    }
