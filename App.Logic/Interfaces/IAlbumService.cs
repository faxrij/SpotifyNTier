using App.DataTransferObjects.Request;
using App.Entities;

namespace App.Logic.SampleLogic.Services;

public interface IAlbumService
{
    Task<List<Album>> GetAllAlbumsAsync();
    Task<Album?> GetAlbumByIdAsync(int id);
    Task<Album> CreateAlbumAsync(CreateAlbumRequest createAlbumRequest);
    Task<bool> RemoveAlbumAsync(int id);
    Task<Album?> UpdateAlbumAsync(UpdateAlbumRequest updateAlbumRequest, int id);
}
