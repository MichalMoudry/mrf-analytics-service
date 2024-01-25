using System.Data;
using Npgsql;

namespace AnalyticsService.Database;

public static class Context
{
    // Method for creating a new DB connection.
    public static IDbConnection GetDbConnection(string? connectionString)
    {
        ArgumentNullException.ThrowIfNull(connectionString, nameof(connectionString));
        return new NpgsqlConnection(connectionString);
    }
}
