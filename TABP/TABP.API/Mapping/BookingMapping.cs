using Riok.Mapperly.Abstractions;
using TABP.API.Contracts.Bookings;
using TABP.Application.Bookings.Commands.Create;
using TABP.Application.Bookings.Queries.GetById;
namespace TABP.API.Mapping
{
    [Mapper]
    public static partial class BookingMapping
    {
        public static partial CreateBookingCommand ToCommand(this CreateBookingRequest request);
        public static partial GetBookingsQuery ToQuery(this GetBookingsRequest request);
    }
}