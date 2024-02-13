using System.Text;
using System.Text.Json;
using AnalyticsService.Database.Model.Domain;
using Dapr;
using MediatR;

namespace AnalyticsService.Service.Commands.Handlers;

/// <summary>
/// A handler class for InsertDlqEntryCommand command.
/// </summary>
internal sealed class InsertDlqEntryCommandHandler : IRequestHandler<InsertDlqEntryCommand, bool>
{
    public async Task<bool> Handle(InsertDlqEntryCommand request, CancellationToken cancellationToken)
    {
        using var streamReader = new StreamReader(request.RequestBody);
        var requestBody = await streamReader.ReadToEndAsync(cancellationToken);
        var cloudEvent = JsonSerializer.Deserialize<CloudEvent>(requestBody);
        if (cloudEvent == null)
        {
            return false;
        }

        var topic = new DeadTopic
        {
            Endpoint = cloudEvent.Subject,
            Source = cloudEvent.Source.AbsoluteUri,
            RequestData = Encoding.UTF8.GetBytes(requestBody)
        };
        return true;
    }
}