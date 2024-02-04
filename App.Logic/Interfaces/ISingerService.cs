using App.DataTransferObjects.Request;
using App.Entities;

namespace App.Logic.SampleLogic.Services;

public interface ISingerService
{
    Task<List<Singer>> GetAllSingersAsync();
    Task<Singer?> GetSingerByIdAsync(int id);
    Task<Singer> CreateSingerAsync(CreateSingerRequest createSingerRequest);
    Task<bool> RemoveSingerAsync(int id);
    Task<Singer?> UpdateSingerAsync(UpdateSingerRequest updateSingerRequest, int id);
}
