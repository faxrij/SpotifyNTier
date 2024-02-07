using MediatR;

namespace App.Infrastructure.Behaviors;

public class CacheBehavior<TRequest, TResponse>(IEnumerable<ICache<TRequest, TResponse>> cachedRequests) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly List<ICache<TRequest, TResponse>> _caches = cachedRequests.ToList();

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // It's possible an ICache for the same request could be added more than once but just try and get the first one
        var cacheRequest = _caches.FirstOrDefault();
        if (cacheRequest == null)
        {
            // A cache request handler implementation for this request was not found, do nothing and continue
            return await next();
        }

        // try and get the response out of cache for this request
        var cachedResult = await cacheRequest.Get(request);

        if (cachedResult != null)
        {
            // cached response found, return and short-circuit the pipeline
            return cachedResult;
        }

        // No cached response was found so continue the handler pipeline and cache the result
        var result = await next();
        await cacheRequest.Set(request, result);
        return result;    
    }
}
