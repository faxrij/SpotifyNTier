using App.Domain.Entities;
using App.Logic.Interfaces;
using MediatR;

namespace App.Logic.Queries.GetAlbum.GetAlbumById;

public sealed record GetAlbumByIdQuery(int Id) : ICachedQuery<Album>
{
    public string CacheKey => $"albums-by-id-{Id}";
    public TimeSpan? Expiration => null;
}