using App.Logic.Interfaces;
using MediatR;

namespace App.Logic.Commands.DeleteSong;

public class DeleteSongCommandHandler : IRequestHandler<DeleteSongCommand, Boolean>
{
    private readonly ISongRepository _songRepository;

    public DeleteSongCommandHandler(ISongRepository songRepository)
    {
        _songRepository = songRepository;
    }

    public async Task<bool> Handle(DeleteSongCommand request, CancellationToken cancellationToken)
    {
        var song = await _songRepository.RemoveSongAsync(request.Id);
        return song;
    }
}