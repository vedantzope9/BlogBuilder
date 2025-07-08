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

        public TestingController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost("register")]
        public JsonResult RegisterUser(UserDTO dto)
        {
            return _userServices.RegisterUser(dto);
        }
    }
}
