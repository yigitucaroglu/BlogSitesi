using BlogProject.Models;
using BlogProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly ApplicationDbContext _context;

        public LikeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Like> GetUserLikeAsync(int blogId, string userId)
        {
            return await _context.Likes
                .FirstOrDefaultAsync(l => l.BlogId == blogId && l.UserId == userId);
        }

        public async Task AddLikeAsync(Like like)
        {
            _context.Likes.Add(like);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLikeAsync(Like like)
        {
            _context.Likes.Update(like);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetLikeCountAsync(int blogId, bool isLiked)
        {
            return await _context.Likes
                .CountAsync(l => l.BlogId == blogId && l.IsLiked == isLiked);
        }
    }
}
