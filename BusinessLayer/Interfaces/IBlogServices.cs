using BlogBuilder.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlogBuilder.BusinessLayer.Interfaces
{
    public interface IBlogServices
    {
        List<BlogDTO> GetAllBlogs();
        BlogDTO? GetBlogById(int id);

        Task CreateBlog(BlogDTO blogDTO, IFormFile image);

        BlogDTO UpdateBlog(BlogDTO blogDTO);
        bool DeleteBlog(int blogId);

    }
}
