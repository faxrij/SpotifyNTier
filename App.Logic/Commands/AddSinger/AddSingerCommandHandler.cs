using App.Domain.Entities;
using App.Logic.Interfaces;
using MediatR;

namespace App.Logic.Commands.AddSinger;

public class AddSingerCommandHandler : IRequestHandler<AddSingerCommand, Singer>
{
    private readonly ISingerRepository _singerRepository;

    public AddSingerCommandHandler(ISingerRepository singerRepository)
    {
        _singerRepository = singerRepository;
    }

    public async Task<Singer> Handle(AddSingerCommand request, CancellationToken cancellationToken)
    {
        return await _singerRepository.CreateSingerAsync(request);
    }
}