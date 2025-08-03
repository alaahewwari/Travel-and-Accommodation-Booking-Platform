using Serilog;
using System.Net;
using System.Text.Json;

namespace TABP.API.Middlewares
{
    /// <summary>
    /// Middleware for handling unhandled exceptions throughout the API request pipeline.
    /// Provides centralized exception logging and standardized error responses for improved debugging and user experience.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the ExceptionHandlingMiddleware.
        /// </summary>
        /// <param name="next">The next middleware delegate in the request pipeline.</param>
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Processes HTTP requests and handles any unhandled exceptions that occur in the pipeline.
        /// Logs detailed exception information with contextual data and returns a standardized error response to the client.
        /// </summary>
        /// <param name="context">The HTTP context containing request and response information.</param>
        /// <returns>A task representing the asynchronous operation of processing the request or handling exceptions.</returns>
        /// <remarks>
        /// When an exception occurs, this method:
        /// - Generates a unique error ID for tracking purposes
        /// - Logs comprehensive exception details including request path, method, and trace ID
        /// - Returns a standardized JSON error response with HTTP 500 status
        /// - Includes the error ID and trace ID for correlation with logs
        /// </remarks>
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