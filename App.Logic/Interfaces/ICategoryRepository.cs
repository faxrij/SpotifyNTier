using App.Domain.Entities;
using App.Logic.DataTransferObjects.Request;

namespace App.Logic.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(int id);
    Task<Category> CreateCategoryAsync(CreateCategoryRequest createCategoryRequest);
    Task<bool> RemoveCategoryAsync(int id);
    Task<Category?> UpdateCategoryAsync(UpdateCategoryRequest updateCategoryRequest, int id);
}
