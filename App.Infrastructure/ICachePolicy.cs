using MediatR;

namespace App.Infrastructure;

public interface ICachePolicy<in TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    DateTime? AbsoluteExpiration => null;
    TimeSpan? AbsoluteExpirationRelativeToNow => TimeSpan.FromMinutes(5);
    TimeSpan? SlidingExpiration => TimeSpan.FromSeconds(30);

    string GetCacheKey(TRequest request)
    {
        var r = new {request};
        var props = r.request.GetType().GetProperties().Select(pi => $"{pi.Name}:{pi.GetValue(r.request, null)}");
        return $"{typeof(TRequest).FullName}{{{String.Join(",", props)}}}";
    }
}