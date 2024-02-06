using App.Domain.Entities;
using App.Logic.Interfaces;
using MediatR;

namespace App.Logic.Commands.AddSong;

public class AddSongCommandHandler(ISongRepository songRepository) : IRequestHandler<AddSongCommand, Song>
{
    public async Task<Song> Handle(AddSongCommand request, CancellationToken cancellationToken)
    {
        return await songRepository.CreateSongAsync(request);
    }
}