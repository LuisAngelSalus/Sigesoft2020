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
using SL.Sigesoft.Common;
using System.Globalization;

namespace SL.Sigesoft.Data.Repositories
{
    public class QuotationRepository : IQuotationRepository
    {
        private ISecuentialRespository _secuentialRespository;
        private readonly SigesoftCoreContext _context;
        private readonly ILogger<QuotationRepository> _logger;
        private DbSet<Quotation> _dbSet;

        public QuotationRepository(SigesoftCoreContext context,
          ILogger<QuotationRepository> logger, ISecuentialRespository secuentialRespository)
        {
            this._context = context;
            this._logger = logger;
            this._dbSet = _context.Set<Quotation>();
            this._secuentialRespository = secuentialRespository;
        }

        public async Task<Quotation> AddAsync(Quotation entity)
        {
            #region Code
            entity.v_Code = Utils.Code("COT", entity.i_UserCreatedId.ToString(),await _secuentialRespository.GetCode("COT", entity.i_UserCreatedId, 1));
            entity.i_Version = 1;
            entity.i_IsProccess = YesNo.Yes;
            #endregion

            #region AUDIT
            //entity.d_ShippingDate = DateTime.UtcNow;
            entity.i_IsDeleted = YesNo.No;
            entity.d_InsertDate = DateTime.UtcNow;
            entity.i_InsertUserId = entity.i_InsertUserId;
         
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

            foreach (var item in entity.AdditionalComponentsQuotes)
            {
                #region AUDIT
                item.i_IsDeleted = YesNo.No;
                item.d_InsertDate = DateTime.UtcNow;
                item.i_InsertUserId = entity.i_InsertUserId;
                #endregion
            }

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

        public async Task<Quotation> NewVersion(Quotation entity)
        {

            #region AUDIT
            entity.i_Version = GetLastVersion(entity.v_Code) + 1;

            if (entity.i_StatusQuotationId == (int)StatusQuotation.Seguimiento)
                entity.d_ShippingDate = DateTime.UtcNow;

            entity.i_IsDeleted = YesNo.No;
            entity.d_InsertDate = DateTime.UtcNow;
            entity.i_InsertUserId = entity.i_InsertUserId;

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
                    //PARCHE
                    if (item2.RecordStatus == RecordStatus.EliminadoLogico)
                    {
                        item2.i_IsDeleted = YesNo.Yes;
                    }
                    else
                    {
                        item2.i_IsDeleted = YesNo.No;
                    }
                    
                    item2.d_InsertDate = DateTime.UtcNow;
                    item2.i_InsertUserId = entity.i_InsertUserId;
                    #endregion
                }
            }

            foreach (var item in entity.AdditionalComponentsQuotes)
            {
                #region AUDIT
                item.i_IsDeleted = YesNo.No;
                item.d_InsertDate = DateTime.UtcNow;
                item.i_InsertUserId = entity.i_InsertUserId;
                #endregion
            }

