using System.Text.Json;
using NotesByDayApi.Exceptions;

namespace NotesByDayApi.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotesByDayException ex)
        {
            _logger.LogError(ex, ex.Message);
            await HandleCustomExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            await HandleGenericExceptionAsync(context, ex);
        }
    }

    private static Task HandleCustomExceptionAsync(HttpContext context, NotesByDayException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = exception.StatusCode;

        return context.Response.WriteAsync(new ErrorDetails
        {
            StatusCode = exception.StatusCode,
            ErrorType = exception.ErrorType,
            Message = exception.Message
        }.ToString());
    }

    private static Task HandleGenericExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 500;

        return context.Response.WriteAsync(new ErrorDetails
        {
            StatusCode = 500,
            ErrorType = "InternalServerError",
            Message = "Something went wrong"
        }.ToString());
    }
}

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string ErrorType { get; set; }
    public string Message { get; set; }

    public override string ToString() => JsonSerializer.Serialize(this);
}