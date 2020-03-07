using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SL.Sigesoft.Data.Contracts
{
    public interface IClientUserRepository : IGenericRepository<ClientUser>
    {
        Task<IEnumerable<ClientUser>> GetAllAsyncByCompany(int companyId);
    }
}
