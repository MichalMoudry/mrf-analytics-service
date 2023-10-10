namespace AnalyticsService.Database

open System
open System.ComponentModel

/// Module containing domain types.
module Domain =
    /// An enum for representing a status of a document batch.
    type BatchStatus =
        | Success = 0
        | Failed = 1
    
    /// Type representing a statistic about a single processed document batch.
    type BatchStat = {
        [<Description("id")>]
        Id: Guid

        [<Description("start_date")>]
        StartDate: DateTime

        [<Description("end_date")>]
        EndDate: DateTime

        [<Description("number_of_documents")>]
        NumberOfDocuments: int

        [<Description("run_time")>]
        RunTime: TimeSpan

        [<Description("status")>]
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
