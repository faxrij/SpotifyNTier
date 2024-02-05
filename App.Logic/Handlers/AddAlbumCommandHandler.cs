using App.Domain.Entities;
using App.Logic.Commands;
using App.Logic.Interfaces;
using MediatR;

namespace App.Logic.Handlers;

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