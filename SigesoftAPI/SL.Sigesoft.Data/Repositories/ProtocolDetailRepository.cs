using SL.Sigesoft.Data.Contracts;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SL.Sigesoft.Data.Repositories
{
    public class ProtocolDetailRepository : IProtocolDetailRepository
    {
        public Task<ProtocolDetail> AddAsync(ProtocolDetail entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProtocolDetail>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProtocolDetail> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(ProtocolDetail entity)
        {
            throw new NotImplementedException();
        }
    }
}
