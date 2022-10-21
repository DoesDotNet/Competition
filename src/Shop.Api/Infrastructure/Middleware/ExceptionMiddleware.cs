using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Shop.Application;

namespace Shop.Api.Infrastructure.Middleware;

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
        catch (ShopValidationException ex)
        {
            _logger.LogWarning($"Validation failed");
            await HandleValidationExceptionAsync(httpContext, ex);
        }
        catch (Exception ex)
        {
            _logger.LogError("Something went wrong: {Ex}", ex);
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static Task HandleValidationExceptionAsync(HttpContext context, ShopValidationException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status400BadRequest;

        var problemDetails = new ProblemDetails
        {
            Title = "Validation Error",
            Detail = "Validation failed",
            Status = StatusCodes.Status400BadRequest,
            Instance = context.Request.Path
        };
        
        foreach (KeyValuePair<string,string> error in exception.Errors)
        {
            problemDetails.Extensions.Add(error.Key, error.Value);
        }
        
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
            Instance = context.Request.Path
        });

        return context.Response.WriteAsync(data);
    }
}