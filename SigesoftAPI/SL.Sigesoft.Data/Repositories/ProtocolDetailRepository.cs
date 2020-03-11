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
    public class ProtocolDetailRepository : IProtocolDetailRepository
    {
        private readonly SigesoftCoreContext _context;
        private readonly ILogger<ProtocolDetailRepository> _logger;
        private DbSet<ProtocolDetail> _dbSet;

        public ProtocolDetailRepository(SigesoftCoreContext context,
            ILogger<ProtocolDetailRepository> logger)
        {
            this._context = context;
            this._logger = logger;
            this._dbSet = _context.Set<ProtocolDetail>();
        }


        public async Task<ProtocolDetail> AddAsync(ProtocolDetail entity)
        {
            #region AUDIT
            entity.i_IsDeleted = YesNo.No;
            entity.d_InsertDate = DateTime.UtcNow;
            entity.i_InsertUserId = entity.i_InsertUserId;
            #endregion

            _dbSet.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(AddAsync)}: " + ex.Message);
            }
            return entity;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProtocolDetail>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProtocolDetail>> GetAllByProtocolIdAsync(int protocolId)
        {
            try
            {
                return await _dbSet.Where(u => u.i_IsDeleted == YesNo.No && u.i_ProtocolId == protocolId).ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
            
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
