using TABP.Domain.Entities;
using TABP.Domain.Models;
namespace TABP.Domain.Interfaces.Repositories
{
    public interface ICityRepository
    {
        Task<City> CreateCityAsync(City city, CancellationToken cancellationToken);
        Task<City?> GetCityByIdAsync(int cityId, CancellationToken cancellationToken);
        Task<IEnumerable<CityForManagement>> GetAllCitiesAsync(CancellationToken cancellationToken);
        Task<City?> UpdateCityAsync(City city, CancellationToken cancellationToken);
        Task<bool> DeleteCityAsync(int cityId, CancellationToken cancellationToken);
        Task<IEnumerable<City>> GetMostBookedCitiesAsync(int count, CancellationToken cancellationToken);
    }
}