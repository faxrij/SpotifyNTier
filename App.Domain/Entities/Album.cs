using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace App.Entities;

public class Album : Auditable
{
    public string Title { get; set; }
    public int ReleaseYear { get; set; }
    
    [JsonIgnore]
    public ICollection<Song> Songs { get; set; }
}