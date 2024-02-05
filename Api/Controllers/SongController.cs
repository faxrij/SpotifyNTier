using App.Domain.Entities;
using App.Logic.Commands.AddSong;
using App.Logic.Commands.DeleteSong;
using App.Logic.Commands.UpdateSong;
using App.Logic.Queries.GetSong.GetAllSongs;
using App.Logic.Queries.GetSong.GetSongById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class SongController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Song>>> GetAllSongs()
    {
        return await mediator.Send(new GetAllSongsQuery());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Song>> GetSongById(int id)
    {
        var getSongByIdQuery = new GetSongByIdQuery() { Id = id };
        return await mediator.Send(getSongByIdQuery);
    }

    [HttpPost]
    public async Task<ActionResult<Song>> CreateSong(AddSongCommand addSongCommand)
    {
        return await mediator.Send(addSongCommand);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Boolean>> RemoveSong(int id)
    { 
        var deleteSongCommand = new DeleteSongCommand() { Id = id };
        return await mediator.Send(deleteSongCommand);
    }
    
    [HttpPut]
    public async Task<ActionResult<Song>> UpdateSong(UpdateSongCommand updateSongCommand)
    {
        return await mediator.Send(updateSongCommand);
    }
}