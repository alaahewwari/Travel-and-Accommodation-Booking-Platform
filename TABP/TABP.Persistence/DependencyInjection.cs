using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TABP.Domain.Interfaces.Repositories;
using TABP.Persistence.Interceptors;
using TABP.Persistence.Repositories;
namespace TABP.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                                optionsBuilder => optionsBuilder.EnableRetryOnFailure(3))
                                .AddInterceptors(new SoftDeleteInterceptor());
            });
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IRoomClassRepository, RoomClassRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IDiscountRespository, DiscountRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}