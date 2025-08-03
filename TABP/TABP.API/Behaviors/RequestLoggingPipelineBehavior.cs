using MediatR;
using Serilog.Context;
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
            CancellationToken cancellationToken)
        {
            string requestName = typeof(TRequest).Name;
            _logger.LogInformation(
                "Processing request {RequestName}", requestName);
            TResponse result = await next();
            if (result.IsSuccess)
            {
                using (LogContext.PushProperty("Info", result.IsSuccess, true))
                {
                    _logger.LogInformation(
                    "Completed request {RequestName}", requestName);
                }
            }
            else
            {
                using (LogContext.PushProperty("Error", result.Error, true))
                {
                    _logger.LogError(
                        "Completed request {RequestName} with error", requestName);
                }
            }
            return result;
        }
    }
}