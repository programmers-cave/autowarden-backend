using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AutoWarden.Api.Swagger;

public class PatchMethodOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.ApiDescription.HttpMethod != null && !context.ApiDescription.HttpMethod.Equals("PATCH"))
            return;

        var requestBody = operation.RequestBody;
        if (requestBody == null)
            return;

        foreach (var content in requestBody.Content)
        {
            var schema = content.Value.Schema;
            if (schema == null)
                continue;

            var modelType = context.ApiDescription.ParameterDescriptions
                .FirstOrDefault(p => p.Source == BindingSource.Body)?.ModelMetadata?.ModelType;

            if (modelType is not {IsGenericType: true})
                continue;

            var genericArgType = modelType.GenericTypeArguments[0];
            var newSchema = context.SchemaGenerator.GenerateSchema(genericArgType, context.SchemaRepository);

            content.Value.Schema = newSchema;
            
            // Drop old schema
            context.SchemaRepository.Schemas.Remove(schema.Reference.Id);
        }
    }
}
