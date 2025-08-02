using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Models.City;
using TABP.Persistence.Context;
namespace TABP.Persistence.Repositories
{
    /// <summary>
    /// Entity Framework implementation of the city repository for city data access operations.
    /// Provides concrete implementation of city CRUD operations, management queries, and booking analytics using Entity Framework Core.
    /// </summary>
    /// <param name="context">The Entity Framework database context for city operations.</param>
    public class CityRepository(ApplicationDbContext context) : ICityRepository
    {
        /// <inheritdoc />
        public async Task<City> CreateCityAsync(City city, CancellationToken cancellationToken)
        {
            var createdCity = await context.Cities.AddAsync(city, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return createdCity.Entity;
        }
        /// <inheritdoc />
        public async Task<bool> DeleteCityAsync(int cityId, CancellationToken cancellationToken)
        {
            var city = await GetCityByIdAsync(cityId, cancellationToken);
            if (city is null)
                return false;
            context.Cities.Remove(city);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
        /// <inheritdoc />
        public async Task<IEnumerable<CityForManagement>> GetAllCitiesAsync(CancellationToken cancellationToken)
        {
            var cities = await context.Cities.Select(c => new CityForManagement
            (
              c.Id,
              c.Name,
              c.Country,
              c.PostOffice,
              c.CreatedAt,
              c.UpdatedAt,
              c.Hotels.Count()
            )).AsNoTracking()
            .ToListAsync(cancellationToken);
            return cities;
        }
        /// <inheritdoc />
        public async Task<City?> GetCityByIdAsync(int cityId, CancellationToken cancellationToken)
        {
            return await context.Cities
                .Include(c => c.CityImages)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == cityId, cancellationToken);
        }
        /// <inheritdoc />
        public async Task<City?> UpdateCityAsync(City city, CancellationToken cancellationToken)
        {
            var updatedCount = await context.Cities
                .Where(c => c.Id == city.Id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(c => c.Name, city.Name)
                    .SetProperty(c => c.Country, city.Country)
                    .SetProperty(c => c.PostOffice, city.PostOffice)
                    .SetProperty(c => c.UpdatedAt, DateTime.UtcNow), cancellationToken
                );
            if (updatedCount == 0)
                return null;
            return await GetCityByIdAsync(city.Id, cancellationToken);
        }
        /// <inheritdoc />
        public async Task<IEnumerable<City>> GetMostBookedCitiesAsync(int count, CancellationToken cancellationToken)
        {
            var cities = await context.Cities
                .Include(c => c.Hotels)
                .ThenInclude(r => r.Bookings)
                .AsNoTracking()
                .OrderByDescending(c => c.Hotels.Sum(r => r.Bookings.Count))
                .Take(count)
                .ToListAsync(cancellationToken);
            return cities;
        }
    }
}