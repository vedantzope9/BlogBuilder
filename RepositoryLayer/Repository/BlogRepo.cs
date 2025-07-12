using BlogBuilder.DTOs;
using BlogBuilder.Models;
using BlogBuilder.RepositoryLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogBuilder.RepositoryLayer.Repository
{
    public class BlogRepo : IBlogRepo
    {
        private readonly BLOG_PROJECTContext _context;
        public BlogRepo(BLOG_PROJECTContext context)
        {
            _context = context;
        }

        public List<BLOG> GetAllBlogs()
        {
            return _context.BLOG
                .Include(b => b.BLOG_COMMENTS)
                .ToList();
        }

        public BLOG? GetBlogById(int id)
        {
            var entity = _context.BLOG
                .Include(b => b.BLOG_COMMENTS)
                .FirstOrDefault(b=> b.BLOGID == id);

            if (entity == null)
                return null;

            return entity;
        }

        public void CreateBlog(BLOG blog)
        {
            _context.BLOG.Add(blog);
            _context.SaveChanges();
        }

        public bool UpdateBlog(BlogDTO updatedBlog)
        {
            var existingBlog = GetBlogById(updatedBlog.BLOGID);

            if (existingBlog == null)
                return false;

            existingBlog.BLOG_CONTENT = updatedBlog.BLOG_CONTENT;
            existingBlog.isUpdated = true;
            existingBlog.TOPIC_NAME = updatedBlog.TOPIC_NAME;
            existingBlog.IMAGE_DATA = updatedBlog.IMAGE_DATA;
            existingBlog.MODIFIED_DATE = DateOnly.FromDateTime(DateTime.Now);

            existingBlog.BLOG_COMMENTS = updatedBlog.BLOG_COMMENTS.Select(c => new BLOG_COMMENTS{
                    COMMENT=c.COMMENT
                }).ToList();

            _context.SaveChanges();
            return true;
        }

        public bool DeleteBlog(int blogId)
        {
            var entity = GetBlogById(blogId);

            if (entity == null)
                return false;

            _context.BLOG.Remove(entity);
            _context.SaveChanges();
            return true;
        }

        
    }
}
