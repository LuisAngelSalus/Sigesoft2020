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
    public class SecuentialRespository : ISecuentialRespository
    {
        private readonly SigesoftCoreContext _context;
        private readonly ILogger<SecuentialRespository> _logger;
        private DbSet<Secuential> _dbSet;

        public SecuentialRespository(SigesoftCoreContext context,
            ILogger<SecuentialRespository> logger)
        {
            this._context = context;
            this._logger = logger;
            this._dbSet = _context.Set<Secuential>();
        }

        public async Task<int> GetCode(string prefix, int systemUserId, int ownerCompanyId)
        {
            try
            {
                var secuentialDB = await (from A in _context.Secuential
                                          where A.i_OwnerCompanyId == ownerCompanyId && A.v_Process == prefix && A.i_SystemUserId == systemUserId
                                          select A).FirstOrDefaultAsync();

                if (secuentialDB != null)
                {
                    secuentialDB.i_Secuential += 1;
                }
                else
                {
                    var oSecuential = new Secuential();
                    oSecuential.i_OwnerCompanyId = ownerCompanyId;
                    oSecuential.i_SystemUserId = systemUserId;
                    oSecuential.v_Process = prefix;
                    oSecuential.i_Secuential = 1;
                    _dbSet.Add(oSecuential);

                }

                await _context.SaveChangesAsync();

                var newSecuentialDB = await (from A in _context.Secuential
                                          where A.i_OwnerCompanyId == ownerCompanyId && A.v_Process == prefix && A.i_SystemUserId == systemUserId
                                          select A).FirstOrDefaultAsync();

                return newSecuentialDB.i_Secuential;
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
    }
}
