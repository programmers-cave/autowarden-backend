using Newtonsoft.Json.Linq;

namespace AutoWarden.Api.Json;

public class MergePatchJson<T> where T : class
{
    public JObject Json { get; set; } = null!;
}
