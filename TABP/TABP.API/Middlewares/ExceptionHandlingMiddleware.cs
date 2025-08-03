using Serilog;
using System.Net;
using System.Text.Json;
namespace TABP.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                var path = context.Request.Path;
                Log.ForContext("ErrorId", errorId)
                    .ForContext("Path", context.Request.Path)
                    .ForContext("Method", context.Request.Method)
                    .ForContext("TraceId", context.TraceIdentifier)
                    .Error("Unhandled exception [{ExceptionType}] on {Method} {Path}: {ErrorMessage}",
                    ex.GetType().Name, context.Request.Method, context.Request.Path, ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var errorResponse = new
                {
                    errorId,
                    message = "An unexpected error occurred.",
                    traceId = context.TraceIdentifier
                };
                var json = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(json);
            }
        }
    }
}