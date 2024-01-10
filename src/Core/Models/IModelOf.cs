namespace AutoWarden.Core.Models;

// Custom interface, should be implemented for all classes that are used as models in non-core assemblies.
// This is useful to determine generic type constraints correctly.
public interface IModelOf<T> where T : class
{
    
}
