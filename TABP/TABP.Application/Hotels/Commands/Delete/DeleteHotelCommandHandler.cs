using MediatR;
using TABP.Application.Common;
using TABP.Application.Hotels.Common;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Hotels.Commands.Delete
{
    public class DeleteHotelCommandHandler(IHotelRepository hotelRepository) : IRequestHandler<DeleteHotelCommand, Result>
    {
        public async Task<Result> Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
        {
            var hotel = await hotelRepository.GetHotelByIdAsync(request.Id, cancellationToken);
            if (hotel is null)
                return Result.Failure(HotelErrors.HotelNotFound);
            await hotelRepository.DeleteHotelAsync(hotel.Id, cancellationToken);
            return Result.Success();
        }
    }
}