using System.Net;
using MedTimeSlot.DataAccess.DataAccess;
using MedTimeSlot.DataAccess.Exceptions;

namespace MedTimeSlot.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            await ExceptionHandler(context, e);
        }
    }

    private Task ExceptionHandler(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var errorMessage = exception.Message;
        var statusCode = (int)HttpStatusCode.InternalServerError;

        switch (exception)
        {
            case NotFoundException notFoundException:
                errorMessage = $"Not found: {notFoundException.Message}";
                statusCode = (int)HttpStatusCode.NotFound;
                break;
            case TimeException timeException:
                errorMessage = $"The time is incorrect: {timeException.Message}";
                statusCode = (int)HttpStatusCode.BadRequest;
                break;
        }

        context.Response.StatusCode = statusCode;
        return context.Response.WriteAsJsonAsync(new { error = errorMessage });
    }
}