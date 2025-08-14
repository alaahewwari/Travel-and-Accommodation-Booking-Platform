using MediatR;
using TABP.Application.Amenities.Common;
using TABP.Application.Amenities.Mapper;
using TABP.Application.Common;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Amenities.Queries.GetAll
{
    public class GetAllAmenitiesQueryHandler(
    IAmenityRepository repository) : IRequestHandler<GetAllAmenitiesQuery, Result<IEnumerable<AmenityResponse>>>
    {
        public async Task<Result<IEnumerable<AmenityResponse>>> Handle(GetAllAmenitiesQuery request, CancellationToken cancellationToken)
        {
            var amenities = await repository.GetAllAmenitiesAsync(cancellationToken);
            var responses = amenities
                .Select(a => a.ToAmenityResponse());
            return Result<IEnumerable<AmenityResponse>>.Success(responses);
        }
    }
}