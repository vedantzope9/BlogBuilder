using BlogBuilder.BusinessLayer.Interfaces;
using BlogBuilder.DTOs;
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

        public IActionResult Index()
        {
            var list = _blogServices.GetAllBlogs();
            return View(list);
        }

        [HttpGet]
        public IActionResult GetBlogById(int id)
        {
            var blog = _blogServices.GetBlogById(id);
            if (blog == null)
            {
                return View("NotFound");
            }

            return View(blog);
        }
        
    }
}
