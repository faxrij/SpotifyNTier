using MediatR;

namespace App.Logic.Commands.DeleteAlbum;

public class DeleteAlbumCommand : IRequest<Boolean>
{
    public int Id { get; set; }
}