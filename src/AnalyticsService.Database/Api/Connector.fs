[<Sealed>]
module AnalyticsService.Database.Api.Connector

open System.Data
open Npgsql

let GetConnection connStr =
    new NpgsqlConnection(connStr) :> IDbConnection
