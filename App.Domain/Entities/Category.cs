using System;
using System.Collections.Generic;

namespace App.Domain.Entities
{
    public class Category : Auditable
    {
        public string Name { get; set; }
        public Category ParentCategory { get; set; }
        public Boolean isParentCategory { get; set; }
        public ICollection<Song> Songs { get; set; }
    }
}