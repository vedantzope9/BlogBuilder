using BlogBuilder.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlogBuilder.BusinessLayer.Interfaces
{
    public interface IBlogServices
    {
        List<BlogDTO> GetAllBlogs();
        BlogDTO? GetBlogById(int id);

        BlogDTO CreateBlog(BlogDTO blogDTO);

        BlogDTO UpdateBlog(BlogDTO blogDTO);
        bool DeleteBlog(int blogId);

    }
}
