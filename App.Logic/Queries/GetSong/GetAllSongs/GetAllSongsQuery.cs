using App.Domain.Entities;
using MediatR;

namespace App.Logic.Queries.GetSong.GetAllSongs;

public class GetAllSongsQuery : IRequest<List<Song>>
{
}