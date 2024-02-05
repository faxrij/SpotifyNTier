using System.ComponentModel.DataAnnotations;
using App.Domain.Entities;
using MediatR;

namespace App.Logic.Commands.AddSinger;

public class AddSingerCommand : IRequest<Singer>
{
    [Required]
    public string? Name { get; set; }
    
    [Required]
    public DateTime BirthDate { get; set; }
}