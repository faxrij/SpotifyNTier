using System.Collections.Generic;

namespace App.Domain.Entities
{
    public class Album : Auditable
    {
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public ICollection<Song> Songs { get; set; }
    }
}