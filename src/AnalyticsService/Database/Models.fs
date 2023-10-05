namespace AnalyticsService.Database

open System

/// Module containing domain types.
module Domain =
    /// An enum for representing a status of a document batch.
    type BatchStatus =
        | Success = 0
        | Failed = 1
    
    /// Type representing a statistic about a single processed document batch.
    type BatchStat = {
        Id: Guid
        StartDate: DateTime
        EndDate: DateTime
        NumberOfDocuments: int
        RunTime: TimeSpan
        Status: BatchStatus
    }
    
    /// A constructor function for creating an instance of BatchStat record.
    let NewBatchStat startDate endDate docNumber status =
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
            })
