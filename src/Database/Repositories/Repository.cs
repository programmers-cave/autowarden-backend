using AutoMapper;
using AutoWarden.Database.Entities;
using AutoWarden.Core;
using AutoWarden.Core.Exceptions;
using AutoWarden.Core.Models;
using MongoDB.Driver;

namespace AutoWarden.Database.Repositories;

public abstract class Repository<T, TEntity, TId> : IRepository<T, TId>
    where T : class, IIndexableObject<TId>
    where TEntity : class, IEntity<TId>
    where TId: IEquatable<TId>
{
    protected readonly IMapper Mapper;
    protected readonly IMongoCollection<TEntity> Collection;
    
    protected Repository(IMapper mapper, MongoDbService mongoDbService)
    {
        Mapper = mapper;
        Collection = mongoDbService.GetCollection<TEntity, TId>();
    }
    
    public virtual async Task<List<T>> GetRangeAsync(int skip, int take)
    {
        var entities = await Collection
            .Find(x => true)
            .Skip(skip)
            .Limit(take)
            .ToListAsync();

        return Mapper.Map<List<T>>(entities);
    }
    
    public virtual async Task<T> GetByIdAsync(TId id)
    {
        var entity = await Collection
            .Find(x => x.Id.Equals(id))
            .SingleOrDefaultAsync();

        if (entity is null)
            throw new EntityNotFoundException($"Cannot perform GetByIdAsync action. '{typeof(T).FullName}' entity with id '{id}' not found.");
        
        return Mapper.Map<T>(entity);
    }

    public virtual async Task<T> CreateAsync(T obj)
    {
        var entity = Mapper.Map<TEntity>(obj);
        await Collection.InsertOneAsync(entity);
        obj.Id = entity.Id;
        
        return obj;
    }

    public virtual async Task<T> ReplaceAsync(T obj)
    {
        var entity = Mapper.Map<TEntity>(obj);
        var result = await Collection.ReplaceOneAsync(
            filter: x => x.Id.Equals(entity.Id),
            options: new ReplaceOptions { IsUpsert = false },
            replacement: entity);

        if (result.MatchedCount == 0)
            throw new EntityNotFoundException($"Cannot perform ReplaceAsync action. '{typeof(T).FullName}' entity with id '{entity.Id}' not found.");
        
        return obj;
    }
    
    public virtual async Task<T> ReplaceByIdAsync(TId id, T obj)
    {
        var entity = Mapper.Map<TEntity>(obj);
        var result = await Collection.ReplaceOneAsync(
            filter: x => x.Id.Equals(id),
            options: new ReplaceOptions { IsUpsert = false },
            replacement: entity);
        
        if (result.MatchedCount == 0)
            throw new EntityNotFoundException($"Cannot perform ReplaceByIdAsync action. '{typeof(T).FullName}' entity with id '{id}' not found.");
        
        return obj;
    }

    public virtual async Task<T> UpsertAsync(T obj)
    {
        var entity = Mapper.Map<TEntity>(obj);
        await Collection.ReplaceOneAsync(
            filter: x => x.Id.Equals(entity.Id),
            options: new ReplaceOptions { IsUpsert = true },
            replacement: entity);

        return obj;
    }

    public virtual async Task DeleteAsync(T obj)
    {
        var entity = Mapper.Map<TEntity>(obj);
        var result = await Collection.DeleteOneAsync(x => x.Id.Equals(entity.Id));

        if (result.DeletedCount == 0)
            throw new EntityNotFoundException($"Cannot perform DeleteAsync action. '{typeof(T).FullName}' entity with id '{entity.Id}' not found.");
    }

    public virtual async Task DeleteByIdAsync(TId id)
    {
        var result = await Collection.DeleteOneAsync(x => x.Id.Equals(id));

        if (result.DeletedCount == 0)
            throw new EntityNotFoundException($"Cannot perform DeleteByIdAsync action. '{typeof(T).FullName}' entity with id '{id}' not found.");
    }

    public virtual async Task<int> GetTotalCountAsync()
    {
        return (int) await Collection.CountDocumentsAsync(x => true);
    }
}
