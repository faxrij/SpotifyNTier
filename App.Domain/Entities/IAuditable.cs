using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Entities;

public abstract class Auditable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}
