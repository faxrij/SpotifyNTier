using App.Domain.Entities;
using App.Logic.Commands.AddSong;
using App.Logic.Commands.UpdateSong;

namespace App.Logic.Interfaces;

public interface ISongRepository
{
    Task<List<Song>> GetAllSongsAsync();
    Task<Song?> GetSongByIdAsync(int id);
    Task<Song> CreateSongAsync(AddSongCommand addSongCommand);
    Task<bool> RemoveSongAsync(int id);
    Task<Song?> UpdateSongAsync(UpdateSongCommand updateSongCommand, int id);
}
