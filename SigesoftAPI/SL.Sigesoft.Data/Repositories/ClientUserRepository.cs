using Microsoft.AspNetCore.Identity;
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
    public class ClientUserRepository : IClientUserRepository
    {
        private SigesoftCoreContext _context;
        private readonly ILogger<ClientUserRepository> _logger;
        private readonly IPasswordHasher<ClientUser> _passwordHasher;
        private DbSet<ClientUser> _dbSet;
        private DbSet<Company> _dbSetCompany;

        public ClientUserRepository(SigesoftCoreContext context, ILogger<ClientUserRepository> logger, IPasswordHasher<ClientUser> passwordHasher)
        {
            _context = context;
            this._logger = logger;
            this._passwordHasher = passwordHasher;
            this._dbSet = _context.Set<ClientUser>();
            this._dbSetCompany = _context.Set<Company>();
        }

        public async Task<ClientUser> AddAsync(ClientUser entity)
        {
            entity.v_Password = _passwordHasher.HashPassword(entity, entity.v_Password);
            #region AUDIT
            entity.i_IsDeleted = YesNo.No;
            entity.d_InsertDate = DateTime.UtcNow;
            entity.i_InsertUserId = entity.i_InsertUserId;
            #endregion

            _context.ClientUser.Add(entity);
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

        public async Task<bool> DeleteAsync(int id)
        {
            var clientUser = await _context.ClientUser
                               .SingleOrDefaultAsync(c => c.i_ClientUserId == id);
            #region AUDIT
            clientUser.i_IsDeleted = YesNo.No;
            clientUser.d_UpdateDate = DateTime.UtcNow;            
            #endregion

            try
            {
                return await _context.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(DeleteAsync)}: {ex.Message}");
            }
            return false;
        }

        public async Task<IEnumerable<ClientUser>> GetAllAsync()
        {
            return await _context.ClientUser.OrderBy(u => u.v_UserName).ToListAsync();
        }

        public async Task<IEnumerable<ClientUser>> GetAllAsyncByCompany(int companyId)
        {            
            return await _context.ClientUser.Where(w => w.i_CompanyId == companyId).OrderBy(u => u.v_UserName).ToListAsync();           
            
        }

        public async Task<ClientUser> GetAsync(int id)
        {
            return await _context.ClientUser.SingleOrDefaultAsync(u => u.i_ClientUserId == id && u.i_IsDeleted == YesNo.No);
        }

        public async Task<bool> UpdateAsync(ClientUser entity)
        {
            var entityDb = await GetAsync(entity.i_ClientUserId);
            entityDb.v_UserName = entity.v_UserName;
            entityDb.v_FullName = entity.v_FullName;
            entityDb.i_UserTypeId = entity.i_UserTypeId;
            entityDb.i_TypeDocumentId = entity.i_TypeDocumentId;
            entityDb.v_NroDocument = entity.v_NroDocument;
            entityDb.v_NroCpm = entity.v_NroCpm;
            entityDb.v_MobileNumber = entity.v_MobileNumber;
            entityDb.v_Email = entity.v_Email;
            entityDb.i_IsActive = entity.i_IsActive;
            #region AUDIT
            entityDb.d_UpdateDate = DateTime.UtcNow;
            entityDb.i_UpdateUserId = entity.i_UpdateUserId;
            #endregion

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

        public async Task<bool> ChangePassword(ClientUser clientUser)
        {
            var clientUserDb = await _dbSet.FirstOrDefaultAsync(u => u.i_ClientUserId == clientUser.i_ClientUserId);
            clientUserDb.v_Password = _passwordHasher.HashPassword(clientUserDb, clientUser.v_Password);
            try
            {
                return await _context.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(ChangePassword)}: " + ex.Message);
            }
            return false;
        }

        public async Task<bool> UpdateCompany(Company entity)
        {
            try
            {
                var entityDb = await _dbSetCompany.Include(i => i.CompanyHeadquarter).FirstOrDefaultAsync(u => u.i_CompanyId == entity.i_CompanyId);

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
                entityDb.i_ResponsibleSystemUserId = entity.i_ResponsibleSystemUserId;
                #endregion
                #region AUDIT
                entity.i_UpdateUserId = entity.i_UpdateUserId;
                entity.d_UpdateDate = DateTime.Now;
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
