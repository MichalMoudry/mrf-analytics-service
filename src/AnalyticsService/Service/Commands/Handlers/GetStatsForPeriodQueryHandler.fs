namespace AnalyticsService.Service.Queries.Handlers

open AnalyticsService.Service.Commands
open AnalyticsService.Service.Model
open AnalyticsService.Database.Repositories
open MediatR
open System.Data

type GetStatsForPeriodQueryHandler(
    conn: IDbConnection,
    batchRepo: IBatchStatRepository
) =
    interface IRequestHandler<GetStatsForPeriodQuery, GeneralBatchStats>