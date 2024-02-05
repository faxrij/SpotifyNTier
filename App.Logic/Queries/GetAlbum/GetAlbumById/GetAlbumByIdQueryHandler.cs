using App.Domain.Entities;
using App.Logic.Interfaces;
using MediatR;

namespace App.Logic.Queries.GetAlbum.GetAlbumById;

public class GetAlbumByIdQueryHandler : IRequestHandler<GetAlbumByIdQuery, Album>
{
    private readonly IAlbumRepository _albumRepository;

    public GetAlbumByIdQueryHandler(IAlbumRepository albumRepository)
    {
        _albumRepository = albumRepository;
    }
    
    public async Task<Album> Handle(GetAlbumByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.Id == null)
        {
            throw new InvalidOperationException("Error when Searching Null id");
        }
        return await _albumRepository.GetAlbumByIdAsync(request.Id.Value);
    }
}