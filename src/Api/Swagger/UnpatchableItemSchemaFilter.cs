using AutoWarden.Api.Json;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AutoWarden.Api.Swagger;

public class UnpatchableItemSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema?.Properties == null)
            return;

        // var excludedProperties = context.Type.GetProperties()
        //     .Where(p => p.GetCustomAttribute<UnpatchableAttribute>() != null);
        
        // pass info there's is a patch item.
        // (maybe additional parameters in PatchMethodOperationFilter?)
        // or create another model in PatchMethodOperationFilter called ActionDefinitionRequestModelPatch ?
        
        
        // Check if request is PATCH method
        var allowedKeys = ModelPatcher.GetAllowedKeys(context.Type);
        
        foreach (var property in schema.Properties)
        {
            if (!allowedKeys.Contains(property.Key.ToLower()))
                property.Value.ReadOnly = true;
        }
    }
}
