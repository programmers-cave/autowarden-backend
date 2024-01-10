namespace AutoWarden.Api.Json;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class UnpatchableAttribute : Attribute
{
    public UnpatchableAttribute()
    {
    }
}
