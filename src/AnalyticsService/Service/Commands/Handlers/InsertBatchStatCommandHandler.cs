using System.Data;
using AnalyticsService.Database.Api;
using AnalyticsService.Database.Model.Domain;
using MediatR;

namespace AnalyticsService.Service.Commands.Handlers;

/// <summary>
/// A handler for inserting a new batch stat to the database.
/// </summary>
internal sealed class InsertBatchStatCommandHandler(
    IDbConnection connection,
    IBatchStatRepository batchStatRepository)
    : IRequestHandler<InsertBatchStatCommand, bool>
{
    public async Task<bool> Handle(InsertBatchStatCommand request, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return false;
        }

        await batchStatRepository.AddNewBatchStat(
            connection,
            new BatchStat
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                NumberOfDocuments = request.DocsNumber,
                WorkflowId = request.WorkflowId,
                Status = request.Status
            }
        );
        return true;
    }
}