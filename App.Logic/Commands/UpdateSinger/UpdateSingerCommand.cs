using System.ComponentModel.DataAnnotations;
using App.Domain.Entities;
using MediatR;

namespace App.Logic.Commands.UpdateSinger;

public class UpdateSingerCommand : IRequest<Singer>
{
    [Required]
    public int Id { get; set; } 
    
    [Required]
    public string? Name { get; set; }
    
    [Required]
    public DateTime BirthDate { get; set; }
}