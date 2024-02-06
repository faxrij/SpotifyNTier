using App.Domain.Entities;
using App.Infrastructure.Contexts;
using App.Logic.Commands.AddCategory;
using App.Logic.Commands.UpdateCategory;
using App.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Repositories;

internal class CategoryRepository(DataBaseContext context) : ICategoryRepository
{
    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await context.Categories.ToListAsync();
    }

    public async Task<Category?> GetCategoryByIdAsync(int id)
    {
        return await context.Categories. 
            Include(c => c.ParentCategory)
            .Include(c => c.Songs)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Category> CreateCategoryAsync(AddCategoryCommand addCategoryCommand)
    {
        Category category = new Category();
        var parentCategory = await context.Categories
            .FirstOrDefaultAsync(c => c.Id == addCategoryCommand.ParentCategoryId);
        
        if (parentCategory == null)
        {
            throw new InvalidOperationException($"Parent category with ID {addCategoryCommand.ParentCategoryId} not found.");
        }

        category.IsParentCategory = false;
        category.Songs = new List<Song>();
        category.Name = addCategoryCommand.Name;
        context.Categories.Add(category);
        await context.SaveChangesAsync();
        return category;
    }

    public async Task<bool> RemoveCategoryAsync(int id)
    {
        var categoryToRemove = await context.Categories.FindAsync(id);

        if (categoryToRemove == null)
        {
            return false;
        }

        context.Categories.Remove(categoryToRemove);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<Category?> UpdateCategoryAsync(UpdateCategoryCommand updateCategoryCommand, int id)
    {
        var categoryToUpdate = await context.Categories.FindAsync(id);
        if (categoryToUpdate == null)
        {
            throw new InvalidOperationException($"Provided category with ID {id} not found.");
        }

        var parentCategory = await context.Categories.FirstOrDefaultAsync(c => c.Id == updateCategoryCommand.ParentCategoryId);

        if (parentCategory == null)
        {
            throw new InvalidOperationException($"Parent category with ID {id} not found.");
        }

        categoryToUpdate.ParentCategory = parentCategory;
        categoryToUpdate.Name = updateCategoryCommand.Name;
        
        await context.SaveChangesAsync();
        return categoryToUpdate;
    }
}
