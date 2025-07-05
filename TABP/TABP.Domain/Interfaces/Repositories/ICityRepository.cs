using TABP.Domain.Entites;
namespace TABP.Domain.Interfaces.Repositories
{
    public interface ICityRepository
    {
        Task<City> CreateCityAsync(City city, CancellationToken cancellationToken);
        Task<City?> GetCityByIdAsync(int cityId, CancellationToken cancellationToken);
        Task<IEnumerable<City>> GetAllCitiesAsync(CancellationToken cancellationToken);
        Task<City?> UpdateCityAsync(City city, CancellationToken cancellationToken);
        Task<bool> DeleteCityAsync(int cityId, CancellationToken cancellationToken);
        Task<IEnumerable<City>> GetMostBookedCitiesAsync(int count, CancellationToken cancellationToken);
    }
}