// SongService.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Entities;
using App.Infrastructure.Contexts;
using App.Logic.DataTransferObjects.Request;
using App.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Logic.Services;

public class SongRepository : ISongService
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

    public async Task<Song> CreateSongAsync(CreateSongRequest createSongRequest)
    {
        Song song = new Song();
        Album? album = await _context.Albums.Where(a => a.Id == createSongRequest.AlbumId).FirstOrDefaultAsync();

        if (album == null)
        {
            throw new InvalidOperationException($"Album with ID {createSongRequest.AlbumId} not found.");
        }
        
        song.Album = album;
        song.Lyrics = createSongRequest.Lyrics;
        song.Title = createSongRequest.Title;
        song.DurationInSeconds = createSongRequest.DurationInSeconds;
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

    public async Task<Song?> UpdateSongAsync(UpdateSongRequest updateSongRequest, int id)
    {
        var songToUpdate = await _context.Songs.FindAsync(id);
        if (songToUpdate == null)
        {
            throw new InvalidOperationException($"Provided Song with ID {id} not found.");
        }

        var album = await _context.Albums.FirstOrDefaultAsync(c => c.Id == updateSongRequest.AlbumId);

        if (album == null)
        {
            throw new InvalidOperationException($"Album with ID {id} not found.");
        }

        songToUpdate.DurationInSeconds = updateSongRequest.DurationInSeconds;
        songToUpdate.Album = album;
        songToUpdate.Lyrics = updateSongRequest.Lyrics;
        songToUpdate.Title = updateSongRequest.Title;
        
        await _context.SaveChangesAsync();
        return songToUpdate;
    }
}