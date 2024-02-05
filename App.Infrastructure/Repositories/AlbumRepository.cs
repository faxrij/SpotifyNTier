using App.Domain.Entities;
using App.Infrastructure.Contexts;
using App.Logic.Commands.AddAlbum;
using App.Logic.Commands.UpdateAlbum;
using App.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Repositories;

internal class AlbumRepository : IAlbumRepository
{
    private readonly DataBaseContext _context;

    public AlbumRepository(DataBaseContext context)
    {
        _context = context;
    }

    public async Task<List<Album>> GetAllAlbumsAsync()
    {
        return await _context.Albums.ToListAsync();
    }

    public async Task<Album?> GetAlbumByIdAsync(int id)
    {
        return await _context.Albums
            .Include(a => a.Songs)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Album> CreateAlbumAsync(AddAlbumCommand addAlbumCommand)
    {
        Album album = new Album();
        album.Songs = new List<Song>();
        album.ReleaseYear = addAlbumCommand.ReleaseYear;
        album.Title = addAlbumCommand.Title;
        _context.Albums.Add(album);
        await _context.SaveChangesAsync();
        return album;
    }

    public async Task<bool> RemoveAlbumAsync(int id)
    {
        var albumToRemove = await _context.Albums.FindAsync(id);

        if (albumToRemove == null)
        {
            return false;
        }

        _context.Albums.Remove(albumToRemove);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Album?> UpdateAlbumAsync(UpdateAlbumCommand updateAlbumCommand, int id)
    {
        var albumToUpdate = await _context.Albums.FindAsync(id);
        if (albumToUpdate == null)
        {
            throw new InvalidOperationException($"Album with ID {id} not found.");
        }

        albumToUpdate.ReleaseYear = updateAlbumCommand.ReleaseYear;
        albumToUpdate.Title = updateAlbumCommand.Title;
        
        await _context.SaveChangesAsync();
        return albumToUpdate;
    }
}
