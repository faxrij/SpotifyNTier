using MediatR;

namespace App.Logic.Commands.DeleteCategory;

public class DeleteCategoryCommand : IRequest<Boolean>
{
    public int Id { get; set; }
}
