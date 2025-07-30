using MediatR;
using TABP.Application.Amenities.Common;
using TABP.Application.Amenities.Mapper;
using TABP.Application.Common;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Amenities.Commands.Create
{
    public class CreateAmenityCommandHandler(
    IAmenityRepository repository
    ) : IRequestHandler<CreateAmenityCommand, Result<AmenityResponse>>
    {
        public async Task<Result<AmenityResponse>> Handle(CreateAmenityCommand request, CancellationToken cancellationToken)
        {
            var existingAmenity = await repository.GetAmenityByNameAsync(request.Name, cancellationToken);
            if (existingAmenity != null)
            {
                return Result<AmenityResponse>.Failure(AmenityErrors.AmenityAlreadyExists);
            }
            var createdAmenity = await repository.CreateAmenityAsync(existingAmenity, cancellationToken);
            var response = createdAmenity.ToAmenityResponse();
            return Result<AmenityResponse>.Success(response);
        }
    }
}