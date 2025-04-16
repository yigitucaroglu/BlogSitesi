using System.ComponentModel.DataAnnotations;
using System;
namespace BlogProject.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Yorum içeriği boş bırakılamaz")]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; }

        public int BlogId { get; set; }
        public BlogPost Blog { get; set; }

        public int? ParentCommentId { get; set; }  // 🔥 Cevap özelliği için
        public Comment ParentComment { get; set; }
        public DateTime? UpdatedAt { get; set; }


        public List<Comment>? Replies { get; set; }
    }
}
