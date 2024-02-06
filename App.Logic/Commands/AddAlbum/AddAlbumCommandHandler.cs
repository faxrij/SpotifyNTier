using App.Domain.Entities;
using App.Logic.Interfaces;
using MediatR;

namespace App.Logic.Commands.AddAlbum;

public class AddAlbumCommandHandler(IAlbumRepository albumRepository) : IRequestHandler<AddAlbumCommand, Album>
{
    public async Task<Album> Handle(AddAlbumCommand request, CancellationToken cancellationToken)
    {
        var album = await albumRepository.CreateAlbumAsync(request);
        return album;
    }
}