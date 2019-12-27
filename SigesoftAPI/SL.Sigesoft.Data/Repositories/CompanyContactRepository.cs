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
    public class CompanyContactRepository : ICompanyContactRepository
    {
        private readonly SigesoftCoreContext _context;
        private readonly ILogger<CompanyContactRepository> _logger;
        private DbSet<CompanyContact> _dbSet;

        public CompanyContactRepository(SigesoftCoreContext context,
            ILogger<CompanyContactRepository> logger)
        {
            this._context = context;
            this._logger = logger;
            this._dbSet = _context.Set<CompanyContact>();
        }

        public async Task<CompanyContact> AddAsync(CompanyContact entity)
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

        public async Task<IEnumerable<CompanyContact>> GetAllAsync()
        {
            return await _dbSet.Where(u => u.i_IsDeleted == YesNo.No)
                               .ToListAsync();
        }

        public async Task<List<CompanyContact>> GetAllByCompanyId(int companyId)
        {
            var query = await (from A in _context.CompanyContact
                               join B in _context.CompanyHeadquarters on A.i_CompanyHeadquarterId equals B.i_CompanyHeadquarterId
                               join C in _context.Company on B.i_CompanyId equals C.i_CompanyId
                               where C.i_CompanyId == companyId
                               select new CompanyContact
                               {
                                   i_CompanyHeadquarterId = A.i_CompanyHeadquarterId,
                                   v_CompanyHeadquarterName = B.v_Name,
                                   v_FullName = A.v_FullName,
                                   v_TypeUs = A.v_TypeUs,
                                   v_Dni =A.v_Dni,
                                   v_CM = A.v_CM,
                                   v_Phone = A.v_Phone,
                                   v_Email =A.v_Email
                               }).ToListAsync();

            return query;
        }

        public async Task<CompanyContact> GetAsync(int id)
        {
            return await _dbSet.SingleOrDefaultAsync(c => c.i_CompanyContactId == id && c.i_IsDeleted == YesNo.No);
        }

        public async Task<bool> UpdateAsync(CompanyContact entity)
        {
            var entityDb = await _dbSet.FirstOrDefaultAsync(u => u.i_CompanyContactId == entity.i_CompanyContactId);

            if (entityDb == null)
            {
                _logger.LogError($"Error en {nameof(UpdateAsync)}: No existe el Contacto con Id: {entity.i_CompanyContactId}");
                return false;
            }

            entityDb.i_CompanyHeadquarterId = entity.i_CompanyHeadquarterId;
            entityDb.v_FullName = entity.v_FullName;
            entityDb.v_TypeUs = entity.v_TypeUs;
            entityDb.v_Dni = entity.v_Dni;
            entityDb.v_CM = entity.v_CM;
            entityDb.v_Phone = entity.v_Phone;
            entityDb.v_Email = entity.v_Email;

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
