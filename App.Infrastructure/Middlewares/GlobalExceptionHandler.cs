// using App.Infrastructure.Models;
// using Microsoft.AspNetCore.Diagnostics;
// using Newtonsoft.Json;
// using Serilog;
//
// namespace App.Infrastructure.Middlewares;
//
// public class GlobalExceptionHandler : IMiddleware
// {
//     public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
//     {
//         Log.Error(exception, "Exception occured: {Message}", exception.Message);
//         var errorResponse = new ErrorResponse
//         {
//             Status = StatusCodes.Status500InternalServerError,
//             Title = "Server Error",
//             Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"
//         };
//         httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
//         await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken: cancellationToken);
//
//         return true;
//     }
//
//     public async Task InvokeAsync(HttpContext context, RequestDelegate next)
//     {
//         try
//         {
//             await next(context);
//         }
//         catch (System.Exception ex)
//         {
//             Log.Error(ex, "An unhandled exception occurred: {ErrorMessage}", ex.Message);
//
//             // Create the error response
//             var errorResponse = new ErrorResponse
//             {
//                 Status = StatusCodes.Status500InternalServerError,
//                 Title = "Server Error",
//                 Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"
//             };
//
//             // Set the response status code
//             context.Response.StatusCode = StatusCodes.Status500InternalServerError;
//
//             // Serialize the error response to JSON
//             var json = JsonConvert.SerializeObject(errorResponse);
//
//             // Write the JSON response to the HTTP response body
//             context.Response.ContentType = "application/json";
//             await context.Response.WriteAsync(json);
//         }
//     }
// }