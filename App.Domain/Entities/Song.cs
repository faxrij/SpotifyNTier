using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace App.Entities;

public class Song : Auditable
{
    public string Title { get; set; }
    public int DurationInSeconds { get; set; }
    public string Lyrics { get; set; }
    
    [JsonIgnore]
    public ICollection<Category> Categories { get; set; }
    
    [JsonIgnore]
    public Album Album { get; set; }
}