using AutoMapper;
using AutoWarden.Core;
using AutoWarden.Core.Exceptions;
using AutoWarden.Core.Models;
using AutoWarden.Database.Entities;
using MongoDB.Driver;

namespace AutoWarden.Database.Repositories.Base;

public abstract class ReadOnlyRepository<T, TEntity, TId> : IReadOnlyRepository<T, TId>
    where T : class, IIndexableObject<TId>
    where TEntity : class, IEntity<TId>
    where TId: IEquatable<TId>
{
    private protected readonly IMapper _mapper;
    private protected readonly IMongoCollection<TEntity> _collection;

    protected ReadOnlyRepository(IMapper mapper, MongoDbService mongoDbService)
    {
        _mapper = mapper;
        _collection = mongoDbService.GetCollection<TEntity>();
    }

    public virtual async Task<List<T>> GetRangeAsync(int skip, int take)
    {
        var entities = await _collection
            .Find(x => true)
            .Skip(skip)
            .Limit(take)
            .ToListAsync();

        return _mapper.Map<List<T>>(entities);
    }

    public virtual async Task<T> GetByIdAsync(TId id)
    {
        var entity = await _collection
            .Find(x => x.Id.Equals(id))
            .SingleOrDefaultAsync();

        if (entity is null)
            throw new EntityNotFoundException($"Cannot perform GetByIdAsync action. '{typeof(T).FullName}' entity with id '{id}' not found.");

        return _mapper.Map<T>(entity);
    }

    public virtual async Task<int> GetTotalCountAsync()
    {
        return (int) await _collection.CountDocumentsAsync(x => true);
    }
}
