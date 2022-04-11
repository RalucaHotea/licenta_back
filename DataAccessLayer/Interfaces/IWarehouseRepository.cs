using BusinessObjectLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IWarehouseRepository
    {
        public Task<List<WarehouseEntity>> GetAllWarehouses();
    }
}
