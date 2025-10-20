using Common;
using Dapper;
using Npgsql;
using System.Data;

namespace Persistence;

public class DapperDatabaseProvider : IDatabaseProvider
{
    private readonly string _connectionString;

    public DapperDatabaseProvider(string connectionString)
    {
        _connectionString = connectionString;
    }

    private IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);

    public async Task<T?> QuerySingleAsync<T>(string sql, object? parameters = null)
    {
        using var conn = CreateConnection();
        return await conn.QuerySingleOrDefaultAsync<T>(sql, parameters);
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null)
    {
        using var conn = CreateConnection();
        return await conn.QueryAsync<T>(sql, parameters);
    }

    public async Task<int> ExecuteAsync(string sql, object? parameters = null)
    {
        using var conn = CreateConnection();
        return await conn.ExecuteAsync(sql, parameters);
    }
}