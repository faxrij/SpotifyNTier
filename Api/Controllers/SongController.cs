using App.Logic.DataTransferObjects.Request;
using App.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class SongController : ControllerBase
{
    private readonly ISongRepository _songRepository;

    public SongController(ISongRepository songRepository)
    {
        _songRepository = songRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSongs()
    {
        var songs = await _songRepository.GetAllSongsAsync();
        return Ok(songs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSongById(int id)
    {
        var song = await _songRepository.GetSongByIdAsync(id); 
        return Ok(song);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSong(CreateSongRequest createSongRequest)
    {
        var createdSong = await _songRepository.CreateSongAsync(createSongRequest);
        return CreatedAtAction(nameof(GetSongById), new { id = createdSong.Id }, createdSong);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveSong(int id)
    { 
        var isRemoved = await _songRepository.RemoveSongAsync(id); 
        return Ok(isRemoved);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSong(UpdateSongRequest updateSongRequest, int id)
    {
        var song = await _songRepository.UpdateSongAsync(updateSongRequest, id);
        return Ok(song);
    }
}