using System.ComponentModel.DataAnnotations;

namespace App.Logic.DataTransferObjects.Request;

public class CreateSingerRequest
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public DateTime BirthDate { get; set; }
}