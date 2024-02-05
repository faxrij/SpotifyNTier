using App.Domain.Entities;
using App.Logic.Commands.AddSinger;
using App.Logic.Commands.UpdateSinger;

namespace App.Logic.Interfaces;

public interface ISingerRepository
{
    Task<List<Singer>> GetAllSingersAsync();
    Task<Singer?> GetSingerByIdAsync(int id);
    Task<Singer> CreateSingerAsync(AddSingerCommand addSingerCommand);
    Task<bool> RemoveSingerAsync(int id);
    Task<Singer?> UpdateSingerAsync(UpdateSingerCommand updateSingerCommand, int id);
}
