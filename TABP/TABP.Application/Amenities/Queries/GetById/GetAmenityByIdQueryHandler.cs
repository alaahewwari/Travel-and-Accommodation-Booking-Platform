using MediatR;
using TABP.Application.Amenities.Common;
using TABP.Application.Amenities.Mapper;
using TABP.Application.Common;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Amenities.Queries.GetById
{
    public class GetAmenityByIdQueryHandler(
        IAmenityRepository amenityRepository) : IRequestHandler<GetAmenityByIdQuery, Result<AmenityResponse>>
    {
        public async Task<Result<AmenityResponse>> Handle(GetAmenityByIdQuery request, CancellationToken cancellationToken)
        {
            var amenity = await amenityRepository.GetAmenityByIdAsync(request.Id, cancellationToken);
            if (amenity is null)
                return Result<AmenityResponse>.Failure(AmenityErrors.AmenityNotFound);
            var response = amenity.ToAmenityResponse();
            return Result<AmenityResponse>.Success(response);
        }
    }
}