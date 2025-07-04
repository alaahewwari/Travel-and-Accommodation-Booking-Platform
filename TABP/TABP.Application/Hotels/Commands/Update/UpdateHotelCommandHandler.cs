using MediatR;
using TABP.Application.Cities.Common;
using TABP.Application.Common;
using TABP.Application.Hotels.Common;
using TABP.Application.Hotels.Mapping;
using TABP.Application.Owners.Common;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Hotels.Commands.Update
{
    public class UpdateHotelCommandHandler(
        IHotelRepository HotelRepository,
        ICityRepository cityRepository,
        IOwnerRepository ownerRepository,
        HotelMapper mapper)
        : IRequestHandler<UpdateHotelCommand, Result<HotelResponse>>
    {
        public async Task<Result<HotelResponse>> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
        {
            var existingHotel = await HotelRepository.GetHotelByIdAsync(request.Id, cancellationToken);
            if (existingHotel is null)
            {
                return Result<HotelResponse>.Failure(HotelErrors.HotelNotFound);
            }
            var existingCity = await cityRepository.GetCityByIdAsync(request.CityId, cancellationToken);
            if (existingCity is null)
            {
                return Result<HotelResponse>.Failure(CityErrors.CityNotFound);
            }
            var existingOwner = await ownerRepository.GetOwnerByIdAsync(request.OwnerId, cancellationToken);
            if (existingOwner is null)
            {
                return Result<HotelResponse>.Failure(OwnerErrors.OwnerNotFound);
            }
            var hotelModel = mapper.ToHotelDomain(request);
            var updatedHotel = await HotelRepository.UpdateHotelAsync(hotelModel, cancellationToken);
            var hotel = mapper.ToHotelResponse(updatedHotel!);
            return Result<HotelResponse>.Success(hotel);
        }
    }
}