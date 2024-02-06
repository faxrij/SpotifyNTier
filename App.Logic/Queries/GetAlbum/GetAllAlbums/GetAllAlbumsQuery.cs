using App.Domain.Entities;
using MediatR;

namespace App.Logic.Queries.GetAlbum.GetAllAlbums;

public class GetAllAlbumsQuery : IRequest<List<Album>>
{
    public string CacheKey => $"albums-all";
    public TimeSpan? Expiration => null;
}