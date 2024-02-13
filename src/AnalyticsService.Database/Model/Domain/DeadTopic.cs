namespace AnalyticsService.Database.Model.Domain;

/// <summary>
/// A record representing a dead topic that was meant for this service.
/// </summary>
public sealed class DeadTopic : Entity
{
    public string? Endpoint { get; init; }

    public byte[] RequestData { get; init; } = Array.Empty<byte>();

    public string? Source { get; init; }

    public DeadTopic()
    {
        Id = Guid.NewGuid();
        DateAdded = DateTime.UtcNow;
    }
}