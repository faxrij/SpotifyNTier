using System.Threading.Tasks;
using App.DataTransferObjects.Request;
using App.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class SongController : ControllerBase
{
    private readonly ISongService _songService;

    public SongController(ISongService songService)
    {
        _songService = songService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSongs()
    {
        var songs = await _songService.GetAllSongsAsync();
        return Ok(songs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSongById(int id)
    {
        var song = await _songService.GetSongByIdAsync(id); 
        return Ok(song);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSong(CreateSongRequest createSongRequest)
    {
        var createdSong = await _songService.CreateSongAsync(createSongRequest);
        return CreatedAtAction(nameof(GetSongById), new { id = createdSong.Id }, createdSong);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveSong(int id)
    { 
        var isRemoved = await _songService.RemoveSongAsync(id); 
        return Ok(isRemoved);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSong(UpdateSongRequest updateSongRequest, int id)
    {
        var song = await _songService.UpdateSongAsync(updateSongRequest, id);
        return Ok(song);
    }
}