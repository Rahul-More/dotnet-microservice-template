using Common;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class EfCoreDatabaseProvider : IDatabaseProvider
{
    private readonly DbContext _dbContext;

    public EfCoreDatabaseProvider(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T?> QuerySingleAsync<T>(string sql, object? parameters = null)
    {
        // Example for raw SQL; adapt for your context
        return await _dbContext.Set<T>().FromSqlRaw(sql).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null)
    {
        return await _dbContext.Set<T>().FromSqlRaw(sql).ToListAsync();
    }

    public async Task<int> ExecuteAsync(string sql, object? parameters = null)
    {
        return await _dbContext.Database.ExecuteSqlRawAsync(sql);
    }
}