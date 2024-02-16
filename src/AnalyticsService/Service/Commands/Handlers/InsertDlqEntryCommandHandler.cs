using System.Data;
using System.Text;
using System.Text.Json;
using AnalyticsService.Database.Api;
using AnalyticsService.Database.Model.Domain;
using AnalyticsService.Service.Model;
using MediatR;

namespace AnalyticsService.Service.Commands.Handlers;

/// <summary>
/// A handler class for InsertDlqEntryCommand command.
/// </summary>
internal sealed class InsertDlqEntryCommandHandler(
    IDbConnection dbConnection,
    IDlqRepository dlqRepository)
    : IRequestHandler<InsertDlqEntryCommand, bool>
{
    public async Task<bool> Handle(InsertDlqEntryCommand request, CancellationToken cancellationToken)
    {
        var cloudEvent = request.RequestBody.Deserialize<CloudEvent<JsonElement>>();
        if (cloudEvent == null)
        {
            return false;
        }

        await dlqRepository.NewDlqTopic(
            dbConnection,
            new DeadTopic
            {
                Endpoint = cloudEvent.Topic,
                Source = cloudEvent.Source,
                RequestData = Encoding.UTF8.GetBytes(cloudEvent.Data.ToString())
            }
        );
        return true;
    }
}