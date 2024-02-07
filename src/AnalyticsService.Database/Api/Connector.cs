using System.Data;
using Npgsql;

namespace AnalyticsService.Database.Api;

public static class Connector
{
    /// <summary>
    /// Method for obtaining a new instance of a class that implements <see cref="IDbConnection"/>.
    /// </summary>
    public static IDbConnection GetConnection(string? connectionString)
    {
        ArgumentException.ThrowIfNullOrEmpty(connectionString, nameof(connectionString));
        return new NpgsqlConnection(connectionString);
    }
}