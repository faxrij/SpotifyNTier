using System.ComponentModel.DataAnnotations;
using App.Domain.Entities;
using MediatR;

namespace App.Logic.Commands.AddAlbum;

public class AddAlbumCommand : IRequest<Album>
{
    [Required]
    public string? Title { get; set; }
    
    [Required]
    public int ReleaseYear { get; set; }   
}