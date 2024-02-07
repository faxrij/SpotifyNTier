using App.Domain.Entities;
using App.Infrastructure.Contexts;
using App.Logic.Commands.AddAlbum;
using App.Logic.Commands.UpdateAlbum;
using App.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace App.Infrastructure.Repositories;

internal class AlbumRepository(DataBaseContext context) : IAlbumRepository
{
    public async Task<List<Album>> GetAllAlbumsAsync()
    {
        var result = await context.Albums.ToListAsync(); 
        Log.Information("Get All Albums => {@result}", result);
        return result;
    }

    public async Task<Album?> GetAlbumByIdAsync(int id)
    {
        var result = await context.Albums.Include(a => a.Songs).FirstOrDefaultAsync(a => a.Id == id);
        Log.Information("Get Album By Id => {@result}", result);
        return result;
    }

    public async Task<Album> CreateAlbumAsync(AddAlbumCommand addAlbumCommand)
    {
        Log.Information("Create Album => {@request}", addAlbumCommand);

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
        Log.Information("Remove Album By Id => {@id}", id);

        var albumToRemove = await context.Albums.FindAsync(id);

        if (albumToRemove == null)
        {
            Log.Error($"Album with ID {id} not found.");
            throw new InvalidOperationException($"Album with ID {id} not found.");
        }

        context.Albums.Remove(albumToRemove);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<Album?> UpdateAlbumAsync(UpdateAlbumCommand updateAlbumCommand, int id)
    {
        Log.Information("Update Album By Id => {@id} => {@request}" , id, updateAlbumCommand);
        var albumToUpdate = await context.Albums.FindAsync(id);
        if (albumToUpdate == null)
        {
            Log.Error($"Album with ID {id} not found.");
            throw new InvalidOperationException($"Album with ID {id} not found.");
        }

        albumToUpdate.ReleaseYear = updateAlbumCommand.ReleaseYear;
        albumToUpdate.Title = updateAlbumCommand.Title;
        await context.SaveChangesAsync();
        return albumToUpdate;
    }
}
