namespace AnalyticsService.Database.Model.Domain;

/// <summary>
/// A base type for all entities in the system.
/// </summary>
public abstract class Entity
{
    /// <summary>
    /// Identifier of an entity.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// The time when entity was created in the system.
    /// </summary>
    public DateTime DateAdded { get; init; }
}