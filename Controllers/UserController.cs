using BlogBuilder.BusinessLayer.Interfaces;
using BlogBuilder.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlogBuilder.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult LoginUser()
        {
            return View();
        }

        [HttpGet] 
        public string? GetUsernameByUserId(int userId)
        {
            return _userServices.GetUsernameByUserId(userId);
        }

        [HttpPost]
        public JsonResult LoginUser(string email, string password)
        {
            return _userServices.LoginUser(email, password);
        }

        public IActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public JsonResult RegisterUser(UserDTO dto)
        {   
            return _userServices.RegisterUser(dto);
        }

        [HttpGet]
        public IActionResult GetAllUsernames()
        {
            return Ok(_userServices.GetAllUsernames());
        }
    }
}
