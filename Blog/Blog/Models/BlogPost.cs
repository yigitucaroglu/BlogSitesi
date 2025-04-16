using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using System.Collections.Generic;
using BlogProject.Models;

namespace BlogProject.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlık zorunludur")]
        [StringLength(100)]
        public string Title { get; set; }

        [Required(ErrorMessage = "İçerik boş bırakılamaz")]
        public string Content { get; set; }

        public DateTime PublishedDate { get; set; }

        public string ImageUrl { get; set; }

        // Foreign Key - Category
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        // Foreign Key - AppUser
        public string UserId { get; set; }
        public AppUser User { get; set; }

        // Yorumlar
        public ICollection<Comment> Comments { get; set; }

        public override string ToString()
        {
            return $"Başlık: {Title}, İçerik: {Content}, KullanıcıId: {UserId}";
        }

        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public int ViewCount { get; set; } = 0;
    }
}
