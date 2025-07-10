using BlogBuilder.DTOs;
using BlogBuilder.Models;
using BlogBuilder.RepositoryLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogBuilder.RepositoryLayer.Repository
{
    public class BlogRepo:IBlogRepo
    {
        private readonly BLOG_PROJECTContext _context;
        public BlogRepo(BLOG_PROJECTContext context)
        {
            _context = context;
        }

        public List<BlogDTO> GetAllBlogs()
        {
            return _context.BLOG
                .Include(b => b.BLOG_COMMENTS)
                .Select(b => new BlogDTO
                {
                    BLOGID = b.BLOGID,
                    USERID = b.USERID,
                    TOPIC_NAME = b.TOPIC_NAME,
                    BLOG_CONTENT = b.BLOG_CONTENT,
                    IMAGE_DATA = b.IMAGE_DATA,
                    MODIFIED_DATE = b.MODIFIED_DATE,
                    isUpdated=b.isUpdated,

                    BLOG_COMMENTS = b.BLOG_COMMENTS.Select(c => new CommentsDTO
                    {
                        COMMENTID = c.COMMENTID,
                        BLOGID = c.BLOGID,
                        USERID = c.USERID,
                        COMMENT = c.COMMENT

                    }).ToList()

                }).ToList();
        }

        public BlogDTO? GetBlogById(int id)
        {
            var entity = _context.BLOG
                .Include(b => b.BLOG_COMMENTS)
                .FirstOrDefault(b=> b.BLOGID == id);

            if (entity == null)
                return null;

            return new BlogDTO
            {
                BLOGID = entity.BLOGID,
                USERID = entity.USERID,
                TOPIC_NAME = entity.TOPIC_NAME,
                BLOG_CONTENT = entity.BLOG_CONTENT,
                IMAGE_DATA = entity.IMAGE_DATA,
                MODIFIED_DATE = entity.MODIFIED_DATE,
                isUpdated=entity.isUpdated,

                BLOG_COMMENTS = entity.BLOG_COMMENTS.Select(c => new CommentsDTO
                {
                    COMMENTID = c.COMMENTID,
                    BLOGID = c.BLOGID,
                    USERID = c.USERID,
                    COMMENT = c.COMMENT

                }).ToList()
            };
        }
    }
}
