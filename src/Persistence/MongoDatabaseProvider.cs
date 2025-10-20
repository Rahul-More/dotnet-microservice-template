using Common;
using MongoDB.Driver;

namespace Persistence;

public class MongoDatabaseProvider : IDatabaseProvider
{
    private readonly IMongoDatabase _database;

    public MongoDatabaseProvider(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    // Example for document queries
    public async Task<T?> QuerySingleAsync<T>(string collection, FilterDefinition<T> filter)
    {
        var coll = _database.GetCollection<T>(collection);
        return await coll.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string collection, FilterDefinition<T> filter)
    {
        var coll = _database.GetCollection<T>(collection);
        return await coll.Find(filter).ToListAsync();
    }

    public async Task<int> ExecuteAsync<T>(string collection, UpdateDefinition<T> update, FilterDefinition<T> filter)
    {
        var coll = _database.GetCollection<T>(collection);
        var result = await coll.UpdateOneAsync(filter, update);
        return (int)result.ModifiedCount;
    }
}