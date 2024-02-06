using MediatR;

namespace App.Logic.Interfaces;

public interface IQuery<TResponse> : IRequest<TResponse>
{
}