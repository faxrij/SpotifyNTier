using MediatR;

namespace App.Infrastructure.Behaviors;

public class CachingBehavior<TRequest, TResponse>(ICache cache, ILogger<CachingBehavior<TRequest, TResponse>> logger, 
    IEnumerable<ICachePolicy<TRequest, TResponse>> cachePolicies) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var cachePolicy = cachePolicies.FirstOrDefault();
        if (cachePolicy == null)
        {
            return await next();
        }
        var cacheKey = cachePolicy.GetCacheKey(request);
        var cachedResponse = await cache.GetAsync<TResponse>(cacheKey, cancellationToken);
        if (cachedResponse != null)
        {
            logger.LogDebug($"Response retrieved {typeof(TRequest).FullName} from cache. CacheKey: {cacheKey}");
            return cachedResponse;
        }

        var response = await next();
        logger.LogDebug($"Caching response for {typeof(TRequest).FullName} with cache key: {cacheKey}");

        await cache.SetAsync(cacheKey, response, cachePolicy.SlidingExpiration, cachePolicy.AbsoluteExpiration, cachePolicy.AbsoluteExpirationRelativeToNow, 
            cancellationToken);
        return response;    
    }
}