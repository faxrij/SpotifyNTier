using System.Net;
using App.Infrastructure.Models;
using Serilog;

namespace App.Infrastructure.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
 
    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
 
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (System.Exception exception)
        {
            Log.Error(exception, "Exception occurred: {Message}", exception.Message);
            var errorResponse = new ErrorResponse
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Title = "Server Error",
            };
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}