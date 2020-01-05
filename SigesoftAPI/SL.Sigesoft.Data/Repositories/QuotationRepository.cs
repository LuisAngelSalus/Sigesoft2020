﻿using Microsoft.EntityFrameworkCore;
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
    public class QuotationRepository : IQuotationRepository
    {
        private readonly SigesoftCoreContext _context;
        private readonly ILogger<QuotationRepository> _logger;
        private DbSet<Quotation> _dbSet;

        public QuotationRepository(SigesoftCoreContext context,
          ILogger<QuotationRepository> logger)
        {
            this._context = context;
            this._logger = logger;
            this._dbSet = _context.Set<Quotation>();
        }

        public async Task<Quotation> AddAsync(Quotation entity)
        {            
            #region AUDIT
            entity.i_IsDeleted = YesNo.No;
            entity.d_InsertDate = DateTime.UtcNow;
            entity.i_InsertUserId = entity.i_InsertUserId;
            #endregion
            foreach (var item in entity.QuotationProfiles)
            {
                #region AUDIT
                item.i_IsDeleted = YesNo.No;
                item.d_InsertDate = DateTime.UtcNow;
                item.i_InsertUserId = entity.i_InsertUserId;
                #endregion
                foreach (var item2 in item.ProfileComponents)
                {
                    #region AUDIT
                    item2.i_IsDeleted = YesNo.No;
                    item2.d_InsertDate = DateTime.UtcNow;
                    item2.i_InsertUserId = entity.i_InsertUserId;
                    #endregion
                }
            }
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

        public Task<IEnumerable<Quotation>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Quotation> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<QuotationModel> GetQuotationAsync(int id)
        {
            try
            {


                var query = await (from A in _context.Quotation
                                   join B in _context.Company on A.i_CompanyId equals B.i_CompanyId
                                   join C in _context.SystemUser on A.i_UserCreatedId equals C.i_SystemUserId
                                   where A.i_QuotationId == id && A.i_IsDeleted == YesNo.No
                                   select new QuotationModel
                                   {
                                       QuotationId = A.i_QuotationId,
                                       Code = A.v_Code,
                                       Version = A.i_Version,
                                       UserCreatedId = A.i_UserCreatedId,
                                       UserName = C.v_UserName,
                                       CompanyRuc = B.v_IdentificationNumber,
                                       CompanyId = A.i_CompanyId,
                                       CompanyName = B.v_Name,
                                       CompanyDistrictName = B.v_District,
                                       CompanyAddress = B.v_Address,
                                       CompanyHeadquarterId = A.i_CompanyHeadquarterId,
                                       FullName = A.v_FullName,
                                       Email = A.v_Email,
                                       TypeFormatId = A.i_TypeFormatId,
                                       CommercialTerms = A.v_CommercialTerms,
                                       QuotationProfiles = (from A1 in _context.QuotationProfile
                                                            join B1 in _context.SystemParameter on new { a = A1.i_ProfileId.Value, b = 100 }
                                                                                               equals new { a = B1.i_ParameterId, b = B1.i_GroupId } into B1_join
                                                            from B1 in B1_join.DefaultIfEmpty()
                                                            join C1 in _context.SystemParameter on new { a = A1.i_ServiceTypeId.Value, b = 101 }
                                                                                               equals new { a = C1.i_ParameterId, b = C1.i_GroupId } into C1_join
                                                            from C1 in C1_join.DefaultIfEmpty()
                                                            where A1.i_QuotationId == A.i_QuotationId
                                                            select new QuotationProfileModel
                                                            {
                                                                QuotationId = A.i_QuotationId,
                                                                QuotationProfileId = A1.i_QuotationProfileId, 
                                                                ProfileId = A1.i_ProfileId.Value,
                                                                ProfileName = B1.v_Value1,
                                                                ServiceTypeId = A1.i_ServiceTypeId,
                                                                ServiceTypeName = C1.v_Value1,
                                                                ProfileComponents = (from A2 in _context.ProfileComponent
                                                                                     where A2.i_QuotationProfileId == A1.i_QuotationProfileId
                                                                                     orderby A2.v_CategoryName
                                                                                     select new ProfileComponentModel
                                                                                     {
                                                                                         ProfileComponentId = A2.i_ProfileComponentId,
                                                                                         QuotationProfileId = A2.i_QuotationProfileId,
                                                                                         CategoryId = A2.i_CategoryId,
                                                                                         CategoryName = A2.v_CategoryName,
                                                                                         ComponentId = A2.v_ComponentId,
                                                                                         ComponentName = A2.v_ComponentName,
                                                                                         MinPrice = A2.r_MinPrice,
                                                                                         PriceList = A2.r_PriceList,
                                                                                         SalePrice = A2.r_SalePrice
                                                                                     }).ToList()
                                                            }).ToList()

                                   }).FirstOrDefaultAsync();
                return query;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> UpdateAsync(Quotation entity)
        {
            var entityDb = await _dbSet.Include(i => i.QuotationProfiles)
                                    .ThenInclude(i => i.ProfileComponents)
                                .FirstOrDefaultAsync(u => u.i_QuotationId == entity.i_QuotationId);

            if (entityDb == null)
            {
                _logger.LogError($"Error en {nameof(UpdateAsync)}: No existe el usuario con Id: {entity.i_CompanyId}");
                return false;
            }

            #region Update Quotation
            entityDb.v_Code = entity.v_Code;
            entityDb.i_CompanyId = entity.i_CompanyId;
            entityDb.i_CompanyHeadquarterId = entity.i_CompanyHeadquarterId;
            entityDb.v_FullName = entity.v_FullName;
            entityDb.v_Email = entity.v_Email;
            entityDb.i_TypeFormatId = entity.i_TypeFormatId;
            entityDb.v_CommercialTerms = entity.v_CommercialTerms;
            entityDb.i_UpdateUserId = entity.i_UpdateUserId;
            
            entityDb.d_UpdateDate = DateTime.UtcNow;
            entityDb.i_UpdateUserId = entity.i_UpdateUserId;

            #endregion

            #region QuotationProfiles
            UpdateQuotationProfiles(entity.QuotationProfiles, entityDb);
            #endregion
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

        private void UpdateQuotationProfiles(List<QuotationProfile> quotationProfiles, Quotation entityDb)
        {
            foreach (var item in quotationProfiles)
            {
                if (item.RecordType == RecordType.Temporal && item.RecordStatus == RecordStatus.Agregado)
                {
                    var o = new QuotationProfile();
                    o.i_QuotationId = item.i_QuotationId;
                    o.i_ProfileId = item.i_ProfileId;
                    o.i_ServiceTypeId = item.i_ServiceTypeId;
                    o.i_InsertUserId = item.i_UpdateUserId;
                    o.i_IsDeleted = YesNo.No;
                    entityDb.QuotationProfiles.Add(o);
                }
                if (item.RecordType == RecordType.NoTemporal && (item.RecordStatus == RecordStatus.Modificado || item.RecordStatus == RecordStatus.Grabado))
                {
                    var o = entityDb.QuotationProfiles.Where(w => w.i_QuotationProfileId== item.i_QuotationProfileId).FirstOrDefault();
                    o.i_ServiceTypeId = item.i_ServiceTypeId;                                        
                    o.d_UpdateDate = DateTime.UtcNow;
                    o.i_UpdateUserId = item.i_UpdateUserId;
                    entityDb.QuotationProfiles.Add(o);
                }
                if (item.RecordType == RecordType.NoTemporal && item.RecordStatus == RecordStatus.EliminadoLogico)
                {
                    var o = entityDb.QuotationProfiles.Where(w => w.i_QuotationProfileId== item.i_QuotationProfileId).FirstOrDefault();
                    o.i_IsDeleted = YesNo.Yes;                    
                    o.d_UpdateDate = DateTime.UtcNow;
                    o.i_UpdateUserId = item.i_UpdateUserId;
                    entityDb.QuotationProfiles.Add(o);
                }

                #region ProfileComponents
                var x = entityDb.QuotationProfiles.Find(p => p.i_QuotationProfileId == item.i_QuotationProfileId);
                UpdateProfileComponent(item.ProfileComponents, x);
                #endregion
            }
        }

        private void UpdateProfileComponent(List<ProfileComponent> profileComponents, QuotationProfile quotationProfile)
        {
            foreach (var component in profileComponents)
            {
                if (component.RecordType == RecordType.Temporal && component.RecordStatus == RecordStatus.Agregado)
                {
                    var o = new ProfileComponent();
                    o.i_CategoryId = component.i_CategoryId;
                    o.v_CategoryName = component.v_CategoryName;
                    o.v_ComponentId = component.v_ComponentId;
                    o.v_ComponentName = component.v_ComponentName;
                    o.r_SalePrice = component.r_SalePrice;
                    o.i_UpdateUserId = component.i_UpdateUserId;
                    o.i_IsDeleted = YesNo.No;
                    quotationProfile.ProfileComponents.Add(o);
                }
                if (component.RecordType == RecordType.NoTemporal && (component.RecordStatus == RecordStatus.Modificado || component.RecordStatus == RecordStatus.Grabado))
                {
                    var o = quotationProfile.ProfileComponents.Where(w => w.i_ProfileComponentId == component.i_ProfileComponentId).FirstOrDefault();
                    o.r_SalePrice = component.r_SalePrice;
                    o.d_UpdateDate = DateTime.UtcNow;
                    o.i_UpdateUserId = component.i_UpdateUserId;
                    quotationProfile.ProfileComponents.Add(o);
                }
                if (component.RecordType == RecordType.NoTemporal && component.RecordStatus == RecordStatus.EliminadoLogico)
                {
                    var o = quotationProfile.ProfileComponents.Where(w => w.i_ProfileComponentId == component.i_ProfileComponentId).FirstOrDefault();
                    o.i_IsDeleted = YesNo.Yes;
                    o.d_UpdateDate = DateTime.UtcNow;
                    o.i_UpdateUserId = component.i_UpdateUserId;
                    quotationProfile.ProfileComponents.Add(o);
                }
            }           
        }
    }
}