using BlogBuilder.BusinessLayer.Interfaces;
using BlogBuilder.DTOs;
using BlogBuilder.Models;
using BlogBuilder.RepositoryLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

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
                return _blogRepo.GetAllBlogs()
                .Select(b => new BlogDTO
                {
                    BLOGID = b.BLOGID,
                    USERID = b.USERID,
                    BLOG_NAME=b.BLOG_NAME,
                    TOPIC_NAME = b.TOPIC_NAME,
                    BLOG_CONTENT = b.BLOG_CONTENT,
                    IMAGE_DATA = b.IMAGE_DATA,
                    MODIFIED_DATE = b.MODIFIED_DATE,
                    isUpdated = b.isUpdated,

                    BLOG_COMMENTS = b.BLOG_COMMENTS.Select(c => new CommentsDTO
                    {
                        COMMENTID = c.COMMENTID,
                        BLOGID = c.BLOGID,
                        USERID = c.USERID,
                        COMMENT = c.COMMENT,
                        MODIFIED_DATE=c.MODIFIED_DATE

                    }).ToList()

                }).ToList();
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
                var entity = _blogRepo.GetBlogById(id);

                if (entity == null)
                    return null;

                return new BlogDTO
                {
                    BLOGID = entity.BLOGID,
                    USERID = entity.USERID,
                    BLOG_NAME=entity.BLOG_NAME,
                    TOPIC_NAME = entity.TOPIC_NAME,
                    BLOG_CONTENT = entity.BLOG_CONTENT,
                    IMAGE_DATA = entity.IMAGE_DATA,
                    MODIFIED_DATE = entity.MODIFIED_DATE,
                    isUpdated = entity.isUpdated,

                    BLOG_COMMENTS = entity.BLOG_COMMENTS.Select(c => new CommentsDTO
                    {
                        COMMENTID = c.COMMENTID,
                        BLOGID = c.BLOGID,
                        USERID = c.USERID,
                        COMMENT = c.COMMENT,
                        MODIFIED_DATE=c.MODIFIED_DATE

                    }).ToList()
                };
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task CreateBlog(BlogDTO blogDTO )
        {
            byte[] img = null;

            if (blogDTO.image != null && blogDTO.image.Length>0)
            {
                using var ms = new MemoryStream();
                await blogDTO.image.CopyToAsync(ms);
                img = ms.ToArray();
            }

            try
            {
                BLOG blog = new BLOG
                {
                    BLOGID = blogDTO.BLOGID,
                    USERID = blogDTO.USERID,
                    BLOG_NAME = blogDTO.BLOG_NAME,
                    TOPIC_NAME = blogDTO.TOPIC_NAME,
                    BLOG_CONTENT = blogDTO.BLOG_CONTENT,
                    IMAGE_DATA = img,
                    isUpdated = false,
                    MODIFIED_DATE = DateOnly.FromDateTime(DateTime.Now)
                };

                await _blogRepo.CreateBlog(blog);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public BlogDTO UpdateBlog(BlogDTO blogDTO)
        {
            try
            {
                bool res = _blogRepo.UpdateBlog(blogDTO);

                if(!res)
                    throw new Exception("Blog not found");

                return blogDTO;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteBlog(int blogId)
        {
            try
            {
                bool res = _blogRepo.DeleteBlog(blogId);

                if (!res)
                    throw new Exception("Blog not found!");

                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
