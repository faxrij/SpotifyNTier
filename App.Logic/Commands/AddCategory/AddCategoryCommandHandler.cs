using App.Domain.Entities;
using App.Logic.Interfaces;
using MediatR;

namespace App.Logic.Commands.AddCategory;

public class AddCategoryCommandHandler(ICategoryRepository categoryRepository)
    : IRequestHandler<AddCategoryCommand, Category>
{
    public async Task<Category> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {
        var album = await categoryRepository.CreateCategoryAsync(request);
        return album;
    }
}