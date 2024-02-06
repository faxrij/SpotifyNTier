using App.Infrastructure.Contexts;
using App.Infrastructure.Repositories;
using App.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace App.Infrastructure
{
    public static class InfrastructureInjection
    {
        public static void AddInfrastructureServices(this IServiceCollection services, string connectionString,
            string elasticSearchString,
            Boolean autoRegisterTemplate, string indexFormat)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticSearchString))
                {
                    AutoRegisterTemplate = autoRegisterTemplate,
                    IndexFormat = indexFormat
                })
                .CreateLogger();

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
}