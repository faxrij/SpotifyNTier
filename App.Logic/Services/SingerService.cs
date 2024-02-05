using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Entities;
using App.Infrastructure.Contexts;
using App.Logic.DataTransferObjects.Request;
using App.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Logic.Services;

public class SingerService : ISingerService
{
    private readonly DataBaseContext _context;

    public SingerService(DataBaseContext context)
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

    public async Task<Singer> CreateSingerAsync(CreateSingerRequest createSingerRequest)
    {
        Singer singer = new Singer();
        singer.Albums = new List<Album>();
        singer.Name = createSingerRequest.Name;
        singer.BirthDate = createSingerRequest.BirthDate;
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

    public async Task<Singer?> UpdateSingerAsync(UpdateSingerRequest updateSingerRequest, int id)
    {
        var singerToUpdate = await _context.Singers.FindAsync(id);
        if (singerToUpdate == null)
        {
            throw new InvalidOperationException($"Provided category with ID {id} not found.");
        }

        singerToUpdate.BirthDate = updateSingerRequest.BirthDate;
        singerToUpdate.Name = updateSingerRequest.Name;
        
        await _context.SaveChangesAsync();
        return singerToUpdate;
    }
}
