using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SL.Sigesoft.Data.Contracts;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using SL.Sigesoft.Models.Enum;

namespace SL.Sigesoft.Data.Repositories
{
    public class ProtocolRepository : IProtocolRepository
    {
        private readonly SigesoftCoreContext _context;
        private DbSet<Protocol> _dbSet;
        private readonly ILogger<ProtocolRepository> _logger;

        public ProtocolRepository(SigesoftCoreContext context, ILogger<ProtocolRepository> logger)
        {
            this._context = context;            
            this._logger = logger;
            this._dbSet = _context.Set<Protocol>();
        }
        
        public async Task<Protocol> AddAsync(Protocol entity)
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

        public Task<IEnumerable<Protocol>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Protocol> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProtocolListModel>> GetProtocolsByCompanyId(int CompanyId)
        {
            try
            {
                var query = await (from A in _context.Protocol
                                   join B in _context.Company on A.i_CompanyId equals B.i_CompanyId

                                   join C in _context.SystemParameter on new { a = A.i_ServiceTypeId, b = 101 }
                                                equals new { a = C.i_ParameterId, b = C.i_GroupId } into C_join
                                   from C in C_join.DefaultIfEmpty()

                                   join D in _context.SystemParameter on new { a = A.i_ServiceTypeId, b = 104 }
                                                equals new { a = D.i_ParameterId, b = D.i_GroupId } into D_join
                                   from D in D_join.DefaultIfEmpty()

                                   where A.i_CompanyId == CompanyId
                                   select new ProtocolListModel
                                   {
                                       i_ProtocolId = A.i_ProtocolId,
                                       i_CompanyId = A.i_CompanyId,
                                       v_CompanyName = B.v_Name,
                                       v_ProtocolName = A.v_ProtocolName,
                                       i_ServiceTypeId = A.i_ServiceTypeId,
                                       v_ServiceTypeName = C.v_Value1,
                                       i_TypeFormatId = A.i_TypeFormatId,
                                       v_TypeFormatName = D.v_Value1,
                                       //i_QuotationProfileIdRef = A.i_QuotationProfileIdRef.Value
                                   }).ToListAsync();

                return query;
            }
            catch (Exception ex)
            {

                throw;
            }
    
        }

        public Task<bool> UpdateAsync(Protocol entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AdditionalComponentsModel>> GetAdditionalComponents(int protocolId)
        {
            try
            {
                var query = await (from A in _context.Protocol
                                   join B in _context.AdditionalComponentsQuote on A.i_QuotationId equals B.i_QuotationId
                                   where A.i_ProtocolId == protocolId
                                   select new AdditionalComponentsModel
                                   {
                                       ComponentId = B.v_ComponentId,
                                       Name = B.v_ComponentName,
                                       CategoryName = B.v_CategoryName,
                                       CategoryId = B.i_CategoryId,
                                       SalePrice = B.r_SalePrice
                                   }).ToListAsync();
                return query;
            }
            catch (Exception ex)
            {

                throw;
            }    
        }
    }
}
