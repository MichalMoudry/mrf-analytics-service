namespace AnalyticsService.Database

open System.Data

module Management =
    let init(connection: IDbConnection) =
        task {
            Dapper.FSharp.PostgreSQL.OptionTypes.register()
            return ()
        }