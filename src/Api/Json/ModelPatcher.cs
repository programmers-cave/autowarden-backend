using AutoWarden.Core.Models;
using Newtonsoft.Json.Linq;

namespace AutoWarden.Api.Json;

public static class ModelPatcher
{
    public static TResult Patch<TResult, TModel>(TResult objToPatch, MergePatchJson<TModel> patchingData) 
        where TResult : class
        where TModel : class, IModelOf<TResult>, new()
    {
        // Create list of allowed keys.
        var allowedKeys = GetAllowedKeys(typeof(TModel));

        // Preserve only allowed keys in json
        var inputProperties = patchingData.Json.Properties().Select(p => p.Name).ToList();
        foreach (var key in inputProperties.Where(key => !allowedKeys.Contains(key.ToLower())))
        {
            patchingData.Json.Remove(key);
        }
        
        // Merge JSONs.
        var objToPatchJson = JObject.FromObject(objToPatch);
        objToPatchJson.Merge(patchingData.Json, new JsonMergeSettings()
        {
            MergeArrayHandling = MergeArrayHandling.Replace,
            MergeNullValueHandling = MergeNullValueHandling.Merge
        });
        
        return objToPatchJson.ToObject<TResult>()!;
    }

    public static List<string> GetAllowedKeys(Type type) 
    {
        return type.GetProperties()
            .Select(p => p.Name.ToLower())
            .ToList();
    }
}
