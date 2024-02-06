using App.Logic.Interfaces;
using MediatR;

namespace App.Infrastructure.Behaviors;

internal sealed class QueryCachingPipelineBehavior<TRequest, TResponse>(ICacheService cacheService) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICachedQuery<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        return await cacheService.GetOrCreateAsync(request.CacheKey, _ => next(), request.Expiration, cancellationToken);
    }
}

