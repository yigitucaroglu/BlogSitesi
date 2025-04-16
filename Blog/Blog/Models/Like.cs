using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogProject.Models
{
    public class Like
    {
        public int Id { get; set; }

        public int BlogId { get; set; }
        public BlogPost Blog { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; }

        public bool IsLiked { get; set; } 

        public bool LikeCount { get; set; } 

        public bool DislikeCount { get; set; }


    }
}
