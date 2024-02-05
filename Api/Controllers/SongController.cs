using App.Domain.Entities;
using App.Logic.Commands.AddSong;
using App.Logic.Commands.DeleteSong;
using App.Logic.Commands.UpdateSong;
using App.Logic.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class SongController(ISongRepository songRepository, IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllSongs()
    {
        var songs = await songRepository.GetAllSongsAsync();
        return Ok(songs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSongById(int id)
    {
        var song = await songRepository.GetSongByIdAsync(id); 
        return Ok(song);
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