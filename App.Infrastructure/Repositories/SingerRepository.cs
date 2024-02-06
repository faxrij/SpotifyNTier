using App.Domain.Entities;
using App.Infrastructure.Contexts;
using App.Logic.Commands.AddSinger;
using App.Logic.Commands.UpdateSinger;
using App.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Repositories;

internal class SingerRepository(DataBaseContext context) : ISingerRepository
{
    public async Task<List<Singer>> GetAllSingersAsync()
    {
        return await context.Singers.ToListAsync();
    }

    public async Task<Singer?> GetSingerByIdAsync(int id)
    {
        return await context.Singers
            .Include(s => s.Albums) 
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Singer> CreateSingerAsync(AddSingerCommand addSingerCommand)
    {
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
        var singerToUpdate = await context.Singers.FindAsync(id);
        if (singerToUpdate == null)
        {
            throw new InvalidOperationException($"Provided category with ID {id} not found.");
        }

        singerToUpdate.BirthDate = updateSingerCommand.BirthDate;
        singerToUpdate.Name = updateSingerCommand.Name;
        
        await context.SaveChangesAsync();
        return singerToUpdate;
    }
}
