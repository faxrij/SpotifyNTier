using System.Collections.Generic;
using System.Threading.Tasks;
using App.DataTransferObjects.Request;
using App.Entities;

namespace App.Logic.Interfaces;

public interface ISongService
{
    Task<List<Song>> GetAllSongsAsync();
    Task<Song?> GetSongByIdAsync(int id);
    Task<Song> CreateSongAsync(CreateSongRequest createSongRequest);
    Task<bool> RemoveSongAsync(int id);
    Task<Song?> UpdateSongAsync(UpdateSongRequest updateSongRequest, int id);
}
