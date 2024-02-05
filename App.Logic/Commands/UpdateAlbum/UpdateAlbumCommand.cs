using System.ComponentModel.DataAnnotations;
using App.Domain.Entities;
using MediatR;

namespace App.Logic.Commands.UpdateAlbum;

public class UpdateAlbumCommand : IRequest<Album>
{
    [Required]
    public int Id { get; set; } 
    
    [Required]
    public string? Title { get; set; }
    
    [Required]
    public int ReleaseYear { get; set; }   
}