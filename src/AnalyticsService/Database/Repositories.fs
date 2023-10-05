namespace AnalyticsService.Database

open AnalyticsService.Database.Domain
open Dapper.FSharp.PostgreSQL

/// A module for batch statistics.
module BatchStatRepository =
    let table = table'<BatchStat> "batch_stats"

    let insertRecord =
        ""
