using System.ComponentModel.DataAnnotations;

namespace App.Logic.DataTransferObjects.Request;

public class CreateSongRequest
{
    [Required]
    public string Title { get; set; }
    
    [Required]
    public int DurationInSeconds { get; set; }
    
    [Required]
    public string Lyrics { get; set; }
    
    [Required]
    public int AlbumId { get; set; }
}