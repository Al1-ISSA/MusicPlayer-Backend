using System.Net;
using System.Text.Json;
using MusicBackend.Exceptions;

namespace MusicBackend.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, exception.Message);

        var code = HttpStatusCode.InternalServerError;
        
        if (exception is NotFoundException) code = HttpStatusCode.NotFound;
        else if (exception is AlreadyExistsException) code = HttpStatusCode.BadRequest;
        else if (exception is BadRequestException) code = HttpStatusCode.BadRequest;
        else if (exception is HttpException) code = (HttpStatusCode)((HttpException)exception).StatusCode;

        var result = JsonSerializer.Serialize(new { error = exception.Message });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}