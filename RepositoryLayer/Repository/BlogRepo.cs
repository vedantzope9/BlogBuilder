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

        public async Task<List<BLOG>> GetAllBlogs()
        {
            return await _context.BLOG
                .Include(b => b.BLOG_COMMENTS)
                .ToListAsync();
        }

        public async Task<BLOG?> GetBlogById(int id)
        {
            var entity = _context.BLOG
                .Include(b => b.BLOG_COMMENTS)
                .FirstOrDefaultAsync(b=> b.BLOGID == id);

            if (entity == null)
                return null;

            return await entity;
        }

        public async Task CreateBlog(BLOG blog)
        {
            _context.BLOG.Add(blog);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateBlog(BlogDTO updatedBlog)
        {
            var existingBlog = await _context.BLOG
                .Include(b => b.BLOG_COMMENTS)
                .FirstOrDefaultAsync(b => b.BLOGID == updatedBlog.BLOGID);


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
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBlog(int blogId)
        {
            var entity = await GetBlogById(blogId);

            if (entity == null)
                return false;

            //Altered constraint Foreign Key 
            //Added ON DELETE CASCADE 

            _context.BLOG.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<BLOG>? > GetBlogsByUserId(int userId)
        {
            return await _context.BLOG
                    .Where(b => b.USERID == userId)
                    .ToListAsync();
        }

        public async Task<List<BLOG>?> GetBlogsByCategory(string category)
        {
            return await _context.BLOG
                    .Where(b => b.TOPIC_NAME.Equals(category))
                    .ToListAsync();
        }

        public async Task<List<BLOG>?> SearchBlogs(string query)
        {
            if (string.IsNullOrEmpty(query))
                return null;

            return await _context.BLOG
                    .Where(b =>
                        b.BLOG_NAME.Contains(query) ||
                        b.TOPIC_NAME.Contains(query) ||
                        b.BLOG_CONTENT.Contains(query))
                    .OrderByDescending(b => b.MODIFIED_DATE)
                    .Take(5)
                    .ToListAsync();
        }
    }
}
