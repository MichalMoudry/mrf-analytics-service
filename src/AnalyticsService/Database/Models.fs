namespace AnalyticsService.Database

open System

/// Module containing domain types.
module Domain =
    /// An enum for representing a status of a document batch.
    type BatchStatus =
        | Success = 0
        | Failed = 1
    
    /// Type representing a statistic about a single processed document batch.
    [<CLIMutable>]
    type BatchStat = {
        Id: Guid
        StartDate: DateTime
        EndDate: DateTime
        NumberOfDocuments: int
        RunTime: TimeSpan
        Status: BatchStatus
        WorkflowId: Guid
        Created: DateTime
    }
    
    /// A constructor function for creating an instance of BatchStat record.
    let NewBatchStat startDate endDate docNumber status workflowId =
        if startDate > endDate then
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
                Created = DateTime.Now 
            })

    /// A record representing a dead topic that was meant for this service.
    [<CLIMutable>]
    type DeadTopic = {
        Id: Guid
        Endpoint: string
        RequestData: byte[]
        Source: string
        DateAdded: DateTime
    }

    /// Constructor method for DeadTopic record.
    let NewDeadTopic data source = {
        Id = Guid.NewGuid()
        Endpoint = "[temp]"
        RequestData = data
        Source = source
        DateAdded = DateTime.Now 
    }
