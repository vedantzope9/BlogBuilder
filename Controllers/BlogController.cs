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
        public IActionResult Index()
        {
            var list = _blogServices.GetAllBlogs();
            return View(list);
        }

        [AllowAnonymous]
        [HttpGet("/Blog/GetBlogById/{id}")]
        public IActionResult GetBlogById(int id)
        {
            var blog = _blogServices.GetBlogById(id);
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
        public IActionResult CreateBlog(BlogDTO blogDTO , IFormFile image)
        {
            _blogServices.CreateBlog(blogDTO , image);
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        public IActionResult UpdateBlog(BlogDTO blogDTO)
        {
            _blogServices.UpdateBlog(blogDTO);
            return View();
        }

        [Authorize]
        [HttpDelete]
        public IActionResult DeleteBlog(int blogId)
        {
            _blogServices.DeleteBlog(blogId);
            return View();
        }
    }
}
