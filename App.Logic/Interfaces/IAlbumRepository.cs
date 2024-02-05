using App.Domain.Entities;
using App.Logic.Commands;
using App.Logic.DataTransferObjects.Request;

namespace App.Logic.Interfaces;

public interface IAlbumRepository
{
    Task<List<Album>> GetAllAlbumsAsync();
    Task<Album?> GetAlbumByIdAsync(int id);
    Task<Album> CreateAlbumAsync(AddAlbumCommand createAlbumRequest);
    Task<bool> RemoveAlbumAsync(int id);
    Task<Album?> UpdateAlbumAsync(UpdateAlbumRequest updateAlbumRequest, int id);
}
