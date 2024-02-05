using App.Domain.Entities;
using App.Logic.Interfaces;
using MediatR;

namespace App.Logic.Commands.UpdateSinger;

public class UpdateSingerCommandHandler : IRequestHandler<UpdateSingerCommand, Singer>
{
    private readonly ISingerRepository _singerRepository;

    public UpdateSingerCommandHandler(ISingerRepository singerRepository)
    {
        _singerRepository = singerRepository;
    }

    public async Task<Singer> Handle(UpdateSingerCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        return await _singerRepository.UpdateSingerAsync(request, id);
    }
}