            #endregion
            _dbSet.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(NewVersion)}: " + ex.Message);
                return null;
            }
            return entity;
        }

        public Task<bool> DeleteAsync(int id)
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
                                       CommercialTerms = A.v_CommercialTerms,
                                       StatusQuotationId = A.i_StatusQuotationId.Value,
                                       QuotationProfiles = (from A1 in _context.QuotationProfile
                                                            join C1 in _context.SystemParameter on new { a = A1.i_ServiceTypeId.Value, b = 101 }
                                                                                               equals new { a = C1.i_ParameterId, b = C1.i_GroupId } into C1_join
                                                            from C1 in C1_join.DefaultIfEmpty()
                                                            where A1.i_QuotationId == A.i_QuotationId && A1.i_IsDeleted == YesNo.No
                                                            select new QuotationProfileModel
                                                            {
                                                                QuotationId = A.i_QuotationId,
                                                                QuotationProfileId = A1.i_QuotationProfileId,
                                                                ProfileName = A1.v_ProfileName,
                                                                ServiceTypeId = A1.i_ServiceTypeId,
                                                                ServiceTypeName = C1.v_Value1,
                                                                TypeFormatId = A1.i_TypeFormatId,
                                                                RecordStatus = RecordStatus.Grabado,
                                                                RecordType = RecordType.NoTemporal,
                                                                ProfileComponents = (from A2 in _context.ProfileComponent
                                                                                     where A2.i_QuotationProfileId == A1.i_QuotationProfileId && A2.i_IsDeleted == YesNo.No
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
                                                                                         SalePrice = A2.r_SalePrice,
                                                                                         AgeConditionalId = A2.i_AgeConditionalId.Value,
                                                                                         Age = A2.i_Age.Value,
                                                                                         GenderConditionalId = A2.i_GenderConditionalId.Value,
                                                                                         RecordStatus = RecordStatus.Grabado,
                                                                                         RecordType = RecordType.NoTemporal,
                                                                                     }).ToList()
                                                            }).ToList(),

                                       AdditionalComponentsQuotes = (from A3 in _context.AdditionalComponentsQuote
                                                                     where A3.i_QuotationId == A.i_QuotationId
                                                                     select new AdditionalComponentsQuoteModel {
                                                                         AdditionalComponentsQuoteId =  A3.i_AdditionalComponentsQuoteId,
                                                                         CategoryId =  A3.i_CategoryId,
                                                                         CategoryName =  A3.v_CategoryName,
                                                                         ComponentId = A3.v_ComponentId,
                                                                         ComponentName = A3.v_ComponentName,
                                                                         MinPrice =  A3.r_MinPrice,
                                                                         PriceList =  A3.r_PriceList,
                                                                         SalePrice = A3.r_SalePrice,
                                                                         RecordStatus = RecordStatus.Grabado,
                                                                         RecordType = RecordType.NoTemporal,
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
            //Solo para actulizar Estado de cotización dentro de la matriz
            if (string.IsNullOrEmpty(entity.v_Code))
            {
                if (entity.i_StatusQuotationId == (int)StatusQuotation.Seguimiento)
                    entityDb.d_ShippingDate = DateTime.Now;

                if (entity.i_StatusQuotationId == (int)StatusQuotation.Aceptada)
                    entityDb.d_AcceptanceDate = DateTime.UtcNow;

                entityDb.i_StatusQuotationId = entity.i_StatusQuotationId;               
            }
            else
            {
                #region Update Quotation
                entityDb.v_Code = entity.v_Code;
                entityDb.i_CompanyId = entity.i_CompanyId;
                entityDb.i_CompanyHeadquarterId = entity.i_CompanyHeadquarterId;
                entityDb.v_FullName = entity.v_FullName;
                entityDb.v_Email = entity.v_Email;
                entityDb.v_CommercialTerms = entity.v_CommercialTerms;
                entityDb.r_TotalQuotation = entity.r_TotalQuotation;
                entityDb.i_StatusQuotationId = entity.i_StatusQuotationId;
                entityDb.i_UpdateUserId = entity.i_UpdateUserId;
                entityDb.d_UpdateDate = DateTime.UtcNow;
                entityDb.i_UpdateUserId = entity.i_UpdateUserId;

                #endregion


                UpdateAddittionalExamn(entity.AdditionalComponentsQuotes, entityDb);

                #region QuotationProfiles
                UpdateQuotationProfiles(entity.QuotationProfiles, entityDb);
                #endregion

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

        private void UpdateQuotationProfiles(List<QuotationProfile> quotationProfiles, Quotation entityDb)
        {
            try
            {
                foreach (var item in quotationProfiles)
                {
                    if (item.RecordType == RecordType.Temporal && item.RecordStatus == RecordStatus.Agregado)
                    {
                        var o = new QuotationProfile();
                        o.i_QuotationId = item.i_QuotationId;
                        o.v_ProfileName = item.v_ProfileName;
                        o.i_ServiceTypeId = item.i_ServiceTypeId;
                        o.i_InsertUserId = item.i_UpdateUserId;
                        o.i_IsDeleted = YesNo.No;
                        entityDb.QuotationProfiles.Add(o);
                    }
                    if (item.RecordType == RecordType.NoTemporal && (item.RecordStatus == RecordStatus.Modificado || item.RecordStatus == RecordStatus.Grabado))
                    {
                        var o = entityDb.QuotationProfiles.Where(w => w.i_QuotationProfileId == item.i_QuotationProfileId).FirstOrDefault();
                        o.i_ServiceTypeId = item.i_ServiceTypeId;
                        o.v_ProfileName = item.v_ProfileName;
                        o.d_UpdateDate = DateTime.UtcNow;
                        o.i_UpdateUserId = item.i_UpdateUserId;
                        entityDb.QuotationProfiles.Add(o);
                    }
                    if (item.RecordType == RecordType.NoTemporal && item.RecordStatus == RecordStatus.EliminadoLogico)
                    {
                        var o = entityDb.QuotationProfiles.Where(w => w.i_QuotationProfileId == item.i_QuotationProfileId).FirstOrDefault();
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
            catch (Exception ex)
            {

                throw;
            }
         
        }

        private void UpdateAddittionalExamn(List<AdditionalComponentsQuote> additionalComponents, Quotation entityDb)
        {
            try
            {
                foreach (var item in additionalComponents)
                {
                    if (item.RecordType == RecordType.Temporal && item.RecordStatus == RecordStatus.Agregado)
                    {
                        var o = new AdditionalComponentsQuote();
                        o.i_QuotationId = item.i_QuotationId;
                        o.v_CategoryName = item.v_CategoryName;
                        o.i_CategoryId = item.i_CategoryId;

                        o.v_ComponentId = item.v_ComponentId;
                        o.v_ComponentName = item.v_ComponentName;
                        o.r_MinPrice = item.r_MinPrice;
                        o.r_PriceList = item.r_PriceList;
                        o.r_SalePrice = item.r_SalePrice;

                        o.i_InsertUserId = item.i_UpdateUserId;
                        o.i_IsDeleted = YesNo.No;
                        entityDb.AdditionalComponentsQuotes.Add(o);
                    }
                    if (item.RecordType == RecordType.NoTemporal && (item.RecordStatus == RecordStatus.Modificado || item.RecordStatus == RecordStatus.Grabado))
                    {
                        //var o = entityDb.QuotationProfiles.Where(w => w.i_QuotationProfileId == item.i_QuotationProfileId).FirstOrDefault();
                        //o.i_ServiceTypeId = item.i_ServiceTypeId;
                        //o.v_ProfileName = item.v_ProfileName;
                        //o.d_UpdateDate = DateTime.UtcNow;
                        //o.i_UpdateUserId = item.i_UpdateUserId;
                        //entityDb.QuotationProfiles.Add(o);
                    }
                    if (item.RecordType == RecordType.NoTemporal && item.RecordStatus == RecordStatus.EliminadoLogico)
                    {
                        //var o = entityDb.QuotationProfiles.Where(w => w.i_QuotationProfileId == item.i_QuotationProfileId).FirstOrDefault();
                        //o.i_IsDeleted = YesNo.Yes;
                        //o.d_UpdateDate = DateTime.UtcNow;
                        //o.i_UpdateUserId = item.i_UpdateUserId;
                        //entityDb.QuotationProfiles.Add(o);
                    }
                  
                }
            }
            catch (Exception ex)
            {

                throw;
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
                    o.i_AgeConditionalId = component.i_AgeConditionalId;
                    o.i_Age = component.i_Age;
                    o.i_GenderConditionalId = component.i_GenderConditionalId;
                    o.i_IsDeleted = YesNo.No;
                    quotationProfile.ProfileComponents.Add(o);
                }
                if (component.RecordType == RecordType.NoTemporal && (component.RecordStatus == RecordStatus.Modificado || component.RecordStatus == RecordStatus.Grabado))
                {
                    var o = quotationProfile.ProfileComponents.Where(w => w.i_ProfileComponentId == component.i_ProfileComponentId).FirstOrDefault();
                    o.r_SalePrice = component.r_SalePrice;
                    o.d_UpdateDate = DateTime.UtcNow;
                    o.i_AgeConditionalId = component.i_AgeConditionalId;
                    o.i_Age = component.i_Age;
                    o.i_GenderConditionalId = component.i_GenderConditionalId;
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

        public async Task<IEnumerable<QuotationVersionModel>> GetVersions(string code)
        {
            var query = await (from A in _context.Quotation
                               join B in _context.Company on A.i_CompanyId equals B.i_CompanyId
                               join C in _context.SystemParameter on new { a = A.i_StatusQuotationId.Value, b = 103 }
                                                               equals new { a = C.i_ParameterId, b = C.i_GroupId } into C_join
                               from C in C_join.DefaultIfEmpty()
                               where A.i_IsDeleted == 0 && A.v_Code == code
                               orderby A.d_ShippingDate descending
                               select new QuotationVersionModel
                               {
                                   QuotationId = A.i_QuotationId,
                                   NroQuotation = A.v_Code ,
                                   IsProccess = A.i_IsProccess,
                                   Version = A.i_Version,
                                   ShippingDate = A.d_ShippingDate,                                   
                                   CompanyName = B.v_Name,
                                   Total = A.r_TotalQuotation,
                                   StatusQuotationId = A.i_StatusQuotationId.Value,
                                   StatusQuotationName = C.v_Value1,
                               }).ToListAsync();

            return query;
        }

        public async Task<IEnumerable<QuotationFilterModel>> GetFilterAsync(ParamsQuotationFilterDto parameters)
        {
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "yyyy-MM-dd"};

            string nroQuotation = string.IsNullOrWhiteSpace(parameters.NroQuotation) ? null : parameters.NroQuotation;
            string companyName = string.IsNullOrWhiteSpace(parameters.CompanyName) ? null : parameters.CompanyName;
            var statusQuotationId =  parameters.StatusQuotationId.ToList();
            bool validfi = DateTime.TryParseExact(parameters.StartDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fi);
            bool validff = DateTime.TryParseExact(parameters.EndDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ff);

            var query = await(from A in _context.Quotation
                              join B in _context.Company on A.i_CompanyId equals B.i_CompanyId
                              join C in _context.SystemParameter on new { a = A.i_StatusQuotationId.Value, b = 103 }
                                                                                               equals new { a = C.i_ParameterId, b = C.i_GroupId } into C_join
                              from C in C_join.DefaultIfEmpty()
                              where A.i_IsDeleted == 0
                              && (companyName ==null || B.v_Name.Contains(companyName) || B.v_IdentificationNumber.Contains(companyName))                              
                              && (nroQuotation == null || A.v_Code.Contains(nroQuotation))
                              //&&(statusQuotationId == -1 || A.i_StatusQuotationId == statusQuotationId)
                              && (statusQuotationId.Contains(A.i_StatusQuotationId.Value))
                              && (!validfi || A.d_InsertDate >= fi)
                              && (!validff || A.d_InsertDate <= ff)
                              && (A.i_IsProccess == YesNo.Yes)
                              orderby A.d_ShippingDate descending
                              select new QuotationFilterModel
                              {
                                  QuotationId = A.i_QuotationId,
                                  NroQuotation = A.v_Code + " v.  " + A.i_Version,
                                  ShippingDate = A.d_ShippingDate,
                                  AcceptanceDate = A.d_AcceptanceDate,
                                  CompanyName = B.v_Name,
                                  Total = A.r_TotalQuotation,
                                  Indicator = GetIndicator((from A2 in _context.QuoteTracking
                                                            where A2.i_QuotationId == A.i_QuotationId && A2.i_IsDeleted == YesNo.No
                                                            orderby A2.d_Date descending
                                                            select A2).FirstOrDefault().d_Date),
                                  USDate = (from A2 in _context.QuoteTracking
                                            where A2.i_QuotationId == A.i_QuotationId && A2.i_IsDeleted == YesNo.No
                                            orderby A2.d_Date descending select A2).FirstOrDefault().d_Date,
                                  TrackingDescription = (from A2 in _context.QuoteTracking
                                                         where A2.i_QuotationId == A.i_QuotationId && A2.i_IsDeleted == YesNo.No
                                                         orderby A2.d_Date descending
                                                         select A2).FirstOrDefault().v_Commentary,
                                  StatusQuotationId = A.i_StatusQuotationId.Value,
                                  StatusQuotationName = C.v_Value1,
                                  QuoteTrackings = (from A1 in _context.QuoteTracking
                                                    join B1 in _context.Quotation on A1.i_QuotationId equals B1.i_QuotationId
                                                        //where A1.i_QuotationId == A.i_QuotationId && A1.i_IsDeleted == YesNo.No
                                                    where B1.v_Code == A.v_Code && A1.i_IsDeleted == YesNo.No
                                                    orderby A1.d_Date descending
                                                    select new QuoteTrackingFilterModel { 
                                                        Commentary =  A1.v_Commentary,
                                                        Version = B1.i_Version,
                                                        Date =  A1.d_Date,
                                                        QuotationId =  A1.i_QuotationId,
                                                        QuoteTrackingId =  A1.i_QuoteTrackingId,
                                                        StatusName = A1.v_StatusName
                                                    }).ToList()
                              }).ToListAsync();

            return query;
        }

        private string GetIndicator(DateTime? shippingDate)
        {
            if (shippingDate == null) return "";

            var diff = (DateTime.Now - shippingDate.Value).TotalDays;

            if (diff <= 10)
            {
                return "GREEN";
            }
            else if (diff > 10 && diff <= 20)
            {
                return "AMBER";
            }
            else if (diff > 21)
            {
                return "RED";
            }

            return "";
        }
               
        public Task<IEnumerable<Quotation>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateIsProccess(string code, int quotationId)
        {
            try
            {
                var entitiesDb = await _dbSet.Where(u => u.v_Code == code).ToListAsync();

                foreach (var item in entitiesDb)
                {
                    if (item.i_QuotationId != quotationId)
                    {
                        item.i_IsProccess = YesNo.No;
                    }
                    else
                    {
                        item.i_IsProccess = YesNo.Yes;
                    }

                    await _context.SaveChangesAsync();
                }

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
                                
        }

        private int GetLastVersion(string code)
        {
            var sql = (from A in _context.Quotation
                       where A.v_Code == code
                       orderby A.i_Version descending
                       select A).FirstOrDefault();
            if (sql != null)
            {
                return sql.i_Version;
            }
            return 0;
        }
    }
}
