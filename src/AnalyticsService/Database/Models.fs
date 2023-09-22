namespace AnalyticsService.Database

open System

/// Module containing domain types.
module Domain =
    /// Type representing a statistic about a single processed document batch.
    type BatchStat = {
        Id: Guid
        StartDate: DateTime
        EndDate: DateTime
        NumberOfDocuments: uint32
        RunTime: DateTime
    }