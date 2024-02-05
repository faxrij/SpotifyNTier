using App.Domain.Entities;
using App.Logic.Commands.AddCategory;
using App.Logic.Commands.UpdateCategory;
using App.Logic.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController(ICategoryRepository categoryRepository, IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Category>>> GetCategories()
    {
        var categories = await categoryRepository.GetAllCategoriesAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategory(int id)
    {
        var category = await categoryRepository.GetCategoryByIdAsync(id);
        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult<Category>> CreateCategory(AddCategoryCommand addCategoryCommand)
    {
        return await mediator.Send(addCategoryCommand);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var result = await categoryRepository.RemoveCategoryAsync(id);
        return Ok(result);
    }
    
    [HttpPut]
    public async Task<ActionResult<Category>> UpdateCategory(UpdateCategoryCommand updateCategoryCommand)
    {
        return await mediator.Send(updateCategoryCommand);
    }
}