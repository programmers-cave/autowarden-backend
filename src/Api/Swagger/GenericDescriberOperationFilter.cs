using AutoWarden.Api.Controllers;
using Humanizer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AutoWarden.Api.Swagger;

public class GenericDescriberOperationFilter : IOperationFilter
{
    private string? _controllerName = null;
    
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.MethodInfo.DeclaringType is not {IsGenericType: true} || context.MethodInfo.DeclaringType.GetGenericTypeDefinition() != typeof(CrudBaseController<,,,>))
            return;
        
        _controllerName ??= operation.Tags[0].Name;
        
        // Change descriptions
        operation.Summary = ResolveString(operation.Summary);
        operation.Description = ResolveString(operation.Description);
        operation.OperationId = ResolveString(operation.OperationId);

        foreach (var key in operation.Responses.Keys)
        {
            operation.Responses[key].Description = ResolveString(operation.Responses[key].Description);
        }
    }

    private string ResolveString(string stringToResolve)
    {
        return stringToResolve
            .Replace("[ControllerName]", _controllerName)
            .Replace("[ControllerNameVerbose]", _controllerName.Humanize().Transform(To.TitleCase));
    }
}
