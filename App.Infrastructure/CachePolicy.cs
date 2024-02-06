using App.Domain.Entities;
using App.Logic.Queries.GetAlbum.GetAlbumById;

namespace App.Infrastructure;

public class CachePolicy : ICachePolicy<GetAlbumByIdQuery, Album>
{
    // Optionally, change defaults
    public TimeSpan? AbsoluteExpirationRelativeToNow => TimeSpan.FromMinutes(10);
    public TimeSpan? SlidingExpiration => TimeSpan.FromMinutes(1);

    // Optionally, provide a different implementation here. By default the CacheKey will be in the following format:
    //     Query{CustomerNumber:001425}
    public string GetCacheKey(GetAlbumByIdQuery query)
    {
        return $"Customers.{query.Id}";
    }
}