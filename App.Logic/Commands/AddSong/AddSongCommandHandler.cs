using App.Domain.Entities;
using App.Logic.Interfaces;
using FluentValidation;
using MediatR;

namespace App.Logic.Commands.AddSong;

public class AddSongCommandHandler(ISongRepository songRepository, IValidator<AddSongCommand> validator) : IRequestHandler<AddSongCommand, Song>
{
    public async Task<Song> Handle(AddSongCommand request, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);
        return await songRepository.CreateSongAsync(request);
    }
}