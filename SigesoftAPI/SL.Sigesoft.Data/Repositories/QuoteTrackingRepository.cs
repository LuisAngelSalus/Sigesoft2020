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
    public class QuoteTrackingRepository : IQuoteTrackingRepository
    {
        private readonly SigesoftCoreContext _context;
        private readonly ILogger<QuoteTrackingRepository> _logger;
        private DbSet<QuoteTracking> _dbSet;

        public QuoteTrackingRepository(SigesoftCoreContext context,
              ILogger<QuoteTrackingRepository> logger)
        {
            this._context = context;
            this._logger = logger;
            this._dbSet = _context.Set<QuoteTracking>();
        }

        public async Task<QuoteTracking> AddAsync(QuoteTracking entity)
        {
            entity.i_IsDeleted = YesNo.No;
            entity.d_Date = DateTime.UtcNow;
            entity.d_InsertDate = DateTime.UtcNow;
            entity.i_InsertUserId = entity.i_InsertUserId;
            _context.QuoteTracking.Add(entity);
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

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.SingleOrDefaultAsync(u => u.i_QuoteTrackingId == id);
            entity.i_IsDeleted = YesNo.Yes;
            entity.d_UpdateDate = DateTime.UtcNow;
            entity.i_UpdateUserId = entity.i_UpdateUserId;
            try
            {
                return (await _context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(DeleteAsync)}: " + ex.Message);
            }
            return false;
        }

        public async Task<IEnumerable<QuoteTracking>> GetAllAsync()
        {
            return await _dbSet.Where(u => u.i_IsDeleted == YesNo.No)
                      .ToListAsync();
        }

        public async Task<QuoteTracking> GetAsync(int id)
        {
            return await _dbSet.SingleOrDefaultAsync(c => c.i_QuoteTrackingId== id && c.i_IsDeleted == YesNo.No);
        }

        public async Task<bool> UpdateAsync(QuoteTracking entity)
        {
            var entityDb = await _dbSet.FirstOrDefaultAsync(u => u.i_QuoteTrackingId == entity.i_QuoteTrackingId);

            if (entityDb == null)
            {
                _logger.LogError($"Error en {nameof(UpdateAsync)}: No existe el usuario con Id: {entity.i_QuoteTrackingId}");
                return false;
            }

            entityDb.v_Commentary = entity.v_Commentary;
            entityDb.d_UpdateDate = DateTime.UtcNow;
            entityDb.i_UpdateUserId = entity.i_UpdateUserId;
            try
            {
                return await _context.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception excepcion)
            {
                _logger.LogError($"Error en {nameof(UpdateAsync)}: " + excepcion.Message);
            }
            return false;
        }
    }
}
