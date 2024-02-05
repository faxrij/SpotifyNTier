using App.Domain.Entities;
using App.Logic.Commands.AddAlbum;
using App.Logic.Commands.UpdateAlbum;
using App.Logic.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AlbumController(IAlbumRepository _albumRepository, IMediator _mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Album>>> GetAlbums()
    {
        var albums = await _albumRepository.GetAllAlbumsAsync();
        return Ok(albums);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Album>> GetAlbum(int id)
    {
        var album = await _albumRepository.GetAlbumByIdAsync(id);
        return Ok(album);
    }

    [HttpPost]
    public async Task<ActionResult<Album>> CreateAlbum(AddAlbumCommand addAlbumCommand)
    {
        return await _mediator.Send(addAlbumCommand);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAlbum(int id)
    {
        var result = await _albumRepository.RemoveAlbumAsync(id);
        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult<Album>> UpdateAlbum(UpdateAlbumCommand updateAlbumCommand)
    {
        return await _mediator.Send(updateAlbumCommand);
    }
}