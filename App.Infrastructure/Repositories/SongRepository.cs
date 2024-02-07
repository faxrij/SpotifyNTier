using App.Domain.Entities;
using App.Infrastructure.Contexts;
using App.Logic.Commands.AddSong;
using App.Logic.Commands.UpdateSong;
using App.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace App.Infrastructure.Repositories;

internal class SongRepository(DataBaseContext context) : ISongRepository
{
    public async Task<List<Song>> GetAllSongsAsync()
    {
        var result = await context.Songs.Include(s => s.Album).ToListAsync();
        Log.Information("Get All Songs => {@result}", result);
        return result;
    }

    public async Task<Song?> GetSongByIdAsync(int id)
    {
        var result = await context.Songs.Include(s => s.Categories).Include(s => s.Album)
            .FirstOrDefaultAsync(s => s.Id == id);
        Log.Information("Get Song By Id => {@result}", result);
        return result;
    }

    public async Task<Song> CreateSongAsync(AddSongCommand addSongCommand)
    {
        Log.Information("Create Song => {@request}", addSongCommand);

        Song song = new Song();
        Album? album = await context.Albums.Where(a => a.Id == addSongCommand.AlbumId).FirstOrDefaultAsync();

        if (album == null)
        {
            Log.Error($"Album with ID {addSongCommand.AlbumId} not found.");
            throw new InvalidOperationException($"Album with ID {addSongCommand.AlbumId} not found.");
        }
        
        song.Album = album;
        song.Lyrics = addSongCommand.Lyrics;
        song.Title = addSongCommand.Title;
        song.DurationInSeconds = addSongCommand.DurationInSeconds;
        song.Categories = new List<Category>();
        context.Songs.Add(song);
        await context.SaveChangesAsync();
        return song;
    }

    public async Task<bool> RemoveSongAsync(int id)
    {
        Log.Information("Remove Song By Id => {@id}", id);
        var songToRemove = await context.Songs.FindAsync(id);

        if (songToRemove == null)
        {
            Log.Error($"Provided Song with ID {id} not found.");
            throw new InvalidOperationException($"Provided Song with ID {id} not found.");
        }

        context.Songs.Remove(songToRemove);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<Song?> UpdateSongAsync(UpdateSongCommand updateSongCommand, int id)
    {
        Log.Information("Update Song By Id => {@id} => {@request}" , id, updateSongCommand);
        var songToUpdate = await context.Songs.FindAsync(id);
        if (songToUpdate == null)
        {
            Log.Error($"Provided Song with ID {id} not found.");
            throw new InvalidOperationException($"Provided Song with ID {id} not found.");
        }

        var album = await context.Albums.FirstOrDefaultAsync(c => c.Id == updateSongCommand.AlbumId);

        if (album == null)
        {
            Log.Error($"Album with ID {id} not found.");
            throw new InvalidOperationException($"Album with ID {id} not found.");
        }

        songToUpdate.DurationInSeconds = updateSongCommand.DurationInSeconds;
        songToUpdate.Album = album;
        songToUpdate.Lyrics = updateSongCommand.Lyrics;
        songToUpdate.Title = updateSongCommand.Title;
        
        await context.SaveChangesAsync();
        return songToUpdate;
    }
}