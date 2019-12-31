using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SL.Sigesoft.Data.Contracts.Win;
using SL.Sigesoft.Models.Win;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SL.Sigesoft.Data.Repositories.Win
{
   public class ComponentRepository : IComponentRepository
    {
        private readonly SigesoftWinContext _context;
        private readonly ILogger<ComponentRepository> _logger;
        private DbSet<ComponentWin> _dbSet;

        public ComponentRepository(SigesoftWinContext context,
           ILogger<ComponentRepository> logger)
        {
            this._context = context;
            this._logger = logger;
            this._dbSet = _context.Set<ComponentWin>();
        }

        public Task<List<ComponentWin>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
