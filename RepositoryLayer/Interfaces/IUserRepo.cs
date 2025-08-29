using BlogBuilder.Models;

namespace BlogBuilder.RepositoryLayer.Interfaces
{
    public interface IUserRepo
    {
        Task<bool> RegisterUser(BLOG_USER user);
        Task<BLOG_USER?> FindUserByEmail(string email);
        Task<BLOG_USER?> FindUserById(int userId);

        Task<List<BLOG_USER>> GetAllUsers();
    }
}
