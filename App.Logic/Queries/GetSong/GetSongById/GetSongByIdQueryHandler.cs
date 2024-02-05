using App.Domain.Entities;
using App.Logic.Interfaces;
using MediatR;

namespace App.Logic.Queries.GetSong.GetSongById;

public class GetSongByIdQueryHandler(ISongRepository songRepository) : IRequestHandler<GetSongByIdQuery, Song>
{
    public async Task<Song> Handle(GetSongByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.Id == null)
        {
            throw new InvalidOperationException("Error when Searching Null id");
        }

        return await songRepository.GetSongByIdAsync(request.Id.Value);
    }
}