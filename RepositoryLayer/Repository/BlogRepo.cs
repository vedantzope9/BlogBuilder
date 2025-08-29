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

        public async Task CreateBlog(BLOG blog)
        {
            _context.BLOG.Add(blog);
            await _context.SaveChangesAsync();
        }

        public bool UpdateBlog(BlogDTO updatedBlog)
        {
            var existingBlog = _context.BLOG
                .Include(b => b.BLOG_COMMENTS)
                .FirstOrDefault(b => b.BLOGID == updatedBlog.BLOGID);


            if (existingBlog == null)
                return false;

            existingBlog.BLOG_CONTENT = updatedBlog.BLOG_CONTENT;
            existingBlog.isUpdated = true;
            existingBlog.BLOG_NAME = updatedBlog.BLOG_NAME;
            existingBlog.TOPIC_NAME = updatedBlog.TOPIC_NAME;
            existingBlog.IMAGE_DATA = updatedBlog.IMAGE_DATA;
            existingBlog.MODIFIED_DATE = DateOnly.FromDateTime(DateTime.Now);

            foreach (var updatedComment in updatedBlog.BLOG_COMMENTS)
            {
                var existingComment = existingBlog.BLOG_COMMENTS
                    .FirstOrDefault(c => c.COMMENTID == updatedComment.COMMENTID);

                if (existingComment != null)
                {
                    existingComment.COMMENT = updatedComment.COMMENT;
                }
                else
                {
                    // Add new comment if it doesn't exist
                    existingBlog.BLOG_COMMENTS.Add(new BLOG_COMMENTS
                    {
                        BLOGID = updatedBlog.BLOGID,
                        COMMENTID = updatedComment.COMMENTID,
                        USERID = updatedComment.USERID,
                        COMMENT = updatedComment.COMMENT,
                        MODIFIED_DATE = DateOnly.FromDateTime(DateTime.Now)
                    });
                }
            }

            _context.SaveChanges();
            return true;
        }

        public bool DeleteBlog(int blogId)
        {
            var entity = GetBlogById(blogId);

            if (entity == null)
                return false;

            foreach(var comments in entity.BLOG_COMMENTS)
            {
                _context.BLOG_COMMENTS.Remove(comments);
            }

            _context.BLOG.Remove(entity);
            _context.SaveChanges();
            return true;
        }

        public async Task<List<BLOG>? > GetBlogsByUserId(int userId)
        {
            return await _context.BLOG
                    .Where(b => b.USERID == userId)
                    .ToListAsync();
        }
    }
}
