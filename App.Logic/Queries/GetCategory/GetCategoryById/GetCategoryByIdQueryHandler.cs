using App.Domain.Entities;
using App.Logic.Interfaces;
using App.Logic.Queries.GetAlbum.GetAlbumById;
using MediatR;

namespace App.Logic.Queries.GetCategory.GetCategoryById;

public class GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetCategoryByIdQuery, Category>
{
    public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.Id == null)
        {
            throw new InvalidOperationException("Error when Searching Null id");
        }
        return await categoryRepository.GetCategoryByIdAsync(request.Id.Value);
    }
}