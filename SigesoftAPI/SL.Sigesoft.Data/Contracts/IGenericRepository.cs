using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SL.Sigesoft.Data.Contracts
{
   public interface IGenericRepository<T> where T: class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task<T> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}
