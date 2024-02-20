namespace AnalyticsService.Database.Repositories

open System
open System.Data
open System.Threading.Tasks
open AnalyticsService.Database.Model
open AnalyticsService.Database.Model.Domain
open AnalyticsService.Database.Model.Dto

/// A repository for document batch statistics.
type IBatchStatRepository =
    /// Method for inserting a new record into BatchStats table.
    abstract member AddNewBatchStat: BatchStat -> DatabaseCtx -> Task
    /// Method for obtaining records/stats for a specific workflow.
    abstract member GetBatchStats: Guid -> IDbConnection -> Task<BatchStatInfo seq>
    /// Method for obtaining records/stats of a specific application for a specific period.
    abstract member GetBatchStatsForPeriod:
        Guid -> DateTimeOffset -> TimeSpan -> IDbConnection -> Task<BatchStatInfo seq>