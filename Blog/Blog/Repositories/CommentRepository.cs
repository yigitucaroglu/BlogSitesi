using BlogProject.Models;
using BlogProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Comment>> GetCommentsByBlogIdAsync(int blogId)
        {
            return await _context.Comments
                .Include(c => c.User)
                .Include(c => c.Replies)
                .ThenInclude(r => r.User)
                .Where(c => c.BlogId == blogId)
                .OrderBy(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Comment>> GetCommentsByBlogIdWithRepliesAsync(int blogId)
        {
            return await _context.Comments
                .Where(c => c.BlogId == blogId && c.ParentCommentId == null)
                .Include(c => c.Replies)
                .ThenInclude(r => r.User)
                .Include(c => c.User)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task AddReplyAsync(Comment reply)
        {
            _context.Comments.Add(reply);
            await _context.SaveChangesAsync();
        }
        public async Task<Comment> GetByIdAsync(int id)
        {
            return await _context.Comments
                .Include(c => c.User)
                .Include(c => c.Blog)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Comment comment)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }

    }
}
