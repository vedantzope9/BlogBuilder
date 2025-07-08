using BlogBuilder.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlogBuilder.BusinessLayer.Interfaces
{
    public interface IUserServices
    {
        JsonResult RegisterUser(UserDTO dto);
    }
}
