using BlogBuilder.BusinessLayer.Interfaces;
using BlogBuilder.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogBuilder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestingController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IBlogServices _blogServices;
        private readonly ICommentsServices _commentsServices;

        public TestingController(IUserServices userServices , IBlogServices blogServices , ICommentsServices commentsServices)
        {
            _userServices = userServices;
            _blogServices = blogServices;
            _commentsServices = commentsServices;
        }

        [HttpPost("register")]
        public async Task<JsonResult> RegisterUser(UserDTO dto)
        {
            return await _userServices.RegisterUser(dto);
        }

        [HttpPost("login")]
        public async Task<JsonResult> LoginUser(string email, string password)
        {
            return await _userServices.LoginUser(email, password);
        }

        [HttpGet("GetAllBlogs")]
        public async Task<List<BlogDTO>> GetAllBlogs()
        {
            return await _blogServices.GetAllBlogs();
        }

        [HttpGet("GetBlogById")]
        public async Task<ActionResult> GetBlogById(int id)
        {
            var blog =await _blogServices.GetBlogById(id);
            if (blog == null)
            {
                return NotFound();
            }

            return Ok(blog);
        }

        [HttpPost("CreateBlog")]
        public async Task<ActionResult> CreateBlog(BlogDTO blogDTO)
        {
            await _blogServices.CreateBlog(blogDTO);
            return Ok("Blog Created Successfully");
        }

        [HttpPost("UpdateBlog")]
        public async Task<ActionResult> UpdateBlog(BlogDTO blogDTO)
        {
            return Ok(await _blogServices.UpdateBlog(blogDTO));
        }

        [HttpDelete("DeleteBlog")]
        public async Task<ActionResult> DeleteBlog(int blogId)
        {
            return Ok(await _blogServices.DeleteBlog(blogId));
        }

        [HttpPost("AddComment")]
        public async Task<ActionResult> AddComment(CommentsDTO commentsDTO)
        {
            return Ok(await _commentsServices.AddComment(commentsDTO));
        }

        [HttpPost("UpdateComment")]
        public async Task<ActionResult> UpdateComment(CommentsDTO commentsDTO)
        {
            return Ok(await _commentsServices.UpdateComment(commentsDTO));
        }

        [HttpDelete("DeleteComment")]
        public async Task<ActionResult> DeleteComment(int commentId)
        {
            await _commentsServices.DeleteComment(commentId);
            return Ok("Comment Deleted!");
        }

        [HttpGet("Blogs/GetBlogsByCategory/{category}")]
        public async Task<List<BlogDTO>?> GetBlogsByCategory(string category)
        {
            return await _blogServices.GetBlogsByCategory(category);
        }

        [HttpGet("/Blog/SearchBlogs")]
        public async Task<List<BlogDTO>?> SearchBlogs(string query)
        {
            return await _blogServices.SearchBlogs(query);
        }
    }
}
