module AnalyticsService.Database.Context

open System.Data
open Npgsql

/// Method for initializing database.
let DbInit connectionString =
    Dapper.FSharp.PostgreSQL.OptionTypes.register()
    new NpgsqlConnection(connectionString) :> IDbConnection