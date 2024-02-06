using App.Domain.Entities;
using App.Logic.Interfaces;
using FluentValidation;
using MediatR;

namespace App.Logic.Commands.AddSinger;

public class AddSingerCommandHandler(ISingerRepository singerRepository, IValidator<AddSingerCommand> validator) : IRequestHandler<AddSingerCommand, Singer>
{
    public async Task<Singer> Handle(AddSingerCommand request, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);
        return await singerRepository.CreateSingerAsync(request);
    }
}