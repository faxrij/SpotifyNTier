using App.Domain.Entities;
using App.Logic.Commands.AddSinger;
using App.Logic.Commands.UpdateSong;
using App.Logic.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SingerController(ISingerRepository singerRepository, IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Singer>>> GetSingers()
    {
        var singers = await singerRepository.GetAllSingersAsync();
        return Ok(singers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Singer>> GetSinger(int id)
    {
        var singer = await singerRepository.GetSingerByIdAsync(id);
        return Ok(singer);
    }

    [HttpPost]
    public async Task<ActionResult<Singer>> CreateSinger(AddSingerCommand addSingerCommand)
    {
        return await mediator.Send(addSingerCommand);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSinger(int id)
    {
        var result = await singerRepository.RemoveSingerAsync(id);
        return Ok(result);
    }
    
    [HttpPut]
    public async Task<ActionResult<Song>> UpdateSinger(UpdateSongCommand updateSongCommand)
    {
        return await mediator.Send(updateSongCommand);
    }
}