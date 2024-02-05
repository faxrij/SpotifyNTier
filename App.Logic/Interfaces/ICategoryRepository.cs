using App.Domain.Entities;
using App.Logic.Commands.AddCategory;
using App.Logic.Commands.UpdateCategory;

namespace App.Logic.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(int id);
    Task<Category> CreateCategoryAsync(AddCategoryCommand addCategoryCommand);
    Task<bool> RemoveCategoryAsync(int id);
    Task<Category?> UpdateCategoryAsync(UpdateCategoryCommand updateCategoryCommand, int id);
}
