namespace AutoWarden.Core.Exceptions;

/// <summary>
/// Every exception that will be handled with any sort of exception middleware should inherit from this class.
/// </summary>
public abstract class HandledException : Exception
{
    public abstract string ErrorCode { get; }
    
    protected HandledException(string message) : base(message)
    {
        
    }
}
