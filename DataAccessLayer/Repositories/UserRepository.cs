using BusinessObjectLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BoschStoreContext dbContext;

        public UserRepository(BoschStoreContext context)
        {
            dbContext = context;
        }

        public async Task AddUserAsync(UserEntity user)
        {
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
        }

        public Task<UserEntity> GetUserByUsernameAsync(string username)
        {
            return dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Username == username);
        }
    }
}
