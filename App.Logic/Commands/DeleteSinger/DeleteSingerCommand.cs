using MediatR;

namespace App.Logic.Commands.DeleteSinger;

public class DeleteSingerCommand : IRequest<Boolean>
{
    public int Id { get; set; }
}