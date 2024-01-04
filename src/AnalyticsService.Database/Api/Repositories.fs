namespace AnalyticsService.Database.Api

open System
open System.Threading.Tasks
open System.Data
open AnalyticsService.Database.Model

// An interface for a DLQ repository.
type IDlqRepository =
    /// Method for selecting all items from dead letter queue.
    abstract GetDlqItems: IDbConnection -> Task<seq<DlqItemProcessingInfo>>
    /// Method for inserting a new row into the DLQ table.
    abstract NewDlqItem: DeadTopic -> IDbConnection -> Task<unit>
    /// Method for deleting a batch of DLQ items.
    abstract DeleteDlqItems: seq<Guid> -> IDbConnection -> Task<unit>
