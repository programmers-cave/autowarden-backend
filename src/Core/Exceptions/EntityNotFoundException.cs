namespace AutoWarden.Core.Exceptions;

/// <summary>
/// Thrown by adapter when entity not found in action that require entity existence (e.g. GetById or ReplaceById).
/// </summary>
public class EntityNotFoundException : HandledException
{
    public override string ErrorCode { get; } = "z6fe-zlw4-iybg";
    
    public EntityNotFoundException(string message) : base(message)
    {
    }
}
