using App.Domain.Entities;
using App.Logic.Commands.AddCategory;
using App.Logic.Commands.DeleteCategory;
using App.Logic.Commands.UpdateCategory;
using App.Logic.Queries.GetCategory.GetAllCategories;
using App.Logic.Queries.GetCategory.GetCategoryById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Category>>> GetCategories()
    {
        return await mediator.Send(new GetAllCategoriesQuery());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategory(int id)
    {
        var getCategoryByIdQuery = new GetCategoryByIdQuery() { Id = id };
        return await mediator.Send(getCategoryByIdQuery);
    }

    [HttpPost]
    public async Task<ActionResult<Category>> CreateCategory(AddCategoryCommand addCategoryCommand)
    {
        return await mediator.Send(addCategoryCommand);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<Boolean>> DeleteAlbum(int id)
    {
        var deleteAlbumCommand = new DeleteCategoryCommand() { Id = id };
        return await mediator.Send(deleteAlbumCommand);
    }
    
    [HttpPut]
    public async Task<ActionResult<Category>> UpdateCategory(UpdateCategoryCommand updateCategoryCommand)
    {
        return await mediator.Send(updateCategoryCommand);
    }
}