using App.Infrastructure.Contexts;
using App.Infrastructure.Repositories;
using App.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure;

public static class Program
{
    public static void ConfigureServices(WebApplicationBuilder builder, IServiceCollection services, string connectionString)
    { 
        builder.Services.AddDbContext<DataBaseContext>(options =>
            options.UseNpgsql(connectionString));
        
        // Register internal repositories
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ISingerRepository, SingerRepository>();
        services.AddScoped<IAlbumRepository, AlbumRepository>();
        services.AddScoped<ISongRepository, SongRepository>();

        // Register RepositoryProvider
        services.AddScoped<IRepositoryProvider, RepositoryProvider>();
        
        // Add Authorization
        services.AddAuthorization();
    }
}