using BlogProject.Models;

namespace BlogProject.Repositories.Interfaces
{
    public interface ILikeRepository
    {
        Task<Like> GetUserLikeAsync(int blogId, string userId);
        Task AddLikeAsync(Like like);
        Task UpdateLikeAsync(Like like);
        Task<int> GetLikeCountAsync(int blogId, bool isLiked);
    }
}
