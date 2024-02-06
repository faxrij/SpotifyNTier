using App.Logic.Commands.AddAlbum;
using App.Logic.Commands.AddCategory;
using App.Logic.Commands.AddSinger;
using App.Logic.Commands.AddSong;
using FluentValidation;
using MediatR;

namespace App.Logic;

public static class LogicInjection
{
    public static void AddLogicServices(this IServiceCollection services)
    { 
        services.AddMediatR(typeof(LogicInjection).Assembly);
        services.AddScoped<IValidator<AddAlbumCommand>, AddAlbumCommandValidator>();
        services.AddScoped<IValidator<AddSongCommand>, AddSongCommandValidator>();
        services.AddScoped<IValidator<AddCategoryCommand>, AddCategoryCommandValidator>();
        services.AddScoped<IValidator<AddSingerCommand>, AddSingerCommandValidator>();

    }
}