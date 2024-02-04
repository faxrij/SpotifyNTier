using App.DataTransferObjects.Request;
using App.Entities;

namespace App.Logic.SampleLogic.Services;

public interface ICategoryService
{
    Task<List<Category>> GetAllCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(int id);
    Task<Category> CreateCategoryAsync(CreateCategoryRequest createCategoryRequest);
    Task<bool> RemoveCategoryAsync(int id);
    Task<Category?> UpdateCategoryAsync(UpdateCategoryRequest updateCategoryRequest, int id);
}
