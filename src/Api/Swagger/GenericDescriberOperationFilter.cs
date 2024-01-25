using AutoWarden.Api.Controllers;
using Humanizer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AutoWarden.Api.Swagger;

public class GenericDescriberOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.MethodInfo.DeclaringType is not {IsGenericType: true} ||
            (
                context.MethodInfo.DeclaringType.GetGenericTypeDefinition() != typeof(CrudBaseController<,,,,,>)
                && context.MethodInfo.DeclaringType.GetGenericTypeDefinition() != typeof(ReadBaseController<,,,>)
            ))
            return;
        
        var controllerName = operation.Tags[0].Name;
        
        // Change descriptions
        operation.Summary = ResolveString(controllerName, operation.Summary);
        operation.Description = ResolveString(controllerName, operation.Description);
        operation.OperationId = ResolveString(controllerName, operation.OperationId);

        foreach (var key in operation.Responses.Keys)
        {
            operation.Responses[key].Description = ResolveString(controllerName, operation.Responses[key].Description);
        }
    }

    private string ResolveString(string controllerName, string stringToResolve)
    {
        return stringToResolve
            .Replace("[ControllerName]", controllerName)
            .Replace("[ControllerNameVerbose]", controllerName.Humanize().Transform(To.TitleCase));
    }
}
