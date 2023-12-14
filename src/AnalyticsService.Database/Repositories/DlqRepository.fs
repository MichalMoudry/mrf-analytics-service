/// Module containing code related to operations for a dead letter database table.
[<Sealed>]
module AnalyticsService.Database.Repositories.DlqRepository

open Dapper.FSharp.PostgreSQL

let private dlqTable = table'<Dead> "DLQ"
