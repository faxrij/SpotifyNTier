using App.Domain.Entities;
using App.Logic.Interfaces;
using MediatR;

namespace App.Logic.Queries.GetSinger.GetSingerById;

public class GetSingerByIdQueryHandler(ISingerRepository singerRepository) : IRequestHandler<GetSingerByIdQuery, Singer>
{
    public async Task<Singer> Handle(GetSingerByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.Id == null)
        {
            throw new InvalidOperationException("Error when Searching Null id");
        }
        return await singerRepository.GetSingerByIdAsync(request.Id.Value);
    }
}