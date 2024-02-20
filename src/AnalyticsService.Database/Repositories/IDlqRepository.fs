namespace AnalyticsService.Database.Repositories

open System
open System.Data
open System.Threading.Tasks
open AnalyticsService.Database.Model
open AnalyticsService.Database.Model.Domain
open AnalyticsService.Database.Model.Dto

/// Repository for a dead letter queue.
type public IDlqRepository =
    /// Method for inserting a new row into the DLQ table.
    abstract member NewDlqTopic: DeadTopic -> DatabaseCtx -> Task<int>
    /// Method for selecting a limited amount of items in a dead letter queue.
    abstract member GetDlqItems: IDbConnection -> int -> Task<DeadTopicInfo seq>
    /// Method for deleting a batch of DLQ items based on supplied identifiers.
    abstract member DeleteDlqItems: seq<Guid> -> DatabaseCtx -> Task<int>