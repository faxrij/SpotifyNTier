using App.Domain.Entities;
using MediatR;

namespace App.Logic.Queries.GetSong.GetSongById;

public class GetSongByIdQuery : IRequest<Song>
{
    public int? Id { get; set; }
}