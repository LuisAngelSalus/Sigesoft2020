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

        public ClientUserRepository(SigesoftCoreContext context, ILogger<ClientUserRepository> logger)
        {
            _context = context;
            this._logger = logger;
        }

        public async Task<ClientUser> AddAsync(ClientUser entity)
        {
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
            entityDb.i_UpdateUserId = entity.i_InsertUserId;
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
    }
}
