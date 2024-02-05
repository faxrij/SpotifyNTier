using App.Domain.Entities;
using App.Logic.Interfaces;
using MediatR;

namespace App.Logic.Commands.AddCategory;

public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, Category>
{
    private readonly ICategoryRepository _categoryRepository;

    public AddCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Category> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {
        var album = await _categoryRepository.CreateCategoryAsync(request);
        return album;
    }
}