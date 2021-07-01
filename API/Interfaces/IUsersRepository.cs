using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface IUsersRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<AppUser> GetUserByUsernameAsync(string username);
        Task<AppUser> GetUserByUserIdAsync(int userId);
        void Delete(AppUser user);
    }
}