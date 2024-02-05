using App.Entities;
using App.Infrastructure.Contexts;
using App.Logic.DataTransferObjects.Request;
using App.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Logic.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly DataBaseContext _context;

    public CategoryRepository(DataBaseContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category?> GetCategoryByIdAsync(int id)
    {
        return await _context.Categories. 
            Include(c => c.ParentCategory)
            .Include(c => c.Songs)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Category> CreateCategoryAsync(CreateCategoryRequest createCategoryRequest)
    {
        Category category = new Category();
        var parentCategory = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == createCategoryRequest.ParentCategoryId);
        
        if (parentCategory == null)
        {
            throw new InvalidOperationException($"Parent category with ID {createCategoryRequest.ParentCategoryId} not found.");
        }

        category.isParentCategory = false;
        category.Songs = new List<Song>();
        category.Name = createCategoryRequest.Name;
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<bool> RemoveCategoryAsync(int id)
    {
        var categoryToRemove = await _context.Categories.FindAsync(id);

        if (categoryToRemove == null)
        {
            return false;
        }

        _context.Categories.Remove(categoryToRemove);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Category?> UpdateCategoryAsync(UpdateCategoryRequest updateCategoryRequest, int id)
    {
        var categoryToUpdate = await _context.Categories.FindAsync(id);
        if (categoryToUpdate == null)
        {
            throw new InvalidOperationException($"Provided category with ID {id} not found.");
        }

        var parentCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == updateCategoryRequest.ParentCategoryId);

        if (parentCategory == null)
        {
            throw new InvalidOperationException($"Parent category with ID {id} not found.");
        }

        categoryToUpdate.ParentCategory = parentCategory;
        categoryToUpdate.Name = updateCategoryRequest.Name;
        
        await _context.SaveChangesAsync();
        return categoryToUpdate;
    }
}
