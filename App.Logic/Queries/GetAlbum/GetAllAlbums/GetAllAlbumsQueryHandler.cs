using App.Domain.Entities;
using App.Logic.Interfaces;
using MediatR;

namespace App.Logic.Queries.GetAlbum.GetAllAlbums;

public class GetAllAlbumsQueryHandler : IRequestHandler<GetAllAlbumsQuery, List<Album>>
{
    private readonly IAlbumRepository _albumRepository;

    public GetAllAlbumsQueryHandler(IAlbumRepository albumRepository)
    {
        _albumRepository = albumRepository;
    }

    public async Task<List<Album>> Handle(GetAllAlbumsQuery request, CancellationToken cancellationToken)
    {
        return await _albumRepository.GetAllAlbumsAsync();
    }
}