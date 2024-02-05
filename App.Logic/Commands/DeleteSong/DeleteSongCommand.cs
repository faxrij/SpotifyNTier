using MediatR;

namespace App.Logic.Commands.DeleteSong;

public class DeleteSongCommand : IRequest<Boolean>
{
    public int Id { get; set; }
}