using App.Domain.Entities;
using App.Logic.Interfaces;

namespace App.Logic.Queries.GetAlbum.GetAllAlbums;

public class GetAllAlbumsQuery : ICachedQuery<List<Album>>
{
    public string CacheKey => $"albums-all";
    public TimeSpan? Expiration => null;
}