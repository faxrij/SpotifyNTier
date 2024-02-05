using System.Collections.Generic;
using System.Threading.Tasks;
using App.Entities;
using App.Logic.DataTransferObjects.Request;

namespace App.Logic.Interfaces;

public interface ISingerService
{
    Task<List<Singer>> GetAllSingersAsync();
    Task<Singer?> GetSingerByIdAsync(int id);
    Task<Singer> CreateSingerAsync(CreateSingerRequest createSingerRequest);
    Task<bool> RemoveSingerAsync(int id);
    Task<Singer?> UpdateSingerAsync(UpdateSingerRequest updateSingerRequest, int id);
}
