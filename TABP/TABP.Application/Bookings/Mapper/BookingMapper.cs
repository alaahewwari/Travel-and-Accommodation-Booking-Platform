using Riok.Mapperly.Abstractions;
using Sieve.Models;
using TABP.Application.Bookings.Commands.Create;
using TABP.Application.Bookings.Common;
using TABP.Application.Bookings.Queries.GetById;
using TABP.Domain.Entities;
using TABP.Domain.Models.Common;
namespace TABP.Application.Bookings.Mapper
{
    [Mapper]
    public static partial class BookingMapper
    {
        [MapperIgnoreTarget(nameof(BookingResponse.HotelName))]
        private static partial BookingCreationResponse ToBookingResponseInternal(Booking booking);
        public static BookingCreationResponse ToBookingCreationResponse(this Booking booking, Hotel hotel, IEnumerable<string> RoomNumbers)
        {
            var bookingResponse = ToBookingResponseInternal(booking);
            bookingResponse.HotelName = hotel.Name;
            bookingResponse.RoomNumbers = RoomNumbers;
            return bookingResponse;
        }
        public static partial BookingResponse ToBookingResponse(this Booking booking);
        public static partial SieveModel ToSieveModel(this GetBookingsQuery query);
        public static partial PagedResult<BookingResponse> ToBookingPaginationResult(this PagedResult<Booking> pagedResult);
    }
}