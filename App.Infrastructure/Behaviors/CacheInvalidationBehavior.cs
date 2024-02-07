using MediatR;

namespace App.Infrastructure.Behaviors;

public class CacheInvalidationBehavior<TRequest, TResponse>(IEnumerable<ICacheInvalidator<TRequest>> cacheInvalidators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly List<ICacheInvalidator<TRequest>> _cacheInvalidators = cacheInvalidators.ToList();

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var result = await next();

        // now loop through each cache invalidator for this request type and call the Invalidate method passing
        // an instance of this request in order to retrieve a cache key.
        foreach (var invalidator in _cacheInvalidators)
        {
            await invalidator.Invalidate(request);
        }

        return result;    
    }
}