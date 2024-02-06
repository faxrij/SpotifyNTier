using App.Domain.Entities;
using App.Logic.Interfaces;
using MediatR;

namespace App.Logic.Commands.UpdateSong;

public class UpdateSongCommandHandler(ISongRepository songRepository) : IRequestHandler<UpdateSongCommand, Song>
{
    public async Task<Song> Handle(UpdateSongCommand request, CancellationToken cancellationToken)
    {
        int id = request.Id;
        return await songRepository.UpdateSongAsync(request, id);
    }
}