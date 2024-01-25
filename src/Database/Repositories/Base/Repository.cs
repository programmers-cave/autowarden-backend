using AutoMapper;
using AutoWarden.Core;
using AutoWarden.Core.Exceptions;
using AutoWarden.Core.Models;
using AutoWarden.Database.Entities;
using MongoDB.Driver;

namespace AutoWarden.Database.Repositories.Base;

public abstract class Repository<T, TEntity, TId> : ReadOnlyRepository<T, TEntity, TId>, IRepository<T, TId>
    where T : class, IIndexableObject<TId>
    where TEntity : class, IEntity<TId>
    where TId: IEquatable<TId>
{
    protected Repository(IMapper mapper, MongoDbService mongoDbService) : base(mapper, mongoDbService)
    {
    }

    public virtual async Task<T> CreateAsync(T obj)
    {
        var entity = _mapper.Map<TEntity>(obj);
        await _collection.InsertOneAsync(entity);
        obj.Id = entity.Id;
        
        return obj;
    }

    public virtual async Task<T> ReplaceAsync(T obj)
    {
        var entity = _mapper.Map<TEntity>(obj);
        var result = await _collection.ReplaceOneAsync(
            filter: x => x.Id.Equals(entity.Id),
            options: new ReplaceOptions { IsUpsert = false },
            replacement: entity);

        if (result.MatchedCount == 0)
            throw new EntityNotFoundException($"Cannot perform ReplaceAsync action. '{typeof(T).FullName}' entity with id '{entity.Id}' not found.");
        
        return obj;
    }
    
    public virtual async Task<T> ReplaceByIdAsync(TId id, T obj)
    {
        var entity = _mapper.Map<TEntity>(obj);
        var result = await _collection.ReplaceOneAsync(
            filter: x => x.Id.Equals(id),
            options: new ReplaceOptions { IsUpsert = false },
            replacement: entity);
        
        if (result.MatchedCount == 0)
            throw new EntityNotFoundException($"Cannot perform ReplaceByIdAsync action. '{typeof(T).FullName}' entity with id '{id}' not found.");
        
        return obj;
    }

    public virtual async Task<T> UpsertAsync(T obj)
    {
        var entity = _mapper.Map<TEntity>(obj);
        await _collection.ReplaceOneAsync(
            filter: x => x.Id.Equals(entity.Id),
            options: new ReplaceOptions { IsUpsert = true },
            replacement: entity);

        return obj;
    }

    public virtual async Task DeleteAsync(T obj)
    {
        var entity = _mapper.Map<TEntity>(obj);
        var result = await _collection.DeleteOneAsync(x => x.Id.Equals(entity.Id));

        if (result.DeletedCount == 0)
            throw new EntityNotFoundException($"Cannot perform DeleteAsync action. '{typeof(T).FullName}' entity with id '{entity.Id}' not found.");
    }

    public virtual async Task DeleteByIdAsync(TId id)
    {
        var result = await _collection.DeleteOneAsync(x => x.Id.Equals(id));

        if (result.DeletedCount == 0)
            throw new EntityNotFoundException($"Cannot perform DeleteByIdAsync action. '{typeof(T).FullName}' entity with id '{id}' not found.");
    }
}
