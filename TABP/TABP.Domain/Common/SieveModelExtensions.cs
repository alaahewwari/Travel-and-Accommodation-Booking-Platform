using Sieve.Models;
namespace TABP.Domain.Common
{
    public static class SieveModelExtensions
    {
        private const int DefaultPage = 1;
        private const int DefaultPageSize = 10;
        public static void EnsureDefaults(this SieveModel model)
        {
            model.Page ??= DefaultPage;
            model.PageSize ??= DefaultPageSize;
        }
    }
}