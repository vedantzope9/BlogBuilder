using BlogBuilder.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlogBuilder.BusinessLayer.Interfaces
{
    public interface IUserServices
    {
        JsonResult RegisterUser(UserDTO dto);
        JsonResult LoginUser(string email, string password);
    }
}
