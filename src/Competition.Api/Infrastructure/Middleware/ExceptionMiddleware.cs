using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Shop.Application;

namespace Competition.Api.Infrastructure.Middleware;

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
        catch (GameValidationException ex)
        {
            _logger.LogWarning($"Validation failed");
            await HandleValidationExceptionAsync(httpContext, ex);
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning("Entity not found");
            await HandleNotFoundAsync(httpContext, ex);
        }
        catch (Exception ex)
        {
            _logger.LogError("Something went wrong: {Ex}", ex);
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static Task HandleValidationExceptionAsync(HttpContext context, GameValidationException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status400BadRequest;

        var problemDetails = new ProblemDetails
        {
            Title = "Validation Error",
            Detail = "Validation failed",
            Status = StatusCodes.Status400BadRequest,
            Instance = context.Request.Path,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };
        
        problemDetails.Extensions.Add(new KeyValuePair<string, object?>("errors", exception.Errors));
        
        string data = JsonSerializer.Serialize(problemDetails);

        return context.Response.WriteAsync(data);
    }
    
    private static Task HandleNotFoundAsync(HttpContext context, NotFoundException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status404NotFound;

        var problemDetails = new ProblemDetails
        {
            Title = "Not Found",
            Detail = "Not found",
            Status = StatusCodes.Status404NotFound,
            Instance = context.Request.Path,
            Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.4"
        };
        
        string data = JsonSerializer.Serialize(problemDetails);

        return context.Response.WriteAsync(data);
    }
    
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        string data = JsonSerializer.Serialize(new ProblemDetails
        {
            Status = context.Response.StatusCode,
            Title = "Internal Server Error",
            Detail = exception.Message,
            Instance = context.Request.Path,
            Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.6.1"
        });

        return context.Response.WriteAsync(data);
    }
}