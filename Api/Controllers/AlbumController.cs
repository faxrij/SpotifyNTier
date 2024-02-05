using App.Domain.Entities;
using App.Logic.Commands.AddAlbum;
using App.Logic.Commands.DeleteAlbum;
using App.Logic.Commands.UpdateAlbum;
using App.Logic.Queries.GetAlbum.GetAlbumById;
using App.Logic.Queries.GetAlbum.GetAllAlbums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AlbumController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Album>>> GetAlbums()
    {
        return await mediator.Send(new GetAllAlbumsQuery());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Album>> GetAlbum(int id)
    {
        var getAlbumByIdQuery = new GetAlbumByIdQuery { Id = id };
        return await mediator.Send(getAlbumByIdQuery);
    }

    [HttpPost]
    public async Task<ActionResult<Album>> CreateAlbum(AddAlbumCommand addAlbumCommand)
    {
        return await mediator.Send(addAlbumCommand);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Boolean>> DeleteAlbum(int id)
    {
        var deleteAlbumCommand = new DeleteAlbumCommand { Id = id };
        return await mediator.Send(deleteAlbumCommand);
    }

    [HttpPut]
    public async Task<ActionResult<Album>> UpdateAlbum(UpdateAlbumCommand updateAlbumCommand)
    {
        return await mediator.Send(updateAlbumCommand);
    }
}