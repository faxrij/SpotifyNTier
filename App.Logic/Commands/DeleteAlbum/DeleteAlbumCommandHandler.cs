using App.Logic.Interfaces;
using MediatR;

namespace App.Logic.Commands.DeleteAlbum;

public class DeleteAlbumCommandHandler(IAlbumRepository albumRepository) : IRequestHandler<DeleteAlbumCommand, Boolean>
{
    public async Task<Boolean> Handle(DeleteAlbumCommand request, CancellationToken cancellationToken)
    {
        var result = await albumRepository.RemoveAlbumAsync(request.Id);
        return result;    
    }
}