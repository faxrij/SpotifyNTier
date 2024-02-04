using System;
using System.ComponentModel.DataAnnotations;

namespace App.DataTransferObjects.Request;

public class UpdateSingerRequest
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public DateTime BirthDate { get; set; }
}