using BlogBuilder.BusinessLayer.Interfaces;
using BlogBuilder.DTOs;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<string?> GetUsernameByUserId(int userId)
        {
            return await _userServices.GetUsernameByUserId(userId);
        }

        [HttpPost]
        public async Task<JsonResult> LoginUser(string email, string password)
        {
            return await _userServices.LoginUser(email, password);
        }

        public IActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> RegisterUser(UserDTO dto)
        {   
            return await _userServices.RegisterUser(dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsernames()
        {
            return Ok(await _userServices.GetAllUsernames());
        }
    }
}
