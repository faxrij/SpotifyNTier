using App.Domain.Entities;
using App.Logic.Interfaces;
using MediatR;

namespace App.Logic.Commands.UpdateAlbum;

public class UpdateAlbumCommandHandler : IRequestHandler<UpdateAlbumCommand, Album>
{
    private readonly IAlbumRepository _albumRepository;

    public UpdateAlbumCommandHandler(IAlbumRepository albumRepository)
    {
        _albumRepository = albumRepository;
    }

    public async Task<Album> Handle(UpdateAlbumCommand request, CancellationToken cancellationToken)
    {
        int id = request.Id;
        var album = await _albumRepository.UpdateAlbumAsync(request, id);
        return album;    
    }
}