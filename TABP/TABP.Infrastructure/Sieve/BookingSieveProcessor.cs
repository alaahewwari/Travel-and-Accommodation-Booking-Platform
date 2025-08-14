using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;
using TABP.Domain.Entities;
namespace TABP.Infrastructure.Sieve
{
    public class BookingSieveProcessor : SieveProcessor
    {
        public BookingSieveProcessor(IOptions<SieveOptions> options) : base(options) { }
        protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
        {
            mapper.Property<Booking>(p => p.Status)
                  .CanFilter()
                  .CanSort();
            mapper.Property<Booking>(p => p.CheckInDate)
                  .CanFilter()
                  .CanSort();
            mapper.Property<Booking>(p => p.CheckOutDate)
                  .CanFilter()
                  .CanSort();
            mapper.Property<Booking>(h => h.CreatedAt)
                  .CanFilter()
                  .CanSort();
            return mapper;
        }
    }
}