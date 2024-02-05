using App.Domain.Entities;
using App.Infrastructure.Contexts;
using App.Logic.Commands.AddSinger;
using App.Logic.Commands.UpdateSinger;
using App.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Repositories;

internal class SingerRepository : ISingerRepository
{
    private readonly DataBaseContext _context;

    public SingerRepository(DataBaseContext context)
    {
        _context = context;
    }

    public async Task<List<Singer>> GetAllSingersAsync()
    {
        return await _context.Singers.ToListAsync();
    }

    public async Task<Singer?> GetSingerByIdAsync(int id)
    {
        return await _context.Singers
            .Include(s => s.Albums) 
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Singer> CreateSingerAsync(AddSingerCommand addSingerCommand)
    {
        Singer singer = new Singer();
        singer.Albums = new List<Album>();
        singer.Name = addSingerCommand.Name;
        singer.BirthDate = addSingerCommand.BirthDate;
        _context.Singers.Add(singer);
        await _context.SaveChangesAsync();
        return singer;
    }

    public async Task<bool> RemoveSingerAsync(int id)
    {
        var singerToRemove = await _context.Singers.FindAsync(id);

        if (singerToRemove == null)
        {
            return false;
        }

        _context.Singers.Remove(singerToRemove);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Singer?> UpdateSingerAsync(UpdateSingerCommand updateSingerCommand, int id)
    {
        var singerToUpdate = await _context.Singers.FindAsync(id);
        if (singerToUpdate == null)
        {
            throw new InvalidOperationException($"Provided category with ID {id} not found.");
        }

        singerToUpdate.BirthDate = updateSingerCommand.BirthDate;
        singerToUpdate.Name = updateSingerCommand.Name;
        
        await _context.SaveChangesAsync();
        return singerToUpdate;
    }
}
