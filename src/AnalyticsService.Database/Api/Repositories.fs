namespace AnalyticsService.Database.Api

open System.Threading.Tasks
open System.Data

// An interface for a DLQ repository.
type IDlqRepository =
    /// Method for selecting all items from dead letter queue.
    abstract GetDlqItems: IDbConnection -> Task<seq<DlqItemProcessingInfo>>
