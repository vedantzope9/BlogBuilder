using BlogBuilder.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlogBuilder.BusinessLayer.Interfaces
{
    public interface IUserServices
    {
        Task<JsonResult> RegisterUser(UserDTO dto);
        Task<JsonResult> LoginUser(string email, string password);
        Task<string?> GetUsernameByUserId(int userId);
        Task<Dictionary<int, string>> GetAllUsernames();
    }
}
