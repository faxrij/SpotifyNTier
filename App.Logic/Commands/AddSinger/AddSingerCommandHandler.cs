using App.Domain.Entities;
using App.Logic.Interfaces;
using MediatR;

namespace App.Logic.Commands.AddSinger;

public class AddSingerCommandHandler(ISingerRepository singerRepository) : IRequestHandler<AddSingerCommand, Singer>
{
    public async Task<Singer> Handle(AddSingerCommand request, CancellationToken cancellationToken)
    {
        return await singerRepository.CreateSingerAsync(request);
    }
}