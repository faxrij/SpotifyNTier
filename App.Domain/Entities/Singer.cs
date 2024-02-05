using System;
using System.Collections.Generic;

namespace App.Domain.Entities
{
    public class Singer : Auditable
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    
        public ICollection<Album> Albums { get; set; }
    }
}