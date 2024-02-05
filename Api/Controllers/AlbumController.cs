using App.Entities;
using App.Logic.DataTransferObjects.Request;
using App.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AlbumController : ControllerBase
{
    private readonly IAlbumRepository _albumRepository;

    public AlbumController(IAlbumRepository albumRepository)
    {
        _albumRepository = albumRepository;
    }
    
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
    public async Task<ActionResult<Album>> CreateAlbum(CreateAlbumRequest createAlbumRequest)
    {
        var createdAlbum = await _albumRepository.CreateAlbumAsync(createAlbumRequest);
        return CreatedAtAction(nameof(GetAlbum), new { id = createdAlbum.Id }, createdAlbum);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAlbum(int id)
    {
        var result = await _albumRepository.RemoveAlbumAsync(id);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAlbum(UpdateAlbumRequest updateAlbumRequest, int id)
    {
        var album = await _albumRepository.UpdateAlbumAsync(updateAlbumRequest, id);
        return Ok(album);
    }
}