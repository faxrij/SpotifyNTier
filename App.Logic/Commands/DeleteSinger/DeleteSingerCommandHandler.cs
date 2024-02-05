using App.Logic.Interfaces;
using MediatR;

namespace App.Logic.Commands.DeleteSinger;

public class DeleteSingerCommandHandler : IRequestHandler<DeleteSingerCommand, Boolean>
{
    private readonly ISingerRepository _singerRepository;

    public DeleteSingerCommandHandler(ISingerRepository singerRepository)
    {
        _singerRepository = singerRepository;
    }

    public async Task<bool> Handle(DeleteSingerCommand request, CancellationToken cancellationToken)
    {
        var singer = await _singerRepository.RemoveSingerAsync(request.Id);
        return singer;
    }
}