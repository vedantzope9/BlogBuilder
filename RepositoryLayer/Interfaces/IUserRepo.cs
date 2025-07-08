using BlogBuilder.Models;

namespace BlogBuilder.RepositoryLayer.Interfaces
{
    public interface IUserRepo
    {
        bool RegisterUser(BLOG_USER user);
        bool isEmailRepetitive(string email);
    }
}
