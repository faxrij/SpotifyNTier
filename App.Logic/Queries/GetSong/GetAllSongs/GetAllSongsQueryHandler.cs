using App.Domain.Entities;
using App.Logic.Interfaces;
using MediatR;

namespace App.Logic.Queries.GetSong.GetAllSongs;

public class GetAllSongsQueryHandler(ISongRepository songRepository) : IRequestHandler<GetAllSongsQuery, List<Song>>
{
    public async Task<List<Song>> Handle(GetAllSongsQuery request, CancellationToken cancellationToken)
    {
        return await songRepository.GetAllSongsAsync();
    }
}