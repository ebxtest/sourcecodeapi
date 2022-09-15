using Newtonsoft.Json;
using System.Net;

namespace SourceCodeApi.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex, HttpStatusCode.InternalServerError);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex, HttpStatusCode statusCode)
    {
        _logger.LogError(ex.Message, ex);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var errorDetails = new
        {
            Message = ex.Message
        };

        await context.Response.WriteAsync(JsonConvert.SerializeObject(errorDetails));
    }
}