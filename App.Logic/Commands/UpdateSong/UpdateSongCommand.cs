using System.ComponentModel.DataAnnotations;
using App.Domain.Entities;
using MediatR;

namespace App.Logic.Commands.UpdateSong;

public class UpdateSongCommand : IRequest<Song>
{
    [Required]
    public int Id { get; set; } 
    
    [Required]
    public string? Title { get; set; }
    
    [Required]
    public int DurationInSeconds { get; set; }
    
    [Required]
    public string? Lyrics { get; set; }
    
    [Required]
    public int AlbumId { get; set; }
}