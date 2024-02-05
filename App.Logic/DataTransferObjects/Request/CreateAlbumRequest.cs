using System.ComponentModel.DataAnnotations;

namespace App.Logic.DataTransferObjects.Request;

public class CreateAlbumRequest
{
    [Required]
    public string Title { get; set; }
    
    [Required]
    public int ReleaseYear { get; set; }
}