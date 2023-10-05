module AnalyticsService.Database.Context

open System.Data
open Npgsql

/// Method for opening a new connection to the database.
let GetConnection connectionString =
    new NpgsqlConnection(connectionString) :> IDbConnection
