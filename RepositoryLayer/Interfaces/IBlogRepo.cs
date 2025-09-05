using BlogBuilder.DTOs;
using BlogBuilder.Models;

namespace BlogBuilder.RepositoryLayer.Interfaces
{
    public interface IBlogRepo
    {
        Task<List<BLOG>> GetAllBlogs(int pageNumber);
        Task<BLOG?> GetBlogById(int id);

        Task CreateBlog(BLOG blog);

        Task<bool> UpdateBlog(BlogDTO blog);
        Task<bool> DeleteBlog(int blogId);

        Task<List<BLOG>? > GetBlogsByUserId(int userId);

        Task<List<BLOG>?> GetBlogsByCategory(string category);

        Task<List<BLOG>?> SearchBlogs(string query);
    }
}
