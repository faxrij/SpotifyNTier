using App.Domain.Entities;
using App.Logic.Interfaces;
using MediatR;

namespace App.Logic.Queries.GetAlbum.GetAllAlbums;

public class GetAllAlbumsQueryHandler(IAlbumRepository albumRepository) : IRequestHandler<GetAllAlbumsQuery, List<Album>>
{
    public async Task<List<Album>> Handle(GetAllAlbumsQuery request, CancellationToken cancellationToken)
    {
        return await albumRepository.GetAllAlbumsAsync();
    }
}