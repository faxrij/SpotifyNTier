using App.Logic.Interfaces;
using MediatR;

namespace App.Logic.Commands.DeleteAlbum;

public class DeleteAlbumCommandHandler : IRequestHandler<DeleteAlbumCommand, Boolean>
{
    private readonly IAlbumRepository _albumRepository;

    public DeleteAlbumCommandHandler(IAlbumRepository albumRepository)
    {
        _albumRepository = albumRepository;
    }

    public async Task<Boolean> Handle(DeleteAlbumCommand request, CancellationToken cancellationToken)
    {
        var album = await _albumRepository.RemoveAlbumAsync(request.Id);
        return album;    
    }
}