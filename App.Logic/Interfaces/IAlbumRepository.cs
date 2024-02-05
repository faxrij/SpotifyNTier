using App.Entities;
using App.Logic.DataTransferObjects.Request;

namespace App.Logic.Interfaces;

public interface IAlbumRepository
{
    Task<List<Album>> GetAllAlbumsAsync();
    Task<Album?> GetAlbumByIdAsync(int id);
    Task<Album> CreateAlbumAsync(CreateAlbumRequest createAlbumRequest);
    Task<bool> RemoveAlbumAsync(int id);
    Task<Album?> UpdateAlbumAsync(UpdateAlbumRequest updateAlbumRequest, int id);
}