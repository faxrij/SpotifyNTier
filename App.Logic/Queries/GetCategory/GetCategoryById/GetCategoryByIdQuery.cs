using App.Domain.Entities;
using MediatR;

namespace App.Logic.Queries.GetCategory.GetCategoryById;

public class GetCategoryByIdQuery : IRequest<Category>
{
    public int? Id { get; set; }
}