using System.Net;
using Newtonsoft.Json;

namespace App.Infrastructure.Middlewares;

public class ErrorHandlingMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var errorResponse = new
        {
            message = "An error occurred while processing your request.",
            exception = exception.Message
        };

        var json = JsonConvert.SerializeObject(errorResponse);
        return context.Response.WriteAsync(json);
    }
}
