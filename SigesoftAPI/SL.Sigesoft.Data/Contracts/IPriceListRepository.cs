using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SL.Sigesoft.Data.Contracts
{
    public interface IPriceListRepository : IGenericRepository<PriceList>
    {
        Task<IEnumerable<PriceList>> GetAllByCompanyIdAsync(int companyId);
    }
}
