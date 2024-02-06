using App.Logic.Commands.AddAlbum;
using App.Logic.Commands.AddCategory;
using App.Logic.Commands.AddSinger;
using App.Logic.Commands.AddSong;
using App.Logic.Validators;
using FluentValidation;
using MediatR;

namespace App.Logic;

public static class LogicInjection
{
    public static void AddLogicServices(this IServiceCollection services)
    { 
        services.AddMediatR(typeof(LogicInjection).Assembly);
        services.AddScoped<IValidator<AddAlbumCommand>, AlbumValidator>();
        services.AddScoped<IValidator<AddSongCommand>, SongValidator>();
        services.AddScoped<IValidator<AddCategoryCommand>, CategoryValidator>();
        services.AddScoped<IValidator<AddSingerCommand>, SingerValidator>();

    }
}