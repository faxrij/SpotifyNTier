using App.Domain.Entities;
using App.Infrastructure.Contexts;
using App.Logic.Commands.AddSinger;
using App.Logic.Commands.UpdateSinger;
using App.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace App.Infrastructure.Repositories;

internal class SingerRepository(DataBaseContext context) : ISingerRepository
{
    public async Task<List<Singer>> GetAllSingersAsync()
    {
        var result = await context.Singers.ToListAsync();
        Log.Information("Get All Singers => {@result}", result);
        return result;
    }

    public async Task<Singer?> GetSingerByIdAsync(int id)
    {
        var result = await context.Singers.Include(s => s.Albums).FirstOrDefaultAsync(s => s.Id == id);
        Log.Information("Get Singer By Id => {@result}", result);
        return result;
    }

    public async Task<Singer> CreateSingerAsync(AddSingerCommand addSingerCommand)
    {
        Log.Information("Create Singer => {@request}", addSingerCommand);
        Singer singer = new Singer();
        singer.Albums = new List<Album>();
        singer.Name = addSingerCommand.Name;
        singer.BirthDate = addSingerCommand.BirthDate;
        context.Singers.Add(singer);
        await context.SaveChangesAsync();
        return singer;
    }

    public async Task<bool> RemoveSingerAsync(int id)
    {
        Log.Information("Remove Singer By Id => {@id}", id);
        var singerToRemove = await context.Singers.FindAsync(id);

        if (singerToRemove == null)
        {
            return false;
        }
        context.Singers.Remove(singerToRemove);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<Singer?> UpdateSingerAsync(UpdateSingerCommand updateSingerCommand, int id)
    {
        Log.Information("Update Singer By Id => {@id} => {@request}" , id, updateSingerCommand);
        var singerToUpdate = await context.Singers.FindAsync(id);
        if (singerToUpdate == null)
        {
            Log.Error($"Provided Singer with ID {id} not found.");
            throw new InvalidOperationException($"Provided Singer with ID {id} not found.");
        }

        singerToUpdate.BirthDate = updateSingerCommand.BirthDate;
        singerToUpdate.Name = updateSingerCommand.Name;
        await context.SaveChangesAsync();
        return singerToUpdate;
    }
}
