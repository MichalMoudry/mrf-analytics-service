namespace AnalyticsService.Database.Model

open System.Data

/// A record for wrapping an entire DB context for repositories.
type DatabaseCtx = {
    Conn: IDbConnection
    Tx: IDbTransaction
}
