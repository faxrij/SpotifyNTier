using App.Domain.Entities;
using App.Infrastructure.Behaviors;
using App.Infrastructure.Contexts;
using App.Infrastructure.Repositories;
using App.Logic.Interfaces;
using App.Logic.Queries.GetAlbum.GetAlbumById;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace App.Infrastructure
{
    public static class InfrastructureInjection
    {
        public static void AddInfrastructureServices(this IServiceCollection services, string connectionString, string elasticSearchString, 
            Boolean autoRegisterTemplate, string indexFormat)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{CorrelationId}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticSearchString))
                {
                    AutoRegisterTemplate = autoRegisterTemplate,
                    IndexFormat = indexFormat
                })
                .CreateLogger();

            services.AddDbContext<DataBaseContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddMemoryCache();
            services.AddDistributedMemoryCache();
            services.AddSingleton<ICache, CacheService>();
            services.AddScoped(typeof(ICachePolicy<,>), typeof(CachePolicy<,>));
            
            // Register internal repositories
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISingerRepository, SingerRepository>();
            services.AddScoped<IAlbumRepository, AlbumRepository>();
            services.AddScoped<ISongRepository, SongRepository>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
            
            // Add Authorization
            services.AddAuthorization();
        }
    }
}