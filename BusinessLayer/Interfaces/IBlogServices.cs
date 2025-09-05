using BlogBuilder.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlogBuilder.BusinessLayer.Interfaces
{
    public interface IBlogServices
    {
        Task<List<BlogDTO>> GetAllBlogs(int pageNumber);
        Task<BlogDTO?> GetBlogById(int id);

        Task CreateBlog(BlogDTO blogDTO);

        Task<BlogDTO> UpdateBlog(BlogDTO blogDTO);
        Task<bool> DeleteBlog(int blogId);

        Task<List<BlogDTO>?> GetBlogsbyUserId(int userId);

        Task<List<BlogDTO>?> GetBlogsByCategory(string category);

        Task<List<BlogDTO>?> SearchBlogs(string query);
    }
}
