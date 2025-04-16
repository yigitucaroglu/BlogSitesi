using BlogProject.Models;

namespace BlogProject.Repositories.Interfaces
{
    public interface IBlogRepository
    {
        Task<List<BlogPost>> GetAllAsync();
        Task<BlogPost> GetByIdAsync(int id);
        Task<List<BlogPost>> GetByUserIdAsync(string userId);
        Task<BlogPost> GetBlogWithDetailsAsync(int id);

        Task<List<BlogPost>> SearchAsync(string query);
        Task<List<BlogPost>> GetByCategoryAsync(int categoryId);

        Task<List<BlogPost>> GetTopViewedBlogsAsync(int count);


        Task AddAsync(BlogPost blog);
        Task UpdateAsync(BlogPost blog);
        Task DeleteAsync(BlogPost blog);
    }
}
