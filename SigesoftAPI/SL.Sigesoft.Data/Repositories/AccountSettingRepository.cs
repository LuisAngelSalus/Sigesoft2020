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
    public class AccountSettingRepository : IAccountSettingRepository
    {
        private readonly SigesoftCoreContext _context;
        private readonly ILogger<AccountSettingRepository> _logger;
        private DbSet<AccountSetting> _dbSet;

        public AccountSettingRepository(SigesoftCoreContext context,
            ILogger<AccountSettingRepository> logger)
        {
            this._context = context;
            this._logger = logger;
            this._dbSet = _context.Set<AccountSetting>();
        }

        public async Task<AccountSetting> AddAsync(AccountSetting entity)
        {
            entity.i_IsDeleted = YesNo.No;
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

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.SingleOrDefaultAsync(u => u.i_AccountSettingId == id);
            entity.i_IsDeleted = YesNo.Yes;
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

        public async Task<IEnumerable<AccountSetting>> GetAllAsync()
        {
            return await _dbSet.Where(u => u.i_IsDeleted == YesNo.No)
                               .ToListAsync();
        }

        public async Task<AccountSetting> GetAsync(int id)
        {
            return await _dbSet.SingleOrDefaultAsync(c => c.i_AccountSettingId == id && c.i_IsDeleted == YesNo.No);
        }

        public async Task<bool> UpdateAsync(AccountSetting entity)
        {
            var entityDb = await _dbSet.FirstOrDefaultAsync(u => u.i_AccountSettingId== entity.i_AccountSettingId);

            if (entityDb == null)
            {
                _logger.LogError($"Error en {nameof(UpdateAsync)}: No existe el AccountSetting con Id: {entity.i_AccountSettingId}");
                return false;
            }

            entityDb.i_OwnerCompanyId = entity.i_OwnerCompanyId;
            entityDb.i_RoleId = entity.i_RoleId;
            
            try
            {
                return await _context.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(UpdateAsync)}: " + ex.Message);
            }
            return false;
        }
    }
}
