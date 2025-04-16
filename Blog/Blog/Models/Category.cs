using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogProject.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kategori adı zorunludur")]
        public string Name { get; set; }

        // Navigation property
        public ICollection<BlogPost> Blogs { get; set; }
    }
}
