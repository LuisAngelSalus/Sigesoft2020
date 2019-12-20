using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SL.Sigesoft.Data.Contracts
{
   public interface IPersonsRepository
    {
        Task<List<Person>> GetPersonsAsync();
        Task<Person> GetPersonAsync(int personId);
        Task<Person> AddAsync(Person person);
        Task<bool> UpdateAsync(Person person);
        Task<bool> DeleteAsync(int personId);
    }
}
