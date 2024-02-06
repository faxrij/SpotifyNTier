using App.Domain.Entities;
using App.Logic.Interfaces;
using FluentValidation;
using MediatR;

namespace App.Logic.Commands.AddAlbum;

public class AddAlbumCommandHandler(IAlbumRepository albumRepository, IValidator<AddAlbumCommand> validator) : IRequestHandler<AddAlbumCommand, Album>
{
    public async Task<Album> Handle(AddAlbumCommand request, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);
        var album = await albumRepository.CreateAlbumAsync(request);
        return album;
    }
}