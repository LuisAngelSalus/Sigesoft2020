using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SL.Sigesoft.Data.Contracts
{
    public interface IWarehouseRepository
    {
        Task<Warehouse> AddAsync(Warehouse warehouse);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Warehouse>> GetAllAsync();
        Task<Warehouse> GetAsync(int id);
        Task<bool> UpdateAsync(Warehouse entity);
    }
}
