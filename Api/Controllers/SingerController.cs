using App.Domain.Entities;
using App.Logic.Commands.AddSinger;
using App.Logic.Commands.DeleteSinger;
using App.Logic.Commands.UpdateSong;
using App.Logic.Queries.GetSinger.GetAllSingers;
using App.Logic.Queries.GetSinger.GetSingerById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SingerController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Singer>>> GetSingers()
    {
        return await mediator.Send(new GetAllSingersQuery());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Singer>> GetSinger(int id)
    {
        var getSingerByIdQuery = new GetSingerByIdQuery() { Id = id };
        return await mediator.Send(getSingerByIdQuery);
    }

    [HttpPost]
    public async Task<ActionResult<Singer>> CreateSinger(AddSingerCommand addSingerCommand)
    {
        return await mediator.Send(addSingerCommand);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Boolean>> DeleteSinger(int id)
    {
        var deleteSingerCommand = new DeleteSingerCommand() { Id = id };
        return await mediator.Send(deleteSingerCommand);
    }
    
    [HttpPut]
    public async Task<ActionResult<Song>> UpdateSinger(UpdateSongCommand updateSongCommand)
    {
        return await mediator.Send(updateSongCommand);
    }
}