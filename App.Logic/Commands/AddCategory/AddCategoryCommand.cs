using System.ComponentModel.DataAnnotations;
using App.Domain.Entities;
using MediatR;

namespace App.Logic.Commands.AddCategory;

public class AddCategoryCommand : IRequest<Category>
{
    [Required]
    public string? Name { get; set; }
    
    [Required]
    public int ParentCategoryId { get; set; }
}