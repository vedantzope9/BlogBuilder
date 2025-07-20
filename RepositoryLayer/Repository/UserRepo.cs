using BlogBuilder.Models;
using BlogBuilder.RepositoryLayer.Interfaces;

namespace BlogBuilder.RepositoryLayer.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly BLOG_PROJECTContext _context;
        public UserRepo(BLOG_PROJECTContext context)
        {
            _context = context;
        }

        public bool RegisterUser(BLOG_USER user)
        {

            _context.BLOG_USER.Add(user);
            int changes = _context.SaveChanges();

            return changes > 0;
        }

        public BLOG_USER? FindUserByEmail(string email)
        {
           var entity= _context.BLOG_USER.Where(u => u.EMAIL == email).ToList();

            return entity.Count==0 ? null : entity[0];
        }

        public BLOG_USER? FindUserById(int userId)
        {
            return _context.BLOG_USER.Find(userId);
        }
    }
}
