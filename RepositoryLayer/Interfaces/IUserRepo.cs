using BlogBuilder.Models;

namespace BlogBuilder.RepositoryLayer.Interfaces
{
    public interface IUserRepo
    {
        bool RegisterUser(BLOG_USER user);
        BLOG_USER? FindUserByEmail(string email);
        BLOG_USER? FindUserById(int userId);
    }
}
