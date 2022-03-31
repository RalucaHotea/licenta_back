using BusinessObjectLayer.Dtos;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUserService
    {
        public Task<UserDto> GetUserByUsernameAsync(string username);
        public Task AddUserAsync(UserDto user);
    }
}
