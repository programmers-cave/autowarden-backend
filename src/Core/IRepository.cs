namespace AutoWarden.Core;

public interface IRepository<T, TId> 
    where T : class 
    where TId: IEquatable<TId>
{
    Task<List<T>> GetRangeAsync(int skip, int take);
    
    Task<T> GetByIdAsync(TId id);
    
    Task<T> CreateAsync(T obj);
    
    Task<T> ReplaceAsync(T obj);
    Task<T> ReplaceByIdAsync(TId id, T obj);
    
    Task<T> UpsertAsync(T obj);
    
    Task DeleteAsync(T obj);
    Task DeleteByIdAsync(TId id);

    Task<int> GetTotalCountAsync();
}
