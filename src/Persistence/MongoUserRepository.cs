using Domain.Entities;
using MongoDB.Driver;

namespace Persistence;

public class MongoUserRepository
{
    private readonly IMongoCollection<User> _collection;

    public MongoUserRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<User>("users");
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Id, id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task CreateUserAsync(User user)
    {
        await _collection.InsertOneAsync(user);
    }
}