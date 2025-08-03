using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;
using TABP.Domain.Entities;
namespace TABP.Infrastructure.Sieve
{
    public class HotelSieveProcessor : SieveProcessor
    {
        public HotelSieveProcessor(IOptions<SieveOptions> options) : base(options) { }
        protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
        {
            mapper.Property<Hotel>(p => p.Name)
                  .CanFilter()
                  .CanSort();
            return mapper;
        }
    }
}
