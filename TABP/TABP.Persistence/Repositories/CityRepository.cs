using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entites;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Models;
namespace TABP.Persistence.Repositories
{
    public class CityRepository(ApplicationDbContext context) : ICityRepository
    {
        public async Task<City> CreateCityAsync(City city, CancellationToken cancellationToken)
        {
            var createdCity = await context.Cities.AddAsync(city, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return createdCity.Entity;
        }
        public async Task<bool> DeleteCityAsync(int cityId, CancellationToken cancellationToken)
        {
            var city = await GetCityByIdAsync(cityId, cancellationToken);
            if (city is null)
                return false;
            context.Cities.Remove(city);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
        public async Task<IEnumerable<CityWithHotelCount>> GetAllCitiesAsync(CancellationToken cancellationToken)
        {
            var cities = await context.Cities.Select(c => new CityWithHotelCount
            (
              c.Id,
              c.Name,
              c.Country,
              c.PostOffice,
              c.CreatedAt,
              c.UpdatedAt,
              c.Hotels.Count()
            )).ToListAsync(cancellationToken);
            return cities;
        }
        public async Task<City?> GetCityByIdAsync(int cityId, CancellationToken cancellationToken)
        {
            return await context.Cities
                .Include(c => c.CityImages)
                .FirstOrDefaultAsync(c => c.Id == cityId, cancellationToken);
        }
        public async Task<City?> UpdateCityAsync(City city, CancellationToken cancellationToken)
        {
            var updatedCount = await context.Cities
                .Where(c => c.Id == city.Id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(c => c.Name, city.Name)
                    .SetProperty(c => c.Country, city.Country)
                    .SetProperty(c => c.PostOffice, city.PostOffice)
                    .SetProperty(c => c.UpdatedAt, DateTime.UtcNow)
                );
            if (updatedCount == 0)
                return null;
            return await GetCityByIdAsync(city.Id, cancellationToken);
        }
        public async Task<IEnumerable<City>> GetMostBookedCitiesAsync(int count, CancellationToken cancellationToken)
        {
            return new List<City>(); // Placeholder for actual implementation
        }
    }
}