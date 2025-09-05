using BlogBuilder.BusinessLayer.Interfaces;
using BlogBuilder.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogBuilder.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogServices _blogServices;

        public BlogController(IBlogServices blogServices)
        {
            _blogServices = blogServices;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var list = await _blogServices.GetAllBlogs(pageNumber);
            return View(list);
        }

        [AllowAnonymous]
        [HttpGet("/Blog/GetBlogById/{id}")]
        public async Task<IActionResult> GetBlogById(int id)
        {
            var blog = await _blogServices.GetBlogById(id);
            if (blog == null)
            {
                return View("NotFound");
            }

            return View(blog);
        }


        [HttpGet("/Blog/CreateBlog")]
        public IActionResult CreateBlog()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateBlog([FromForm] BlogDTO blogDTO)
        {
            await _blogServices.CreateBlog(blogDTO );
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateBlog(BlogDTO blogDTO)
        {
            await _blogServices.UpdateBlog(blogDTO);
            return View();
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteBlog(int blogId)
        {
            await _blogServices.DeleteBlog(blogId);
            return View();
        }

        [Authorize]
        [HttpGet("/Blog/GetBlogsByUserId/{userId}")]
        public async Task<IActionResult> GetBlogsByUserId(int userId)
        {
            List<BlogDTO> ? list = await _blogServices.GetBlogsbyUserId(userId);

            return View(list);
        }

        [HttpGet("/Blog/GetBlogsByCategory/{category}")]
        public async Task<IActionResult> GetBlogsByCategory(string category)
        {
            var blogs = await _blogServices.GetBlogsByCategory(category);

            return View(blogs);
        }

        [HttpGet("/Blog/SearchBlogs/{query}")]
        public async Task<IActionResult> SearchBlogs(string query)
        {
            var blogs = await _blogServices.SearchBlogs(query);

            return Ok(blogs.Select(b => new {
                b.BLOGID,
                b.BLOG_NAME,
                b.TOPIC_NAME
            }));
        }
    }
}
