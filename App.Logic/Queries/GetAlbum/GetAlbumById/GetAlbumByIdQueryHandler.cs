using App.Domain.Entities;
using App.Logic.Interfaces;
using MediatR;

namespace App.Logic.Queries.GetAlbum.GetAlbumById;

public class GetAlbumByIdQueryHandler(IAlbumRepository albumRepository) : IRequestHandler<GetAlbumByIdQuery, Album>
{
    public async Task<Album> Handle(GetAlbumByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.Id == null)
        {
            throw new InvalidOperationException("Error when Searching Null id");
        }
        return await albumRepository.GetAlbumByIdAsync(request.Id.Value);
    }
}