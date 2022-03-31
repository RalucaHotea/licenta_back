using BusinessObjectLayer.Entities;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IUserRepository
    {
        public Task<UserEntity> GetUserByUsernameAsync(string username);
        public Task AddUserAsync(UserEntity user);
    }
}
