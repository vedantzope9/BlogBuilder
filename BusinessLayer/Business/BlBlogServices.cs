using BlogBuilder.BusinessLayer.Interfaces;
using BlogBuilder.DTOs;
using BlogBuilder.RepositoryLayer.Interfaces;

namespace BlogBuilder.BusinessLayer.Business
{
    public class BlBlogServices : IBlogServices
    {
        private readonly IBlogRepo _blogRepo;

        public BlBlogServices(IBlogRepo blogRepo)
        {
            _blogRepo = blogRepo;
        }

        public List<BlogDTO> GetAllBlogs()
        {
            try
            {
                return _blogRepo.GetAllBlogs();
            }
            catch(Exception ex)
            {
                throw new Exception("Error in Fetching Records!");
            }
        }

        public BlogDTO? GetBlogById(int id)
        {
            try
            {
                return _blogRepo.GetBlogById(id);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
