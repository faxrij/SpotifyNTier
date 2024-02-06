using App.Domain.Entities;
using App.Logic.Interfaces;
using MediatR;

namespace App.Logic.Commands.UpdateSinger;

public class UpdateSingerCommandHandler(ISingerRepository singerRepository) : IRequestHandler<UpdateSingerCommand, Singer>
{
    public async Task<Singer> Handle(UpdateSingerCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        return await singerRepository.UpdateSingerAsync(request, id);
    }
}