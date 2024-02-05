using App.Domain.Entities;
using App.Logic.Interfaces;
using MediatR;

namespace App.Logic.Commands.AddSong;

public class AddSongCommandHandler : IRequestHandler<AddSongCommand, Song>
{
    private readonly ISongRepository _songRepository;

    public AddSongCommandHandler(ISongRepository songRepository)
    {
        _songRepository = songRepository;
    }

    public async Task<Song> Handle(AddSongCommand request, CancellationToken cancellationToken)
    {
        return await _songRepository.CreateSongAsync(request);
    }
}