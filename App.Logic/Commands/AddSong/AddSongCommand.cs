using System.ComponentModel.DataAnnotations;
using App.Domain.Entities;
using MediatR;

namespace App.Logic.Commands.AddSong;

public class AddSongCommand : IRequest<Song>
{
    [Required]
    public string? Title { get; set; }
    
    [Required]
    public int DurationInSeconds { get; set; }
    
    [Required]
    public string? Lyrics { get; set; }
    
    [Required]
    public int AlbumId { get; set; }
}