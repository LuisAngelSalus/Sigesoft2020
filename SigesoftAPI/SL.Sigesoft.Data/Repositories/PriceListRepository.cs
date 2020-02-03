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
    public class PriceListRepository : IPriceListRepository
    {
        private SigesoftCoreContext _context;
        private readonly ILogger<PriceListRepository> _logger;

        public PriceListRepository(SigesoftCoreContext context, ILogger<PriceListRepository> logger)
        {
            _context = context;
            this._logger = logger;
        }

        public async Task<PriceList> AddAsync(PriceList entity)
        {
            entity.i_CompanyId = entity.i_CompanyId;
            entity.v_ComponentId = entity.v_ComponentId;
            entity.r_Price = entity.r_Price;

            #region AUDIT
            entity.i_IsDeleted = YesNo.No;
            entity.d_InsertDate = DateTime.UtcNow;
            entity.i_InsertUserId = entity.i_InsertUserId;
            #endregion

            _context.PriceList.Add(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(AddAsync)}: {ex.Message}");
            }
            return entity;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PriceList>> GetAllByCompanyIdAsync(int companyId)
        {
            return await _context.PriceList.Where(w => w.i_CompanyId == companyId).OrderBy(u => u.v_ComponentId).ToListAsync();
        }

        public async Task<PriceList> GetAsync(int id)
        {
            return await _context.PriceList.SingleOrDefaultAsync(u => u.i_PriceListId== id && u.i_IsDeleted == YesNo.No);
        }

        public async Task<bool> UpdateAsync(PriceList entity)
        {
            var priceListDb = await GetComponent(entity.i_CompanyId, entity.v_ComponentId);
            if (priceListDb == null)
            {
              var result = await AddAsync(entity);
                if (result.i_PriceListId != 0)                
                    return true;
                else return false;
            }
            else
            {
                priceListDb.r_Price = entity.r_Price;
                #region AUDIT
                priceListDb.d_UpdateDate = DateTime.UtcNow;
                priceListDb.i_UpdateUserId = entity.i_UpdateUserId;
                #endregion
            }         

            try
            {
                return await _context.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(UpdateAsync)}: {ex.Message}");
            }
            return false;
        }

        public Task<IEnumerable<PriceList>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        private async Task<PriceList> GetComponent(int companyId, string componentId)
        {
            return await _context.PriceList.SingleOrDefaultAsync(u => u.i_CompanyId == companyId && u.v_ComponentId == componentId && u.i_IsDeleted == YesNo.No);
        }
    }
}
