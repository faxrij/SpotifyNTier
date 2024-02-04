using System.Collections.Generic;
using System.Threading.Tasks;
using App.DataTransferObjects.Request;
using App.Entities;
using App.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AlbumController : ControllerBase
{
    private readonly IAlbumService _albumService;

    public AlbumController(IAlbumService albumService)
    {
        _albumService = albumService;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Album>>> GetAlbums()
    {
        var albums = await _albumService.GetAllAlbumsAsync();
        return Ok(albums);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Album>> GetAlbum(int id)
    {
        var album = await _albumService.GetAlbumByIdAsync(id);
        return Ok(album);
    }

    [HttpPost]
    public async Task<ActionResult<Album>> CreateAlbum(CreateAlbumRequest createAlbumRequest)
    {
        var createdAlbum = await _albumService.CreateAlbumAsync(createAlbumRequest);
        return CreatedAtAction(nameof(GetAlbum), new { id = createdAlbum.Id }, createdAlbum);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAlbum(int id)
    {
        var result = await _albumService.RemoveAlbumAsync(id);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAlbum(UpdateAlbumRequest updateAlbumRequest, int id)
    {
        var album = await _albumService.UpdateAlbumAsync(updateAlbumRequest, id);
        return Ok(album);
    }
}