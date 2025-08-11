using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
namespace TABP.API.Middlewares
{
    /// <summary>
    /// Global exception middleware for the HTTP API.
    /// - Runs early in the pipeline to catch unhandled exceptions.
    /// - Logs details via <see cref="ILogger{TCategoryName}"/> with a scoped ErrorId/TraceId/Path/Method.
    /// - Produces a standardized RFC 7807 <see cref="ProblemDetails"/> JSON response.
    /// - Keeps logging framework-agnostic (Serilog/NLog/etc. can be plugged in via the host's logging provider).
    /// </summary>
    public sealed class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandlingMiddleware"/>.
        /// </summary>
        /// <param name="next">The next middleware in the request pipeline.</param>
        /// <param name="logger">The logger used to record exception details.</param>
        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        /// <summary>
        /// Processes the HTTP request and handles any unhandled exceptions by:
        /// - Generating a unique ErrorId for correlation.
        /// - Logging exception details with Path, Method, and TraceId in a logging scope.
        /// - Returning a ProblemDetails JSON payload (application/problem+json) with HTTP 500.
        /// </summary>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (OperationCanceledException) when (context.RequestAborted.IsCancellationRequested)
            {
                // Request was aborted by the client; don't overwrite response.
                _logger.LogWarning("Request aborted by client: {Method} {Path}", context.Request.Method, context.Request.Path);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString("n");

                using (_logger.BeginScope(new Dictionary<string, object?>
                {
                    ["ErrorId"] = errorId,
                    ["TraceId"] = context.TraceIdentifier,
                    ["Path"] = context.Request.Path.Value,
                    ["Method"] = context.Request.Method
                }))
                {
                    _logger.LogError(ex, "Unhandled exception");
                }

                if (!context.Response.HasStarted)
                {
                    context.Response.Clear();
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/problem+json";

                    var problem = new ProblemDetails
                    {
                        Title = "An unexpected error occurred.",
                        Status = StatusCodes.Status500InternalServerError,
                        Detail = "Contact support and provide the identifiers for correlation.",
                        Instance = context.Request.Path
                    };
                    problem.Extensions["errorId"] = errorId;
                    problem.Extensions["traceId"] = context.TraceIdentifier;

                    // Use the same JSON settings as the app (optional)
                    var json = JsonSerializer.Serialize(problem, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        WriteIndented = false
                    });
                    await context.Response.WriteAsync(json);
                }
            }
        }
    }
}