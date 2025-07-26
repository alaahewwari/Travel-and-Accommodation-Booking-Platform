using MediatR;
using TABP.Application.Bookings.Common;
using TABP.Application.Bookings.Mapper;
using TABP.Application.Common;
using TABP.Application.Users.Common.Errors;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Interfaces.Services;
using TABP.Domain.Models;
namespace TABP.Application.Bookings.Queries.GetById
{
    public class GetBookingsQueryHandler(
        IUserContext userContext,
        IUserRepository userRepository,
        IBookingRepository bookingRepository
        ) : IRequestHandler<GetBookingsQuery, Result<PagedResult<BookingResponse>>>
    {
        public async Task<Result<PagedResult<BookingResponse>>> Handle(GetBookingsQuery request, CancellationToken cancellationToken)
        {
            var existingUser = await userRepository.GetUserByIdAsync(userContext.UserId, cancellationToken);
            if (existingUser == null)
            {
                return Result<PagedResult<BookingResponse>>.Failure(UserErrors.UserNotFound);
            }
            var sieveModel = request.ToSieveModel();
            var bookings = await bookingRepository.GetBookingsAsync(existingUser.Id, sieveModel, cancellationToken);
            var response = bookings.ToBookingPaginationResult();
            return Result<PagedResult<BookingResponse>>.Success(response);
        }
    }
}