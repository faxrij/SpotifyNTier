using System.Collections.Generic;

namespace App.Domain.Entities
{
    public class Song : Auditable
    {
        public string Title { get; set; }
        public int DurationInSeconds { get; set; }
        public string Lyrics { get; set; }
        public ICollection<Category> Categories { get; set; }
        public Album Album { get; set; }
    }
}