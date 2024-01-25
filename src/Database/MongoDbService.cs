using AutoWarden.Database.Attributes;
using MongoDB.Driver;

namespace AutoWarden.Database;

public class MongoDbService
{
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;
    
    public MongoDbService(MongoDbSettings mongoDbSettings)
    {
        _client = new MongoClient(mongoDbSettings.ConnectionString);
        _database = _client.GetDatabase(mongoDbSettings.DatabaseName);
    }

    public IMongoClient GetClient() => _client;

    public IMongoDatabase GetDatabase() => _database;

    public IMongoCollection<T> GetCollection<T>()
    {
        return _database.GetCollection<T>(GetCollectionName(typeof(T)));
    }

    private static string GetCollectionName(Type entityType)
    {
        return ((BsonCollectionAttribute) entityType.GetCustomAttributes(
                typeof(BsonCollectionAttribute),
                true)
            .FirstOrDefault()!).CollectionName;
    }
}
