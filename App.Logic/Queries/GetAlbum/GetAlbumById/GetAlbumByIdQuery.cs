using App.Domain.Entities;
using MediatR;

namespace App.Logic.Queries.GetAlbum.GetAlbumById;

public class GetAlbumByIdQuery : IRequest<Album>
{
    public int? Id { get; set; }
}