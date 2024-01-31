using System.Data;

namespace AnalyticsService.Database.Api;

/// <summary>
/// A builder for a database connection object.
/// </summary>
public interface IConnectionBuilder
{
    /// <summary>
    /// Method for setting a connection string property.
    /// </summary>
    /// <param name="connectionString"></param>
    IConnectionBuilder SetConnectionString(string? connectionString);

    /// <summary>
    /// Method for building the database connection.
    /// </summary>
    IDbConnection Build();
}