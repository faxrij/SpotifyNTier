using App.Entities;
using App.Logic.DataTransferObjects.Request;
using App.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SingerController : ControllerBase
{
    private readonly ISingerRepository _singerRepository;

    public SingerController(ISingerRepository singerRepository)
    {
        _singerRepository = singerRepository;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Singer>>> GetSingers()
    {
        var singers = await _singerRepository.GetAllSingersAsync();
        return Ok(singers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Singer>> GetSinger(int id)
    {
        var singer = await _singerRepository.GetSingerByIdAsync(id);
        return Ok(singer);
    }

    [HttpPost]
    public async Task<ActionResult<Singer>> CreateSinger(CreateSingerRequest createSingerRequest)
    {
        var createdSinger = await _singerRepository.CreateSingerAsync(createSingerRequest);
        return CreatedAtAction(nameof(GetSinger), new { id = createdSinger.Id }, createdSinger);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSinger(int id)
    {
        var result = await _singerRepository.RemoveSingerAsync(id);
        return Ok(result);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSinger(UpdateSingerRequest updateSingerRequest, int id)
    {
        var singer = await _singerRepository.UpdateSingerAsync(updateSingerRequest, id);
        return Ok(singer);
    }
}