using System.ComponentModel.DataAnnotations;
using App.Domain.Entities;
using App.Logic.Interfaces;
using MediatR;

namespace App.Logic.Commands.UpdateSong;

public class UpdateSongCommandHandler : IRequestHandler<UpdateSongCommand, Song>
{
    private readonly ISongRepository _songRepository;

    public UpdateSongCommandHandler(ISongRepository songRepository)
    {
        _songRepository = songRepository;
    }

    public async Task<Song> Handle(UpdateSongCommand request, CancellationToken cancellationToken)
    {
        int id = request.Id;
        return await _songRepository.UpdateSongAsync(request, id);
    }
}