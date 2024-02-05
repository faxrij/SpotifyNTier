using App.Domain.Entities;
using App.Infrastructure.Contexts;
using App.Logic.Commands.AddSong;
using App.Logic.Commands.UpdateSong;
using App.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Repositories;

internal class SongRepository : ISongRepository
{
    private readonly DataBaseContext _context;

    public SongRepository(DataBaseContext context)
    {
        _context = context;
    }

    public async Task<List<Song>> GetAllSongsAsync()
    {
        return await _context.Songs.ToListAsync();
    }

    public async Task<Song?> GetSongByIdAsync(int id)
    {
        return await _context.Songs
            .Include(s => s.Categories)
            .Include(s => s.Album)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Song> CreateSongAsync(AddSongCommand addSongCommand)
    {
        Song song = new Song();
        Album? album = await _context.Albums.Where(a => a.Id == addSongCommand.AlbumId).FirstOrDefaultAsync();

        if (album == null)
        {
            throw new InvalidOperationException($"Album with ID {addSongCommand.AlbumId} not found.");
        }
        
        song.Album = album;
        song.Lyrics = addSongCommand.Lyrics;
        song.Title = addSongCommand.Title;
        song.DurationInSeconds = addSongCommand.DurationInSeconds;
        song.Categories = new List<Category>();
        _context.Songs.Add(song);
        await _context.SaveChangesAsync();
        return song;
    }

    public async Task<bool> RemoveSongAsync(int id)
    {
        var songToRemove = await _context.Songs.FindAsync(id);

        if (songToRemove == null)
        {
            return false;
        }

        _context.Songs.Remove(songToRemove);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Song?> UpdateSongAsync(UpdateSongCommand updateSongCommand, int id)
    {
        var songToUpdate = await _context.Songs.FindAsync(id);
        if (songToUpdate == null)
        {
            throw new InvalidOperationException($"Provided Song with ID {id} not found.");
        }

        var album = await _context.Albums.FirstOrDefaultAsync(c => c.Id == updateSongCommand.AlbumId);

        if (album == null)
        {
            throw new InvalidOperationException($"Album with ID {id} not found.");
        }

        songToUpdate.DurationInSeconds = updateSongCommand.DurationInSeconds;
        songToUpdate.Album = album;
        songToUpdate.Lyrics = updateSongCommand.Lyrics;
        songToUpdate.Title = updateSongCommand.Title;
        
        await _context.SaveChangesAsync();
        return songToUpdate;
    }
}