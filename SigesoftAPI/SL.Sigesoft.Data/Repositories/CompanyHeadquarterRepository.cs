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
    public class CompanyHeadquarterRepository : ICompanyHeadquarterRepository
    {
        private readonly SigesoftCoreContext _context;
        private readonly ILogger<CompanyHeadquarterRepository> _logger;
        private DbSet<CompanyHeadquarter> _dbSet;

        public CompanyHeadquarterRepository(SigesoftCoreContext context,
            ILogger<CompanyHeadquarterRepository> logger)
        {
            this._context = context;
            this._logger = logger;
            this._dbSet = _context.Set<CompanyHeadquarter>();
        }


        public async Task<CompanyHeadquarter> AddAsync(CompanyHeadquarter entity)
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
            var entity = await _dbSet.SingleOrDefaultAsync(u => u.i_CompanyHeadquarterId == id);
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

        public async Task<IEnumerable<CompanyHeadquarter>> GetAllAsync()
        {
            return await _dbSet.Where(u => u.i_IsDeleted == YesNo.No)
                               .ToListAsync();
        }

        public async Task<CompanyHeadquarter> GetAsync(int id)
        {
            return await _dbSet.SingleOrDefaultAsync(c => c.i_CompanyHeadquarterId == id && c.i_IsDeleted == YesNo.No);
        }

        public async Task<bool> UpdateAsync(CompanyHeadquarter entity)
        {
            var entityDb = await _dbSet.FirstOrDefaultAsync(u => u.i_CompanyHeadquarterId == entity.i_CompanyHeadquarterId);

            if (entityDb == null)
            {
                _logger.LogError($"Error en {nameof(UpdateAsync)}: No existe el usuario con Id: {entity.i_CompanyHeadquarterId}");
                return false;
            }

            entityDb.v_Name = entity.v_Name;
            entityDb.v_Address = entity.v_Address;
            entityDb.v_PhoneNumber = entity.v_PhoneNumber;            
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
