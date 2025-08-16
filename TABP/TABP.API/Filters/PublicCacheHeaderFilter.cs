using Microsoft.AspNetCore.Mvc.Filters;
namespace TABP.API.Filters
{
    /// <summary>
    /// An <see cref="IAsyncResultFilter"/> that automatically adds caching headers to successful GET responses.
    /// </summary>
    /// <remarks>
    /// - Adds a <c>Cache-Control</c> header with <c>public, max-age={seconds}</c> if not already set.  
    /// - Adds a <c>Vary: Accept-Encoding</c> header if not already present, ensuring caches store separate versions
    ///   of the response for different <c>Accept-Encoding</c> values (e.g., <c>gzip</c>, <c>br</c>, uncompressed).
    /// This helps improve performance with proper client/CDN caching while preventing incorrect content encoding delivery.
    /// </remarks>
    public sealed class PublicCacheHeaderFilter(int seconds) : IAsyncResultFilter
    {
        /// <summary>
        /// Called asynchronously before the action result executes.  
        /// If the request is a GET and the response status code is 2xx, it sets caching headers unless already present.
        /// </summary>
        /// <param name="context">The result execution context.</param>
        /// <param name="next">The delegate to execute the next action filter or the action result.</param>
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var req = context.HttpContext.Request;
            var res = context.HttpContext.Response;
            if (HttpMethods.IsGet(req.Method) && res.StatusCode is >= 200 and < 300)
            {
                if (!res.Headers.ContainsKey("Cache-Control"))
                    res.Headers["Cache-Control"] = $"public, max-age={seconds}"; //"public" → means any cache (browser, CDN, proxy) is allowed to store the response.
                if (!res.Headers.ContainsKey("Vary"))
                    res.Headers["Vary"] = "Accept-Encoding"; //"Vary" → indicates that the response may vary based on the "Accept-Encoding" request header, store separate versions for each encoding.”
            }
            await next();
        }
    }
}