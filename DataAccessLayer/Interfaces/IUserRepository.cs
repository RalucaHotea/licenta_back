using BusinessObjectLayer.Dtos;
using BusinessObjectLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IUserRepository
    {
        public Task<UserEntity> GetUserByUsernameAsync(string username);
        public Task AddUserAsync(UserEntity user);
    }
}
