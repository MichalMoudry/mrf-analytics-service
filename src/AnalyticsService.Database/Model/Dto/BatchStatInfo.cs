namespace AnalyticsService.Database.Model.Dto;

/// <summary>
/// A data-transfer-object representing a single result of 
/// </summary>
public sealed class BatchStatInfo
{
    public Guid Id { get; init; }

    public DateTimeOffset StartDate { get; init; }

    public DateTimeOffset EndDate { get; init; }

    public int NumberOfDocuments { get; init; }

    public TimeSpan RunTime { get; init; }
}