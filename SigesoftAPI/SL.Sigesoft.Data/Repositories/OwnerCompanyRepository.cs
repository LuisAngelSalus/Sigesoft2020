using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SL.Sigesoft.Data.Contracts;
using SL.Sigesoft.Models;
using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Sigesoft.Data.Repositories
{
   public class OwnerCompanyRepository : IOwnerCompanyRepository
    {
        private readonly SigesoftCoreContext _context;
        private readonly ILogger<CompanyRepository> _logger;
        private DbSet<OwnerCompany> _dbSet;

        public OwnerCompanyRepository(SigesoftCoreContext context,
            ILogger<CompanyRepository> logger)
        {
            this._context = context;
            this._logger = logger;
            this._dbSet = _context.Set<OwnerCompany>();
        }

        public Task<OwnerCompany> AddAsync(OwnerCompany entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OwnerCompany>> GetAllAsync()
        {
            return await _dbSet.Where(w => w.i_IsDeleted == YesNo.No).ToListAsync();
        }

        public Task<OwnerCompany> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(OwnerCompany entity)
        {
            throw new NotImplementedException();
        }
    }
}
