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
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly SigesoftCoreContext _context;
        private readonly ILogger<WarehouseRepository> _logger;
        private DbSet<Warehouse> _dbSet;
        public WarehouseRepository(SigesoftCoreContext context, ILogger<WarehouseRepository> logger)
        {
            this._context = context;
            this._logger = logger;
            this._dbSet = _context.Set<Warehouse>();
        }


        public async Task<Warehouse> AddAsync(Warehouse warehouse)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                warehouse.i_CompanyId = warehouse.i_CompanyId == 0 ? null : warehouse.i_CompanyId;
                warehouse.i_CompanyHeadquarterId = warehouse.i_CompanyHeadquarterId == 0 ? null : warehouse.i_CompanyHeadquarterId;
                warehouse.i_IsDeleted = YesNo.No;
                warehouse.d_InsertDate = DateTime.Now;
                _dbSet.Add(warehouse);
                try
                {
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error en {nameof(AddAsync)}: " + ex.Message);
                    transaction.Rollback();
                    return null;
                }
                return warehouse;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.SingleOrDefaultAsync(w => w.i_WarehouseId == id);
            entity.i_IsDeleted = YesNo.Yes;
            entity.d_UpdateDate = DateTime.UtcNow;
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

        public async Task<IEnumerable<Warehouse>> GetAllAsync()
        {
            return await _dbSet
                .Include(w => w.Company)
                .Include(w => w.CompanyHeadquarter)
                        .Where(u => u.i_IsDeleted == YesNo.No)
                        .ToListAsync();
        }

        public async Task<Warehouse> GetAsync(int id)
        {
            return await _dbSet
                 .Include(w => w.Company)
                 .Include(w => w.CompanyHeadquarter)
                                .SingleOrDefaultAsync(c => c.i_WarehouseId == id && c.i_IsDeleted == YesNo.No);
        }

        public async Task<bool> UpdateAsync(Warehouse entity)
        {
            try
            {
                var entityDb = await _dbSet
                      .Include(w => w.Company)
                      .Include(w => w.CompanyHeadquarter)
                      .FirstOrDefaultAsync(u => u.i_WarehouseId == entity.i_WarehouseId);

                if (entityDb == null)
                {
                    _logger.LogError($"Error en {nameof(UpdateAsync)}: No existe el almacén con Id: {entity.i_WarehouseId}");
                    return false;
                }

                entityDb.v_Description = entity.v_Description;
                entityDb.i_CompanyId = entity.i_CompanyId == 0 ? null : entity.i_CompanyId;
                entityDb.i_CompanyHeadquarterId = entity.i_CompanyHeadquarterId == 0 ? null : entity.i_CompanyHeadquarterId;

                entity.i_UpdateUserId = entity.i_UpdateUserId;
                entity.d_UpdateDate = DateTime.Now;

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
