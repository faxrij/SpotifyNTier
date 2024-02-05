using App.Infrastructure.Contexts;
using App.Logic.Interfaces;
using App.Logic.Repositories;
using App.Logic.Services;
using Microsoft.EntityFrameworkCore;

namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        builder.Services.AddDbContext<DataBaseContext>(options => 
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<ISingerRepository, SingerRepository>();
        builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
        builder.Services.AddScoped<ISongRepository, SongRepository>();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(conf => {
                conf.SwaggerEndpoint("/swagger/v1/swagger.json","Sample Api v1");
                conf.RoutePrefix = string.Empty;
            });
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}