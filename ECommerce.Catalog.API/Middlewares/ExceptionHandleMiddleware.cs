using System.Diagnostics;
using System.Net;
using System.Text.Json;
using ECommerce.Common.Exceptions;

namespace ECommerce.Catalog.API.Middlewares;

public class ExceptionHandleMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandleMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionHandleMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception has occurred while processing the request.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var (statusCode, message) = exception switch
        {
            DomainException => (HttpStatusCode.BadRequest, exception.Message),
            NotFoundException => (HttpStatusCode.NotFound, exception.Message),
            UnauthorizedAccessException => (HttpStatusCode.Unauthorized, exception.Message),
            _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred.")
        };

        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            error = message,
            statusCode = context.Response.StatusCode,
            timestamp = DateTime.UtcNow
        };

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
    }
}