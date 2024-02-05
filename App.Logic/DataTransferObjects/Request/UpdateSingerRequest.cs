using System.ComponentModel.DataAnnotations;

namespace App.Logic.DataTransferObjects.Request;

public class UpdateSingerRequest
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public DateTime BirthDate { get; set; }
}