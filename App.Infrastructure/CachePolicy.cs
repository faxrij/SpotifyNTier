using MediatR;

namespace App.Infrastructure;

public class CachePolicy<TRequest, TResponse> : ICachePolicy<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public DateTime? AbsoluteExpiration => null;
    public TimeSpan? AbsoluteExpirationRelativeToNow => TimeSpan.FromMinutes(5);
    public TimeSpan? SlidingExpiration => TimeSpan.FromSeconds(30);

    public string GetCacheKey(TRequest request)
    {
        var r = new { request };
        var props = r.request.GetType().GetProperties().Select(pi => $"{pi.Name}:{pi.GetValue(r.request, null)}");
        return $"{typeof(TRequest).FullName}{{{string.Join(",", props)}}}";
    }
}