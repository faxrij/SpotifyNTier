using App.Domain.Entities;
using App.Logic.Commands.AddAlbum;
using App.Logic.Commands.UpdateAlbum;

namespace App.Logic.Interfaces;

public interface IAlbumRepository
{
    Task<List<Album>> GetAllAlbumsAsync();
    Task<Album?> GetAlbumByIdAsync(int id);
    Task<Album> CreateAlbumAsync(AddAlbumCommand addAlbumCommand);
    Task<bool> RemoveAlbumAsync(int id);
    Task<Album?> UpdateAlbumAsync(UpdateAlbumCommand updateAlbumCommand, int id);
}
