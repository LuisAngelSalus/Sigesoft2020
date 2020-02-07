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
    public class RoleRepository : IRoleRepository
    {
        private readonly SigesoftCoreContext _context;
        private readonly ILogger<RoleRepository> _logger;
        private DbSet<Role> _dbSet;

        public RoleRepository(SigesoftCoreContext context,
            ILogger<RoleRepository> logger)
        {
            this._context = context;
            this._logger = logger;
            this._dbSet = _context.Set<Role>();
        }

        public Task<Role> AddAsync(Role entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _dbSet.Where(w => w.i_IsDeleted == YesNo.No).ToListAsync();
        }

        public Task<Role> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Role entity)
        {
            throw new NotImplementedException();
        }
    }
}
