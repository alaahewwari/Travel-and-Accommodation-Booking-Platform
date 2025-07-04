using MediatR;
using TABP.Application.Cities.Common;
using TABP.Application.Common;
using TABP.Application.Hotels.Common;
using TABP.Application.Hotels.Mapping;
using TABP.Application.Owners.Common;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Hotels.Commands.Create
{
    public class CreateHotelCommandHandler(
        IHotelRepository hotelRepository,
        ICityRepository cityRepository,
        IOwnerRepository ownerRepository,
        HotelMapper mapper) : IRequestHandler<CreateHotelCommand, Result<long>>
    {
        public async Task<Result<long>> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
        {
            var existingCity = await cityRepository.GetCityByIdAsync(request.CityId, cancellationToken);
            if (existingCity is null)
            { 
                return Result<long>.Failure(CityErrors.CityNotFound);
            }
            var existingOwner = await ownerRepository.GetOwnerByIdAsync(request.OwnerId, cancellationToken);
            if (existingOwner is not null)
            {
                return Result<long>.Failure(OwnerErrors.OwnerNotFound);
            }
            var existingHotel = await hotelRepository.GetHotelByLocationAsync(request.LocationLongitude,request.LocationLatitude, cancellationToken);
            if (!existingHotel)
            {
                return Result<long>.Failure(HotelErrors.HotelAlreadyExists);
            }
            var hotelModel = mapper.ToHotelDomain(request);
            var hotel = await hotelRepository.CreateHotelAsync(hotelModel, cancellationToken);
            return Result<long>.Success(hotel.Id);
        }
    }
}