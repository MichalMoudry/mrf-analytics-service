namespace AnalyticsService.Database.Model.Dto;

/// <summary>
/// A data-transfer-object for DLQ processing.
/// </summary>
public sealed class DeadTopicInfo
{
    public Guid Id { get; init; }

    public string? Endpoint { get; init; }

    public byte[] RequestData { get; init; } = Array.Empty<byte>();
}