using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SL.Sigesoft.Data.Contracts;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace SL.Sigesoft.Data.Repositories
{
    public class InfoRepository : IInfoRepository
    {
        private readonly SigesoftCoreContext _context;
        private readonly ILogger<InfoRepository> _logger;
        private DbSet<Info> _dbSet;

        public InfoRepository(SigesoftCoreContext context,
            ILogger<InfoRepository> logger)
        {
            this._context = context;
            this._logger = logger;
            this._dbSet = _context.Set<Info>();
        }


        public async Task<Info> GetInfo(string ruc)
        {
            var result = await _dbSet.Include(p => p.Detail)
                              .SingleOrDefaultAsync(c => c.Ruc == ruc);
            var ubigeo = result.Ubigeo;
            var obj = await (from A in _context.Ubigeo
                             where A.v_Ubigeo == ubigeo
                             select A
                                    ).FirstOrDefaultAsync();
            if (obj != null)
            {
                result.Distrito = obj.v_Distrito;
            }
            
            return result;
            
        }
    }
}
