using App.Domain.Entities;
using App.Infrastructure.Contexts;
using App.Logic.Commands.AddAlbum;
using App.Logic.Commands.UpdateAlbum;
using App.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Repositories;

internal class AlbumRepository(DataBaseContext context) : IAlbumRepository
{
    public async Task<List<Album>> GetAllAlbumsAsync()
    {
        return await context.Albums.ToListAsync();
    }

    public async Task<Album?> GetAlbumByIdAsync(int id)
    {
        return await context.Albums
            .Include(a => a.Songs)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Album> CreateAlbumAsync(AddAlbumCommand addAlbumCommand)
    {
        Album album = new Album();
        album.Songs = new List<Song>();
        album.ReleaseYear = addAlbumCommand.ReleaseYear;
        album.Title = addAlbumCommand.Title;
        context.Albums.Add(album);
        await context.SaveChangesAsync();
        return album;
    }

    public async Task<bool> RemoveAlbumAsync(int id)
    {
        var albumToRemove = await context.Albums.FindAsync(id);

        if (albumToRemove == null)
        {
            return false;
        }

        context.Albums.Remove(albumToRemove);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<Album?> UpdateAlbumAsync(UpdateAlbumCommand updateAlbumCommand, int id)
    {
        var albumToUpdate = await context.Albums.FindAsync(id);
        if (albumToUpdate == null)
        {
            throw new InvalidOperationException($"Album with ID {id} not found.");
        }

        albumToUpdate.ReleaseYear = updateAlbumCommand.ReleaseYear;
        albumToUpdate.Title = updateAlbumCommand.Title;
        
        await context.SaveChangesAsync();
        return albumToUpdate;
    }
}
