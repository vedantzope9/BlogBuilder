using BlogBuilder.DTOs;

namespace BlogBuilder.RepositoryLayer.Interfaces
{
    public interface IBlogRepo
    {
        List<BlogDTO> GetAllBlogs();
        public BlogDTO? GetBlogById(int id);
    }
}
