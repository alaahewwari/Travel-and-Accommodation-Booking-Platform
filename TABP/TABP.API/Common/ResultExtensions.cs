using Microsoft.AspNetCore.Mvc;
using TABP.Application.Common;
namespace TABP.API.Extensions
{
    public static class ResultExtensions
    {
        /// <summary>
        /// Converts a Result to an appropriate ActionResult with proper HTTP status codes
        /// </summary>
        public static ActionResult<T> ToActionResult<T>(this Result<T> result)
        {
            if (result.IsSuccess)
                return new OkObjectResult(result.Value);
            return CreateErrorResult<T>(result.Error);
        }
        /// <summary>
        /// Converts a Result to a CreatedAtAction result for POST operations
        /// </summary>
        public static ActionResult<T> ToCreatedResult<T>(this Result<T> result, string actionName, object routeValues)
        {
            if (result.IsSuccess)
                return new CreatedAtActionResult(actionName, null, routeValues, result.Value);
            return CreateErrorResult<T>(result.Error);
        }
        /// <summary>
        /// Converts a Result to a CreatedAtAction result with a custom location builder
        /// </summary>
        public static ActionResult<T> ToCreatedResult<T>(this Result<T> result, string actionName, Func<T, object> routeValueBuilder)
        {
            if (result.IsSuccess)
            {
                var routeValues = routeValueBuilder(result.Value);
                return new CreatedAtActionResult(actionName, null, routeValues, result.Value);
            }
            return CreateErrorResult<T>(result.Error);
        }
        /// <summary>
        /// Converts a Result to an ActionResult for operations that don't return data (like DELETE)
        /// </summary>
        public static ActionResult<object> ToActionResult(this Result result)
        {
            if (result.IsSuccess)
                return new NoContentResult();
            return CreateErrorResult<object>(result.Error);
        }
        private static ActionResult<T> CreateErrorResult<T>(Error error)
        {
            var errorResponse = CreateErrorResponse(error);
            var badRequestCodes = new[] { "InvalidData", "InvalidDates", "CancellationNotAllowed", "NotPending", "UpdateFailed", "PaymentProcessingFailed", "PaymentConfirmationFailed", "CreationFailed", "NotModified" };

            if (badRequestCodes.Any(code => error.Code.Contains(code)))
                return new BadRequestObjectResult(errorResponse);
            return error.Code switch
            {
                var code when code.Contains("NotFound") => new NotFoundObjectResult(errorResponse),
                var code when code.Contains("AlreadyExists") => new ConflictObjectResult(errorResponse),
                var code when code.Contains("Overlap") => new ConflictObjectResult(errorResponse),
                var code when code.Contains("UnauthorizedAccess") => new ForbidResult(),
                var code when code.Contains("UnexpectedError") => new ObjectResult(errorResponse)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                },
                _ => new BadRequestObjectResult(errorResponse)
            };
        }
        private static object CreateErrorResponse(Error error) => new
        {
            error.Code,
            error.Description
        };
    }
}