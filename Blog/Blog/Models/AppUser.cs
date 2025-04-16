using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
namespace BlogProject.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<BlogPost> Blogs { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        // Profil resmi yolu
        public string? ProfileImageUrl { get; set; }
        public string? X { get; set; }
        public string? LinkedIn { get; set; }
        public string? Instagram { get; set; }
       

    }
}
