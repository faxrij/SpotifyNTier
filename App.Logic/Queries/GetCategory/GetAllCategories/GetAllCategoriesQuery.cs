using App.Domain.Entities;
using MediatR;

namespace App.Logic.Queries.GetCategory.GetAllCategories;

public class GetAllCategoriesQuery : IRequest<List<Category>>
{
}