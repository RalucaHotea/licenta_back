using BusinessObjectLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface ICartRepository
    {
        public Task<int> CreateCartAsync(UserCartEntity cart);
        public Task<UserCartEntity> GetCartByUserIdAsync(int userId);
        public Task<UserCartEntity> GetCartByIdAsync(int cartId);

    }
}
