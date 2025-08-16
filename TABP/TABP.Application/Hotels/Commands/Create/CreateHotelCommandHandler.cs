using MediatR;
using TABP.Application.Cities.Common;
using TABP.Application.Common;
using TABP.Application.Hotels.Common;
using TABP.Application.Hotels.Mapper;
using TABP.Application.Owners.Common;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Hotels.Commands.Create
{
    public class CreateHotelCommandHandler(
        IHotelRepository hotelRepository,
        ICityRepository cityRepository,
        IOwnerRepository ownerRepository,
        IReviewRepository reviewRepository
        ) : IRequestHandler<CreateHotelCommand, Result<HotelResponse>>
    {
        public async Task<Result<HotelResponse>> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
        {
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
            var existingHotel = await hotelRepository.GetHotelByLocationAsync(request.LocationLongitude,request.LocationLatitude, cancellationToken);
            if (existingHotel)
            {
                return Result<HotelResponse>.Failure(HotelErrors.HotelAlreadyExists);
            }
            var hotelModel = request.ToHotelDomain();
            var hotel = await hotelRepository.CreateHotelAsync(hotelModel, cancellationToken);
            var response = hotel.ToHotelResponse();
            return Result<HotelResponse>.Success(response);
        }
    }
}