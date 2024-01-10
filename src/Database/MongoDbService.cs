using AutoWarden.Database.Attributes;
using AutoWarden.Database.Entities;
using MongoDB.Driver;

namespace AutoWarden.Database;

public class MongoDbService
{
    private readonly IMongoDatabase _database;
    
    public MongoDbService(MongoDbSettings mongoDbSettings)
    {
        var client = new MongoClient(mongoDbSettings.ConnectionString);
        _database = client.GetDatabase(mongoDbSettings.DatabaseName);
    }
    
    public IMongoCollection<T> GetCollection<T, TId>() 
        where T : IEntity<TId> 
        where TId: IEquatable<TId>
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
