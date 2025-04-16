using BlogProject.Models;
using System.Collections.Generic;

namespace BlogProject.ViewModels
{
    public class BlogDetailsViewModel
    {
        public BlogPost Blog { get; set; }
        public bool IsLikedByCurrentUser { get; set; }
        public int TotalLikes { get; set; }
        public List<Comment> Comments { get; set; }
        public string NewComment { get; set; }

        public int LikeCount { get; set; }

        public int DislikeCount { get; set; }
        public int? ParentCommentId { get; set; } // Eğer bir yanıtsa
        public string NewCommentContent { get; set; }

        public List<CommentViewModel> CommentTree { get; set; }

    }
}
