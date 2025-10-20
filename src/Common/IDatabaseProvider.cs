namespace Common;

public interface IDatabaseProvider
{
    Task<T?> QuerySingleAsync<T>(string sql, object? parameters = null);
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null);
    Task<int> ExecuteAsync(string sql, object? parameters = null);
    // Add more abstracted methods as needed
}