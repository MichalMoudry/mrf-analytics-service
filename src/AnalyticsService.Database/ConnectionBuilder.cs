using System.Data;
using AnalyticsService.Database.Api;
using Npgsql;

namespace AnalyticsService.Database;

public sealed class ConnectionBuilder : IConnectionBuilder
{
    private string? _connectionString;

    /// <inheritdoc/>
    public IConnectionBuilder SetConnectionString(string? connectionString)
    {
        _connectionString = connectionString;
        return this;
    }

    /// <inheritdoc/>
    public IDbConnection Build()
    {
        ArgumentException.ThrowIfNullOrEmpty(_connectionString, nameof(_connectionString));
        return new NpgsqlConnection(_connectionString);
    }
}
