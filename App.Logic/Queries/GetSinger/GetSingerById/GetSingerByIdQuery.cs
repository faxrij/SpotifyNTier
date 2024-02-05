using App.Domain.Entities;
using MediatR;

namespace App.Logic.Queries.GetSinger.GetSingerById;

public class GetSingerByIdQuery : IRequest<Singer>
{
    public int? Id { get; set; }
}