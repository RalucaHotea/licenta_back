using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessObjectLayer.Dtos;
using BusinessObjectLayer.Entities;
using DataAccessLayer.Interfaces;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this._mapper = mapper;
        }

        public async Task AddUserAsync(UserDto user)
        {
            await userRepository.AddUserAsync(_mapper.Map<UserDto, UserEntity>(user));
        }

        public async Task<UserDto> GetUserByUsernameAsync(string username)
        {
            var user = await userRepository.GetUserByUsernameAsync(username);
            return _mapper.Map<UserEntity, UserDto>(user);
        }
    }
}
