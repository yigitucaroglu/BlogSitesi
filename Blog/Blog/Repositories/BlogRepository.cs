
using BlogProject.Models;
using BlogProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly ApplicationDbContext _context;

        public BlogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BlogPost>> GetAllAsync()
        {
            return await _context.Blogs.Include(b => b.Category).Include(b => b.User).ToListAsync();
        }

        public async Task<BlogPost> GetByIdAsync(int id)
        {
            return await _context.Blogs
            .Include(b => b.User)
            .Include(b => b.Category)
            .Include(b => b.Likes) 
            .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<BlogPost>> GetByUserIdAsync(string userId)
        {
            return await _context.Blogs
                .Where(b => b.UserId == userId)
                .Include(b => b.Category)
                .Include(b => b.User)
                .ToListAsync();
        }
        public async Task<BlogPost> GetBlogWithDetailsAsync(int id)
        {
            return await _context.Blogs
                .Include(b => b.User)
                .Include(b => b.Category)
                .Include(b => b.Comments)
                    .ThenInclude(c => c.User)
                .Include(b => b.Likes)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
        public async Task<List<BlogPost>> SearchAsync(string query)
        {
            return await _context.Blogs
                .Include(b => b.User)
                .Include(b => b.Category)
                .Where(b =>
                    b.Title.Contains(query) ||
                    b.Content.Contains(query) ||
                    b.Category.Name.Contains(query) ||
                    b.User.UserName.Contains(query))
                .ToListAsync();
        }
        public async Task<List<BlogPost>> GetByCategoryAsync(int categoryId)
        {
            return await _context.Blogs
                .Include(b => b.User)
                .Include(b => b.Category)
                .Where(b => b.CategoryId == categoryId)
                .ToListAsync();
        }


        public async Task AddAsync(BlogPost blog)
        {
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BlogPost blog)
        {
            _context.Blogs.Update(blog);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(BlogPost blog)
        {
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
        }
        public async Task<List<BlogPost>> GetTopViewedBlogsAsync(int count)
        {
            return await _context.Blogs
            .Where(b => b.ViewCount > 0)
            .OrderByDescending(b => b.ViewCount)
            .Take(count)
            .Include(b => b.Category)
            .ToListAsync();
        }

    }
}
