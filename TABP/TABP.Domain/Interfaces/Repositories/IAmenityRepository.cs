using TABP.Domain.Entities;
namespace TABP.Domain.Interfaces.Repositories
{
    public interface IAmenityRepository
    {
        Task<Amenity?> GetAmenityByIdAsync(long id, CancellationToken cancellationToken);
        Task<Amenity?> GetAmenityByNameAsync(string name, CancellationToken cancellationToken);
        Task<IEnumerable<Amenity>> GetAllAmenitiesAsync (CancellationToken cancellationToken);
        Task<Amenity?> CreateAmenityAsync(Amenity amenity, CancellationToken cancellationToken);
        Task<Amenity?> UpdateAmenityAsync(Amenity amenity, CancellationToken cancellationToken);
        Task AssignAmenityToRoomClassAsync(Amenity amenity, RoomClass roomClass, CancellationToken cancellationToken);
    }
}