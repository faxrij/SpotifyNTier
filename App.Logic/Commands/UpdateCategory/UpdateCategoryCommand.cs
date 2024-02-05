using System.ComponentModel.DataAnnotations;
using App.Domain.Entities;
using MediatR;

namespace App.Logic.Commands.UpdateCategory;

public class UpdateCategoryCommand : IRequest<Category>
{
    [Required]
    public int Id { get; set; } 
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public int ParentCategoryId { get; set; }
}