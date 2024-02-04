using System.ComponentModel.DataAnnotations;

namespace App.DataTransferObjects.Request;

public class UpdateCategoryRequest
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public int ParentCategoryId { get; set; }
}