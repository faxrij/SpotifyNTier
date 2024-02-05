using App.Domain.Entities;
using MediatR;

namespace App.Logic.Queries.GetSinger.GetAllSingers;

public class GetAllSingersQuery : IRequest<List<Singer>>
{
}