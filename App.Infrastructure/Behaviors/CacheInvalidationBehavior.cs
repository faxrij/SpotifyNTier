using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace App.Infrastructure.Behaviors;

public class CacheInvalidationBehavior<TRequest, TResponse>(IMemoryCache cache) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private void InvalidateCache(TRequest request)
    {
        string cacheKey = GenerateCacheKey(request);
        cache.Remove(cacheKey);
    }

    private string GenerateCacheKey(TRequest request)
    {
        return $"CacheKey_{request.GetType().FullName}";
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = await next();
        
        InvalidateCache(request);

        return response;
    }
}
