using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AutoWarden.Api.Json;

public class MergePatchJsonConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(MergePatchJson<>);
    }

    public override bool CanWrite => false;
    
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException("MergePatchJsonConverter cannot serialize to JSON.");
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        var jObject = JObject.Load(reader);
        var target = (dynamic)Activator.CreateInstance(objectType)!;
        target.Json = jObject;

        return target;
    }
}
