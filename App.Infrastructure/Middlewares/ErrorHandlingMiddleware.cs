using System.Net;
using App.Infrastructure.Models;
using Serilog;

namespace App.Infrastructure.Middlewares;

public class ErrorHandlingMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
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