using App.Domain.Entities;
using App.Logic.Interfaces;
using MediatR;

namespace App.Logic.Queries.GetSinger.GetAllSingers;

public class GetAllSingersQueryHandler(ISingerRepository singerRepository) : IRequestHandler<GetAllSingersQuery, List<Singer>>
{
    public async Task<List<Singer>> Handle(GetAllSingersQuery request, CancellationToken cancellationToken)
    {
        return await singerRepository.GetAllSingersAsync();
    }
}