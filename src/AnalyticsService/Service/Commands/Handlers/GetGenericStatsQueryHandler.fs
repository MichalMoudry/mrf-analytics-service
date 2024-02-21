namespace AnalyticsService.Service.Queries.Handlers

open AnalyticsService.Service.Commands
open AnalyticsService.Service.Model
open AnalyticsService.Database.Repositories
open MediatR
open System.Data

/// A handler for retrieving generic stats about all document batches in a specified workflow.
type GetGenericStatsQueryHandler(
    conn: IDbConnection,
    batchRepo: IBatchStatRepository
) =
    interface IRequestHandler<GetGenericStatsQuery, GeneralBatchStats>