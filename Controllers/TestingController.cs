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
        public JsonResult RegisterUser(UserDTO dto)
        {
            return _userServices.RegisterUser(dto);
        }

        [HttpPost("login")]
        public JsonResult LoginUser(string email, string password)
        {
            return _userServices.LoginUser(email, password);
        }

        [HttpGet("GetAllBlogs")]
        public List<BlogDTO> GetAllBlogs()
        {
            return _blogServices.GetAllBlogs();
        }

        [HttpGet("GetBlogById")]
        public ActionResult GetBlogById(int id)
        {
            var blog = _blogServices.GetBlogById(id);
            if (blog == null)
            {
                return NotFound();
            }

            return Ok(blog);
        }

        [HttpPost("CreateBlog")]
        public ActionResult CreateBlog(BlogDTO blogDTO , IFormFile image)
        {
            return Ok(_blogServices.CreateBlog(blogDTO , image));
        }

        [HttpPost("UpdateBlog")]
        public ActionResult UpdateBlog(BlogDTO blogDTO)
        {
            return Ok(_blogServices.UpdateBlog(blogDTO));
        }

        [HttpDelete("DeleteBlog")]
        public ActionResult DeleteBlog(int blogId)
        {
            return Ok(_blogServices.DeleteBlog(blogId));
        }

        [HttpPost("AddComment")]
        public ActionResult AddComment(CommentsDTO commentsDTO)
        {
            return Ok(_commentsServices.AddComment(commentsDTO));
        }

        [HttpPost("UpdateComment")]
        public ActionResult UpdateComment(CommentsDTO commentsDTO)
        {
            return Ok(_commentsServices.UpdateComment(commentsDTO));
        }

        [HttpDelete("DeleteComment")]
        public ActionResult DeleteComment(int commentId)
        {
            _commentsServices.DeleteComment(commentId);
            return Ok("Comment Deleted!");
        }
    }
}
