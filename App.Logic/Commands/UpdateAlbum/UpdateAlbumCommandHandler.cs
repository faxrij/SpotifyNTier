using App.Domain.Entities;
using App.Logic.Interfaces;
using MediatR;

namespace App.Logic.Commands.UpdateAlbum;

public class UpdateAlbumCommandHandler(IAlbumRepository albumRepository) : IRequestHandler<UpdateAlbumCommand, Album>
{
    public async Task<Album> Handle(UpdateAlbumCommand request, CancellationToken cancellationToken)
    {
        int id = request.Id;
        var album = await albumRepository.UpdateAlbumAsync(request, id);
        return album;    
    }
}