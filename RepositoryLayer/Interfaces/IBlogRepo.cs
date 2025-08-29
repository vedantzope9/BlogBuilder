using BlogBuilder.DTOs;
using BlogBuilder.Models;

namespace BlogBuilder.RepositoryLayer.Interfaces
{
    public interface IBlogRepo
    {
        List<BLOG> GetAllBlogs();
        BLOG? GetBlogById(int id);

        Task CreateBlog(BLOG blog);

        bool UpdateBlog(BlogDTO blog);
        bool DeleteBlog(int blogId);

        Task<List<BLOG>? > GetBlogsByUserId(int userId);
    }
}
