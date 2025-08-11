using MediatR;
using Microsoft.Extensions.Logging;
using TABP.Application.Common;
namespace TABP.API.Behaviors
{
    internal sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : Result
    {
        private readonly ILogger _logger;
        public RequestLoggingPipelineBehavior(ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }
        public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken ct)
        {
            var requestName = typeof(TRequest).Name;

            using (_logger.BeginScope(new Dictionary<string, object?>
            {
                ["RequestName"] = requestName
            }))
            {
                _logger.LogInformation("Handling {RequestName}", requestName);
                try
                {
                    var result = await next();

                    if (result.IsSuccess)
                        _logger.LogInformation("Handled {RequestName}", requestName);
                    else
                        _logger.LogWarning("Handled {RequestName} with error {Error}", requestName, result.Error);

                    return result;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unhandled exception in {RequestName}", requestName);
                    throw;
                }
            }
        }
    }
}