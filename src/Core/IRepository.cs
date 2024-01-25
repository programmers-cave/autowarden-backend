namespace AutoWarden.Core;

public interface IRepository<T, TId> : IReadOnlyRepository<T, TId>
    where T : class 
    where TId: IEquatable<TId>
{
    Task<T> CreateAsync(T obj);
    
    Task<T> ReplaceAsync(T obj);
    Task<T> ReplaceByIdAsync(TId id, T obj);
    
    Task<T> UpsertAsync(T obj);
    
    Task DeleteAsync(T obj);
    Task DeleteByIdAsync(TId id);
}
