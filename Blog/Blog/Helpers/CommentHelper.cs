using BlogProject.Models;

namespace BlogProject.Helpers
{
    public static class CommentHelper
    {
        public static List<Comment> BuildCommentTree(List<Comment> comments)
        {
            var lookup = comments.ToLookup(c => c.ParentCommentId);

            foreach (var comment in comments)
            {
                comment.Replies = lookup[comment.Id].ToList();
            }

            return lookup[null].ToList(); // Root yorumları döndür
        }
    }
}
