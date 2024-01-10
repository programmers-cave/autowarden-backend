namespace AutoWarden.Core.Models;

public interface IIndexableObject<TId> where TId : IEquatable<TId>
{
    public TId Id { get; set; }
}
