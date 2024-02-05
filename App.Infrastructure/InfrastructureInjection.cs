using App.Infrastructure.Contexts;
using App.Infrastructure.Repositories;
using App.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure;

internal static class InfrastructureInjection
{
    public static void ConfigureServices(IServiceCollection services, string connectionString)
    { 
        services.AddDbContext<DataBaseContext>(options =>
            options.UseNpgsql(connectionString));
        
        // Register internal repositories
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ISingerRepository, SingerRepository>();
        services.AddScoped<IAlbumRepository, AlbumRepository>();
        services.AddScoped<ISongRepository, SongRepository>();

        // Add Authorization
        services.AddAuthorization();
    }
}