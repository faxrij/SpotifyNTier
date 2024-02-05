using System.Collections.Generic;
using System.Threading.Tasks;
using App.Entities;
using App.Logic.DataTransferObjects.Request;

namespace App.Logic.Interfaces;

public interface ISongService
{
    Task<List<Song>> GetAllSongsAsync();
    Task<Song?> GetSongByIdAsync(int id);
    Task<Song> CreateSongAsync(CreateSongRequest createSongRequest);
    Task<bool> RemoveSongAsync(int id);
    Task<Song?> UpdateSongAsync(UpdateSongRequest updateSongRequest, int id);
}
