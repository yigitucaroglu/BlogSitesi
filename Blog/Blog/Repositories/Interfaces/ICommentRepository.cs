using BlogProject.Models;

namespace BlogProject.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task AddAsync(Comment comment);
        Task<List<Comment>> GetCommentsByBlogIdAsync(int blogId);
        Task<List<Comment>> GetCommentsByBlogIdWithRepliesAsync(int blogId);
        Task AddReplyAsync(Comment reply);
        Task<Comment> GetByIdAsync(int id);
        Task UpdateAsync(Comment comment);
        Task DeleteAsync(Comment comment);

    }
}
