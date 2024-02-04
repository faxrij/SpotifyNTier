using App.DataTransferObjects.Request;
using App.Entities;
using App.Logic.SampleLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace SpotifyNTier.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SingerController : ControllerBase
{
    private readonly ISingerService _singerService;

    public SingerController(ISingerService singerService)
    {
        _singerService = singerService;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Singer>>> GetSingers()
    {
        var singers = await _singerService.GetAllSingersAsync();
        return Ok(singers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Singer>> GetSinger(int id)
    {
        var singer = await _singerService.GetSingerByIdAsync(id);
        return Ok(singer);
    }

    [HttpPost]
    public async Task<ActionResult<Singer>> CreateSinger(CreateSingerRequest createSingerRequest)
    {
        var createdSinger = await _singerService.CreateSingerAsync(createSingerRequest);
        return CreatedAtAction(nameof(GetSinger), new { id = createdSinger.Id }, createdSinger);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSinger(int id)
    {
        var result = await _singerService.RemoveSingerAsync(id);
        return Ok(result);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSinger(UpdateSingerRequest updateSingerRequest, int id)
    {
        var singer = await _singerService.UpdateSingerAsync(updateSingerRequest, id);
        return Ok(singer);
    }
}