using MediatR;

namespace App.Logic.Interfaces;

public interface ICachedQuery
{
    string Key { get; set; }
    TimeSpan? Expiration { get;  }
}

public interface ICachedQuery<TResponse> : IRequest<TResponse>
{
    string CacheKey { get; }
    TimeSpan? Expiration { get; }
}