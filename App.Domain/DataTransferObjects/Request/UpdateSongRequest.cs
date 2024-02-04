using System.ComponentModel.DataAnnotations;

namespace App.DataTransferObjects.Request;

public class UpdateSongRequest
{
    [Required]
    public string Title { get; set; }
    
    [Required]
    public int DurationInSeconds { get; set; }
    
    [Required]
    public String Lyrics { get; set; }
    
    [Required]
    public int AlbumId { get; set; }
}