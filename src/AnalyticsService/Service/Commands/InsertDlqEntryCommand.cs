using System.Text.Json;
using MediatR;

namespace AnalyticsService.Service.Commands;

/// <summary>
/// Command for inserting a new entry into the DLQ table.
/// </summary>
internal sealed record InsertDlqEntryCommand(
    JsonElement RequestBody
) : IRequest<bool>;