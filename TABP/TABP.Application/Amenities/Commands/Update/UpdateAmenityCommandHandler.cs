using MediatR;
using TABP.Application.Amenities.Common;
using TABP.Application.Amenities.Mapper;
using TABP.Application.Common;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Amenities.Commands.Update
{
    public class UpdateAmenityCommandHandler(
    IAmenityRepository repository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateAmenityCommand, Result<AmenityResponse>>
    {
        public async Task<Result<AmenityResponse>> Handle(UpdateAmenityCommand request, CancellationToken cancellationToken)
        {
            var existingAmenity = await repository.GetAmenityByIdAsync(request.Id, cancellationToken);
            if (existingAmenity is null)
                return Result<AmenityResponse>.Failure(AmenityErrors.AmenityNotFound);
            var amenity = request.ToAmenityDomain();
            var updatedAmenity = await repository.UpdateAmenityAsync(amenity, cancellationToken);
            if(updatedAmenity is null)
                return Result<AmenityResponse>.Failure(AmenityErrors.AmenityUpdateFailed);
            var response = updatedAmenity.ToAmenityResponse();
            return Result<AmenityResponse>.Success(response);
        }
    }
}
