using BlogBuilder.DTOs;
using BlogBuilder.Models;

namespace BlogBuilder.RepositoryLayer.Interfaces
{
    public interface IBlogRepo
    {
        List<BLOG> GetAllBlogs();
        BLOG? GetBlogById(int id);

        void CreateBlog(BLOG blog);

        bool UpdateBlog(BlogDTO blog);
        bool DeleteBlog(int blogId);
    }
}
