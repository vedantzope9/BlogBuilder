using BlogBuilder.Models;
using BlogBuilder.RepositoryLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogBuilder.RepositoryLayer.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly BLOG_PROJECTContext _context;
        public UserRepo(BLOG_PROJECTContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterUser(BLOG_USER user)
        {

            await _context.BLOG_USER.AddAsync(user);
            int changes = await _context.SaveChangesAsync();

            return changes > 0;
        }

        public async Task<BLOG_USER?> FindUserByEmail(string email)
        {
           var entity= await _context.BLOG_USER.Where(u => u.EMAIL == email).ToListAsync();

            return entity.Count==0 ? null : entity[0];
        }

        public async Task<BLOG_USER?> FindUserById(int userId)
        {
            return await _context.BLOG_USER.FindAsync(userId);
        }

        public async Task<List<BLOG_USER>> GetAllUsers()
        {
            return await _context.BLOG_USER.ToListAsync();
        }
    }
}
