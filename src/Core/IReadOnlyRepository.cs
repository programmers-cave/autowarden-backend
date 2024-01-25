namespace AutoWarden.Core;

public interface IReadOnlyRepository<T, TId>
    where T : class
    where TId : IEquatable<TId>
{
    Task<List<T>> GetRangeAsync(int skip, int take);

    Task<T> GetByIdAsync(TId id);

    Task<int> GetTotalCountAsync();
}
