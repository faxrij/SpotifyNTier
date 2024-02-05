using App.Domain.Entities;
using App.Logic.Interfaces;
using MediatR;

namespace App.Logic.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Category>
{
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Category> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        int id = request.Id;
        var category = await _categoryRepository.UpdateCategoryAsync(request, id);
        return category;    
    }
}