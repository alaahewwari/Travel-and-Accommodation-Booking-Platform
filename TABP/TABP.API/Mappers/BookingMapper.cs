using Riok.Mapperly.Abstractions;
using TABP.API.Contracts.Bookings;
using TABP.Application.Bookings.Commands.Create;
using TABP.Application.Bookings.Commands.Update;
using TABP.Application.Bookings.Common;
using TABP.Application.Bookings.Queries.GetById;
namespace TABP.API.Mappers
{
    [Mapper]
    public static partial class BookingMapper
    {
        public static partial CreateBookingCommand ToCommand(this CreateBookingRequest request);
        public static partial GetBookingsQuery ToQuery(this GetBookingsRequest request);
        public static partial UpdateBookingCommand ToCommand(this UpdateBookingRequest booking, long Id);
        public static partial UpdateBookingRequest ToUpdateBookingRequest(this BookingResponse response);
    }
}