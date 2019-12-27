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
    public class CompanyRepository : ICompanyRepository
    {
        private readonly SigesoftCoreContext _context;
        private readonly ILogger<CompanyRepository> _logger;        
        private DbSet<Company> _dbSet;

        public CompanyRepository(SigesoftCoreContext context,
            ILogger<CompanyRepository> logger)
        {
            this._context = context;
            this._logger = logger;            
            this._dbSet = _context.Set<Company>();
        }


        public async Task<Company> AddAsync(Company entity)
        {
            entity.i_IsDeleted = YesNo.No;
            var list = new List<CompanyHeadquarter>();
            foreach (var item in entity.CompanyHeadquarter) 
            {
                var o = new CompanyHeadquarter();
                o.i_CompanyId = item.i_CompanyId;
                o.v_Name = item.v_Name;
                o.v_Address = item.v_Address;
                o.v_PhoneNumber = item.v_PhoneNumber;
                o.i_IsDeleted = YesNo.No;
                list.Add(o);
            }

            entity.CompanyHeadquarter = list;
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
            var entity = await _dbSet.SingleOrDefaultAsync(u => u.i_CompanyId == id);
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

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _dbSet.Include(su => su.CompanyHeadquarter)
                        .Where(u => u.i_IsDeleted == YesNo.No)
                        .ToListAsync();            
        }

        public async Task<Company> GetAsync(int id)
        {
            return await _dbSet.Include(per => per.CompanyHeadquarter)
                                .SingleOrDefaultAsync(c => c.i_CompanyId == id && c.i_IsDeleted == YesNo.No);
        }
        
        public async Task<bool> UpdateAsync(Company entity)
        {
            var entityDb = await _dbSet.Include(i => i.CompanyHeadquarter).FirstOrDefaultAsync(u => u.i_CompanyId == entity.i_CompanyId);

            if (entityDb == null)
            {
                _logger.LogError($"Error en {nameof(UpdateAsync)}: No existe el usuario con Id: {entity.i_CompanyId}");
                return false;
            }

            #region update Company
            entityDb.v_Name = entity.v_Name;
            entityDb.v_IdentificationNumber = entity.v_IdentificationNumber;
            entityDb.v_Address = entity.v_Address;
            entityDb.v_PhoneNumber = entity.v_PhoneNumber;
            entityDb.v_ContactName = entity.v_ContactName;
            entityDb.v_Mail = entity.v_Mail;
            entityDb.v_District = entity.v_District;
            entityDb.v_PhoneCompany = entity.v_PhoneCompany;
            #endregion

            foreach (var item in entity.CompanyHeadquarter)
            {
                if (item.RecordType == RecordType.Temporal && item.RecordStatus == RecordStatus.Agregado)
                {
                    var o = new CompanyHeadquarter();
                    o.i_CompanyId = item.i_CompanyId;                    
                    o.v_Name = item.v_Name;
                    o.v_Address = item.v_Address;
                    o.v_PhoneNumber = item.v_PhoneNumber;
                    o.i_IsDeleted = YesNo.No;
                    entityDb.CompanyHeadquarter.Add(o);
                }
                if (item.RecordType == RecordType.NoTemporal && (item.RecordStatus == RecordStatus.Modificado || item.RecordStatus == RecordStatus.Grabado))
                {
                    var o = entityDb.CompanyHeadquarter.Where(w => w.i_CompanyHeadquarterId == item.i_CompanyHeadquarterId).FirstOrDefault();                    
                    o.v_Name = item.v_Name;                    
                    o.v_Address = item.v_Address;
                    o.v_PhoneNumber = item.v_PhoneNumber;                    
                    entityDb.CompanyHeadquarter.Add(o);
                }

                if (item.RecordType == RecordType.NoTemporal && item.RecordStatus == RecordStatus.EliminadoLogico)
                {
                    var o = entityDb.CompanyHeadquarter.Where(w => w.i_CompanyHeadquarterId == item.i_CompanyHeadquarterId).FirstOrDefault();
                    o.i_IsDeleted = YesNo.Yes;
                    entityDb.CompanyHeadquarter.Add(o);
                }
            }
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
        
        public async Task<Company> GetCompanyWithHeadquarter(int companyId)
        {
            //return await _dbSet.Where(u => u.i_IsDeleted == YesNo.No)
            //        .Include(qu => qu.CompanyHeadquarter)
            //        .SingleOrDefaultAsync(c => c.i_CompanyId == companyId);

            var query = await (from A in _context.Company
                               where A.i_IsDeleted == YesNo.No
                               select new Company
                               {
                                   i_CompanyId = A.i_CompanyId,
                                   v_Name = A.v_Name,
                                   v_IdentificationNumber = A.v_IdentificationNumber,
                                   v_Address = A.v_Address,
                                   v_PhoneNumber = A.v_PhoneNumber,
                                   v_ContactName = A.v_ContactName,
                                   v_Mail = A.v_Mail,
                                   v_District = A.v_District,
                                   v_PhoneCompany = A.v_PhoneCompany,
                                   CompanyHeadquarter = (from B in _context.CompanyHeadquarters
                                                         where B.i_CompanyId == companyId && B.i_IsDeleted == YesNo.No
                                                         select B)
                                                         .ToList()
                               }
                               ).FirstOrDefaultAsync();
            return query;
            
        }

        public async Task<bool> UpdateWithDetailAsync(Company entity)
        {
            var entityDb = await _dbSet.FirstOrDefaultAsync(u => u.i_CompanyId == entity.i_CompanyId);

            if (entityDb == null)
            {
                _logger.LogError($"Error en {nameof(UpdateAsync)}: No existe el usuario con Id: {entity.i_CompanyId}");
                return false;
            }

            entityDb.v_Name = entity.v_Name;
            entityDb.v_IdentificationNumber = entity.v_IdentificationNumber;
            entityDb.v_Address = entity.v_Address;
            entityDb.v_PhoneNumber = entity.v_PhoneNumber;
            entityDb.v_ContactName = entity.v_ContactName;
            entityDb.v_Mail = entity.v_Mail;
            entityDb.v_District = entity.v_District;
            entityDb.v_PhoneCompany = entity.v_PhoneCompany;
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
