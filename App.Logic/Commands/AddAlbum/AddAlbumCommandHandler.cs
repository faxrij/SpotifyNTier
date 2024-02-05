using App.Domain.Entities;
using App.Logic.Interfaces;
using MediatR;

namespace App.Logic.Commands.AddAlbum;

public class AddAlbumCommandHandler : IRequestHandler<AddAlbumCommand, Album>
{
    private readonly IAlbumRepository _albumRepository;

    public AddAlbumCommandHandler(IAlbumRepository albumRepository)
    {
        _albumRepository = albumRepository;
    }

    public async Task<Album> Handle(AddAlbumCommand request, CancellationToken cancellationToken)
    {
        var album = await _albumRepository.CreateAlbumAsync(request);
        return album;
    }
}