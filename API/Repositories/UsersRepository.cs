using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DataContext _context;
        public UsersRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.UserName == username);  
        }

        public async Task<AppUser> GetUserByUserIdAsync(int userId)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);  
        }        

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Attach(user);
        }

        public void Delete(AppUser user)
        {
            _context.Users.Remove(user);
        }
    }
}