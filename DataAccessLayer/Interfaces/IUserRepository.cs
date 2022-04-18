using BusinessObjectLayer.Entities;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IUserRepository
    {
        public Task<UserEntity> GetUserByUsernameAsync(string username);
        public Task<UserEntity> GetUserByIdAsync(int userId);
        public Task AddUserAsync(UserEntity user);
        public Task UpdateUserAsync(UserEntity user);
    }
}
