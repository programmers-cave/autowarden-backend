using System.Net;
using AutoWarden.Core.Exceptions;
using Newtonsoft.Json;

namespace AutoWarden.Api.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;
    
    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (HandledException e)
        {
            _logger.LogError(e, $"Error during execution of {context}", context.Request.Path.Value);

            var response = context.Response;
            response.ContentType = "application/json";
            var (status, message) = GetResponse(e);
            response.StatusCode = (int) status;
            await response.WriteAsync(message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Unhandled exception during execution of {context}", context.Request.Path.Value);

            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = 500;
            await response.WriteAsync(JsonConvert.SerializeObject(new {message = e.Message, errorCode = "0000-0000-0000"}));
        }
    }
    
    private static (HttpStatusCode code, string reponse) GetResponse(HandledException e)
    {
        return e switch
        {
            EntityNotFoundException => (
                HttpStatusCode.NotFound,
                JsonConvert.SerializeObject(new {message = e.Message, errorCode = e.ErrorCode})
            ),
            _ => (
                HttpStatusCode.InternalServerError,
                JsonConvert.SerializeObject(new {message = e.Message, errorCode = "0000-0000-0001"}) 
            )
        };
    }
}
