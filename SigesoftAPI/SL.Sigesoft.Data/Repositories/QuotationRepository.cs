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
using SL.Sigesoft.Models.Win;
using SL.Sigesoft.Data.Contracts.Win;

namespace SL.Sigesoft.Data.Repositories
{
    public class QuotationRepository : IQuotationRepository
    {
        private ISecuentialRespository _secuentialRespository;
        private IProtocolRepository _protocolRepository;
        private IProtocolDetailRepository _protocolDetailRepository;
        private IInterfaceSigesoftWinRepository _interfaceSigesoftWinRepository;
        private ICompanyRepository _companyRepository;
        private readonly SigesoftCoreContext _context;
        private readonly SigesoftWinContext _contextWin;
        private readonly ILogger<QuotationRepository> _logger;
        private DbSet<Quotation> _dbSet;
        private DbSet<QuotationProfile> _dbSetQuotationProfile;

        public QuotationRepository(SigesoftCoreContext context,
          ILogger<QuotationRepository> logger, ISecuentialRespository secuentialRespository, IProtocolRepository protocolRepository, IProtocolDetailRepository protocolDetailRepository, IInterfaceSigesoftWinRepository interfaceSigesoftWinRepository, SigesoftWinContext contextWin, ICompanyRepository companyRepository)
        {
            this._context = context;
            this._contextWin = contextWin;
            this._logger = logger;
            this._dbSet = _context.Set<Quotation>();
            this._dbSetQuotationProfile = _context.Set<QuotationProfile>();
            this._secuentialRespository = secuentialRespository;
            this._protocolRepository = protocolRepository;
            this._protocolDetailRepository = protocolDetailRepository;
            this._interfaceSigesoftWinRepository = interfaceSigesoftWinRepository;
            this._companyRepository = companyRepository;
        }

        public async Task<Quotation> AddAsync(Quotation entity)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                #region Code
                entity.v_Code = Utils.Code("COT", entity.i_ResponsibleSystemUserId.ToString(), await _secuentialRespository.GetCode(Constants.PROC_REG_QUOTATION, entity.i_ResponsibleSystemUserId, 1));
                entity.i_Version = 1;
                entity.i_IsProccess = YesNo.Yes;
                #endregion

                if (entity.i_StatusQuotationId == (int)StatusQuotation.Seguimiento)
                    entity.d_ShippingDate = DateTime.UtcNow;

                #region AUDIT
                entity.i_IsDeleted = YesNo.No;
                entity.d_InsertDate = DateTime.UtcNow;
                entity.i_InsertUserId = entity.i_InsertUserId;

                foreach (var item in entity.QuotationProfile)
                {
                    #region AUDIT
                    item.i_IsDeleted = YesNo.No;
                    item.d_InsertDate = DateTime.UtcNow;
                    item.i_InsertUserId = entity.i_InsertUserId;
                    #endregion
                    foreach (var item2 in item.ProfileComponent)
                    {
                        #region AUDIT
                        item2.i_IsDeleted = YesNo.No;
                        item2.d_InsertDate = DateTime.UtcNow;
                        item2.i_InsertUserId = entity.i_InsertUserId;
                        #endregion
                    }
                }

                foreach (var item in entity.AdditionalComponentsQuote)
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
                    //throw new Exception("Test Exception");
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error en {nameof(AddAsync)}: " + ex.Message);
                    transaction.Rollback();
                    return null;
                }

                return entity;
            }
        }

        public async Task<Quotation> NewVersion(Quotation entity)
        {

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                #region AUDIT
                entity.i_Version = GetLastVersion(entity.v_Code) + 1;

                if (entity.i_StatusQuotationId == (int)StatusQuotation.Seguimiento)
                    entity.d_ShippingDate = DateTime.UtcNow;

                entity.i_IsDeleted = YesNo.No;
                entity.d_InsertDate = DateTime.UtcNow;
                entity.i_InsertUserId = entity.i_InsertUserId;

                foreach (var item in entity.QuotationProfile)
                {
                    #region AUDIT
                    item.i_IsDeleted = YesNo.No;
                    item.d_InsertDate = DateTime.UtcNow;
                    item.i_InsertUserId = entity.i_InsertUserId;
                    #endregion
                    foreach (var item2 in item.ProfileComponent)
                    {
                        #region AUDIT
                        //ELIMINADO LÓGICO DE UN PERFIL COMPONENT
                        if (item2.RecordStatus == RecordStatus.EliminadoLogico)                        
                            item2.i_IsDeleted = YesNo.Yes;                        
                        else
                            item2.i_IsDeleted = YesNo.No;

                        item2.d_InsertDate = DateTime.UtcNow;
                        item2.i_InsertUserId = entity.i_InsertUserId;
                        #endregion
                    }
                }

                foreach (var item in entity.AdditionalComponentsQuote)
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
                    //throw new Exception("Test Exception");
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error en {nameof(NewVersion)}: " + ex.Message);
                    transaction.Rollback();
                    return null;
                }
                return entity;

            }
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
                                   join C in _context.SystemUser on A.i_ResponsibleSystemUserId equals C.i_SystemUserId
                                   where A.i_QuotationId == id && A.i_IsDeleted == YesNo.No
                                   select new QuotationModel
                                   {
                                       QuotationId = A.i_QuotationId,
                                       Code = A.v_Code,
                                       Version = A.i_Version,
                                       ResponsibleSystemUserId = A.i_ResponsibleSystemUserId,
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
                                       StatusQuotationId = A.i_StatusQuotationId,
                                       QuotationProfile = (from A1 in _context.QuotationProfile
                                                            join C1 in _context.SystemParameter on new { a = A1.i_ServiceTypeId, b = 101 }
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
                                                                ProfileComponent = (from A2 in _context.ProfileComponent
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

                                       AdditionalComponentsQuote = (from A3 in _context.AdditionalComponentsQuote
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
            var entityDb = await _dbSet.Include(i => i.QuotationProfile)
                                    //.ThenInclude(i => i.ProfileComponents)
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


                //UpdateAddittionalExamn(entity.AdditionalComponentsQuote, entityDb);

                #region QuotationProfiles
                //UpdateQuotationProfiles(entity.QuotationProfile, entityDb);
                #endregion

            }

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
                _logger.LogError($"Error en {nameof(UpdateAsync)}: " + ex.Message);
            }
            return false;
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
                    quotationProfile.ProfileComponent.Add(o);
                }
                if (component.RecordType == RecordType.NoTemporal && (component.RecordStatus == RecordStatus.Modificado || component.RecordStatus == RecordStatus.Grabado))
                {
                    var o = quotationProfile.ProfileComponent.Where(w => w.i_ProfileComponentId == component.i_ProfileComponentId).FirstOrDefault();
                    o.r_SalePrice = component.r_SalePrice;
                    o.d_UpdateDate = DateTime.UtcNow;
                    o.i_AgeConditionalId = component.i_AgeConditionalId;
                    o.i_Age = component.i_Age;
                    o.i_GenderConditionalId = component.i_GenderConditionalId;
                    o.i_UpdateUserId = component.i_UpdateUserId;
                    quotationProfile.ProfileComponent.Add(o);
                }
                if (component.RecordType == RecordType.NoTemporal && component.RecordStatus == RecordStatus.EliminadoLogico)
                {
                    var o = quotationProfile.ProfileComponent.Where(w => w.i_ProfileComponentId == component.i_ProfileComponentId).FirstOrDefault();
                    o.i_IsDeleted = YesNo.Yes;
                    o.d_UpdateDate = DateTime.UtcNow;
                    o.i_UpdateUserId = component.i_UpdateUserId;
                    quotationProfile.ProfileComponent.Add(o);
                }
            }           
        }

        public async Task<IEnumerable<QuotationVersionModel>> GetVersions(string code)
        {
            var query = await (from A in _context.Quotation
                               join B in _context.Company on A.i_CompanyId equals B.i_CompanyId
                               join C in _context.SystemParameter on new { a = A.i_StatusQuotationId, b = 103 }
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
                                   StatusQuotationId = A.i_StatusQuotationId,
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
            int? responsibleSystemUser = parameters.ResponsibleSystemUserId;

            var query = await(from A in _context.Quotation
                              join B in _context.Company on A.i_CompanyId equals B.i_CompanyId
                              join C in _context.SystemParameter on new { a = A.i_StatusQuotationId, b = 103 }
                                    equals new { a = C.i_ParameterId, b = C.i_GroupId } into C_join
                              from C in C_join.DefaultIfEmpty()
                              where A.i_IsDeleted == 0
                              && (A.i_ResponsibleSystemUserId == responsibleSystemUser)
                              && (companyName ==null || B.v_Name.Contains(companyName) || B.v_IdentificationNumber.Contains(companyName))                              
                              && (nroQuotation == null || A.v_Code.Contains(nroQuotation))
                              && (statusQuotationId.Contains(A.i_StatusQuotationId))
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
                                  Email =  A.v_Email,
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
                                  StatusQuotationId = A.i_StatusQuotationId,
                                  StatusQuotationName = C.v_Value1,
                                  QuoteTracking = (from A1 in _context.QuoteTracking
                                                    join B1 in _context.Quotation on A1.i_QuotationId equals B1.i_QuotationId                                                        
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
                    #region AUDIT            
                    item.d_UpdateDate = DateTime.UtcNow;
                    //item.i_UpdateUserId = entity.i_UpdateUserId;
                    #endregion

                    await _context.SaveChangesAsync();
                }

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
                                
        }

        public async Task<bool> MigrateQuotationToProtocols(int quotationId)
        {
            try
            {
                var quotation = await _dbSet.Include(i => i.QuotationProfile)
                            .ThenInclude(p => p.ProfileComponent)
                            .Where(w => w.i_QuotationId == quotationId).FirstOrDefaultAsync();

                foreach (var profile in quotation.QuotationProfile)
                {
                    var newProtocol = new Protocol();
                    newProtocol.i_CompanyId = quotation.i_CompanyId;
                    newProtocol.i_QuotationId = quotationId;
                    newProtocol.v_ProtocolName = profile.v_ProfileName;
                    newProtocol.i_ServiceTypeId = profile.i_ServiceTypeId;
                    newProtocol.i_TypeFormatId = profile.i_TypeFormatId;
                    newProtocol.i_QuotationProfileIdRef = profile.i_QuotationProfileId;

                    foreach (var detail in profile.ProfileComponent)
                    {
                        var newProtocolDetail = new ProtocolDetail();
                        newProtocolDetail.i_CategoryId = detail.i_CategoryId;
                        newProtocolDetail.v_CategoryName = detail.v_CategoryName;
                        newProtocolDetail.v_ComponentId = detail.v_ComponentId;
                        newProtocolDetail.v_ComponentName = detail.v_ComponentName;
                        newProtocolDetail.r_MinPrice = detail.r_MinPrice;
                        newProtocolDetail.r_PriceList = detail.r_PriceList;
                        newProtocolDetail.r_SalePrice = detail.r_SalePrice;
                        newProtocolDetail.i_AgeConditionalId = detail.i_AgeConditionalId;
                        newProtocolDetail.i_Age = detail.i_Age;
                        newProtocolDetail.i_GenderConditionalId = detail.i_GenderConditionalId;
                        newProtocolDetail.i_QuotationProfileIdRef = detail.i_QuotationProfileId;

                        newProtocol.ProtocolDetail.Add(newProtocolDetail);
                    }

                    _context.Protocol.Add(newProtocol);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }           

            return true;
        }

        public async Task<List<ListTrackingChartModel>> Trackingchart(ParamsTrackingChartModel trackingchartdto)
        {
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "yyyy-MM-dd" };
            bool validfi = DateTime.TryParseExact(trackingchartdto.StartDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fi);
            bool validff = DateTime.TryParseExact(trackingchartdto.EndDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ff);
            return await (from A in _context.Quotation
                          where A.i_ResponsibleSystemUserId == trackingchartdto.ResponsibleSystemUserId
                                && (!validfi || A.d_InsertDate >= fi)
                                && (!validff || A.d_InsertDate <= ff)
                                && (A.i_IsProccess == YesNo.Yes)
                                && A.i_IsDeleted == YesNo.No
                          select new ListTrackingChartModel
                          {
                              StatusQuotationId = A.i_StatusQuotationId
                          }).ToListAsync();
        }

        private void InsertProtocolDetail(int protocolId, ICollection<ProfileComponent> profileComponents)
        {
            foreach (var detail in profileComponents)
            {
                var newProtocolDetail = new ProtocolDetail();
                newProtocolDetail.i_ProtocolId = protocolId;
                newProtocolDetail.i_CategoryId = detail.i_CategoryId;
                newProtocolDetail.v_CategoryName = detail.v_CategoryName;
                newProtocolDetail.v_ComponentId = detail.v_ComponentId;
                newProtocolDetail.v_ComponentName = detail.v_ComponentName;
                newProtocolDetail.r_MinPrice = detail.r_MinPrice;
                newProtocolDetail.r_PriceList = detail.r_PriceList;
                newProtocolDetail.r_SalePrice = detail.r_SalePrice;
                newProtocolDetail.i_AgeConditionalId = detail.i_AgeConditionalId;
                newProtocolDetail.i_Age = detail.i_Age;
                newProtocolDetail.i_GenderConditionalId = detail.i_GenderConditionalId;
                newProtocolDetail.i_QuotationProfileIdRef = detail.i_QuotationProfileId;

                _protocolDetailRepository.AddAsync(newProtocolDetail);
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

        public async Task<bool> MigrateoProtocolToSIGESoftWin(int quotationId, int systemUserId)
        {
            try
            {            
                var quotation = await _dbSet.Include(i => i.QuotationProfile)
                              .ThenInclude(p => p.ProfileComponent)
                              .Include(i => i.AdditionalComponentsQuote)
                              .Where(w => w.i_QuotationId == quotationId).FirstOrDefaultAsync();

                foreach (var profile in quotation.QuotationProfile)
                {
                    var companyDbWeb = await _companyRepository.GetAsync(quotation.i_CompanyId);                 
                    var organizationProcessed = await _interfaceSigesoftWinRepository.ProcessOrganization(companyDbWeb.v_IdentificationNumber, systemUserId);
                    
                    var newProtocol = new ProtocolWin();
                    newProtocol.v_ProtocolId =  Utils.GetNewIdWin(Constants.NODE_SIGESOFT2020, await _interfaceSigesoftWinRepository.GetNextSecuentialId(Constants.NODE_SIGESOFT2020, Constants.SIGESOFTWIN_TABLE_PROTOCOL), "PR");
                    newProtocol.v_Name = profile.v_ProfileName;
                    newProtocol.v_EmployerOrganizationId = organizationProcessed.v_OrganizationId;
                    newProtocol.v_EmployerLocationId = organizationProcessed.v_LocationId;
                    newProtocol.i_EsoTypeId = profile.i_ServiceTypeId;
                    newProtocol.v_GroupOccupationId = organizationProcessed.v_GroupOccupationId;
                    newProtocol.v_CustomerOrganizationId = organizationProcessed.v_OrganizationId;
                    newProtocol.v_CustomerLocationId = organizationProcessed.v_LocationId;
                    newProtocol.v_WorkingOrganizationId = organizationProcessed.v_OrganizationId;
                    newProtocol.v_WorkingLocationId = organizationProcessed.v_LocationId;
                    newProtocol.i_MasterServiceId = Constants.SIGESOFTWIN_MASTER_SERVICE;
                    newProtocol.v_CostCenter = "";
                    newProtocol.i_MasterServiceTypeId = Constants.SIGESOFTWIN_MASTER_SERVICE_TYPE;
                    newProtocol.i_HasVigency = 0;
                    newProtocol.i_ValidInDays = null;
                    newProtocol.i_IsActive = (int)YesNo.Yes;
                    newProtocol.i_TypeReport = EmologarTypeFormat(profile.i_TypeFormatId);

                    newProtocol.i_IsDeleted = YesNo.Yes;
                    newProtocol.i_InsertUserId = EmologarSystemUser(systemUserId);
                    newProtocol.d_InsertDate = DateTime.Now;

                    _contextWin.Add(newProtocol);
                    await _contextWin.SaveChangesAsync();

                    await InsertProtocolComponent(newProtocol.v_ProtocolId, profile.ProfileComponent, quotation.AdditionalComponentsQuote, systemUserId);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private int EmologarTypeFormat(int typeFormatId)
        {
            if (typeFormatId == 1)
                return 1;
            else if (typeFormatId == 2)
                return 2;
            else if (typeFormatId == 3)
                return 6;
            else if (typeFormatId == 4)
                return 3;
            else if (typeFormatId == 5)
                return 4;            
            else
                return -1;
        }

        private int EmologarOperator(int operatorId)
        {
            if (operatorId == 1)
                return 5;
            else if (operatorId == 2)
                return 3;            
            else
                return -1;
        }

        private int EmologarSystemUser(int systemUserId)
        {          
            if (systemUserId == 8)
            {
                return 123;
            }else if (systemUserId == 7)
            {
                return 127;
            }else if (systemUserId == 9)
            {
                return 145;
            }
            else if (systemUserId == 14)
            {
                return 122;
            }

            return 11;
        }

        private async Task<bool> InsertProtocolComponent(string protocolId, ICollection<ProfileComponent> profileComponents, ICollection<AdditionalComponentsQuote> additionalComponentsQuote, int systemUserId)
        {
            try
            {
                foreach (var detail in profileComponents)
                {
                    var newProtocolDetail = new ProtocolComponentWin();
                    newProtocolDetail.v_ProtocolComponentId = Utils.GetNewIdWin(Constants.NODE_SIGESOFT2020, await _interfaceSigesoftWinRepository.GetNextSecuentialId(Constants.NODE_SIGESOFT2020, Constants.SIGESOFTWIN_TABLE_PROTOCOL_COMPONENT), "PC");
                    newProtocolDetail.v_ComponentId = detail.v_ComponentId;
                    newProtocolDetail.v_ProtocolId = protocolId;
                    newProtocolDetail.r_Price = detail.r_SalePrice;

                    if (detail.i_AgeConditionalId != -1 || detail.i_GenderConditionalId !=-1)
                        newProtocolDetail.i_IsConditionalId = YesNo.Yes ;

                    newProtocolDetail.i_OperatorId = EmologarOperator(detail.i_AgeConditionalId.Value);
                    newProtocolDetail.i_Age = detail.i_Age == null ? 0 : detail.i_Age.Value;
                    newProtocolDetail.i_GenderId = detail.i_GenderConditionalId.Value;                    

                    newProtocolDetail.i_IsDeleted = YesNo.No;
                    newProtocolDetail.d_InsertDate = DateTime.Now;
                    newProtocolDetail.i_InsertUserId = systemUserId;
                    _contextWin.Add(newProtocolDetail);
                }

                foreach (var item in additionalComponentsQuote)
                {
                    var newProtocolDetail = new ProtocolComponentWin();
                    newProtocolDetail.v_ProtocolComponentId = Utils.GetNewIdWin(Constants.NODE_SIGESOFT2020, await _interfaceSigesoftWinRepository.GetNextSecuentialId(Constants.NODE_SIGESOFT2020, Constants.SIGESOFTWIN_TABLE_PROTOCOL_COMPONENT), "PC");
                    newProtocolDetail.v_ComponentId = item.v_ComponentId;
                    newProtocolDetail.v_ProtocolId = protocolId;
                    newProtocolDetail.r_Price = item.r_SalePrice;
                    newProtocolDetail.i_IsConditionalId = YesNo.No;
                    newProtocolDetail.i_IsDeleted = YesNo.No;
                    newProtocolDetail.d_InsertDate = DateTime.Now;
                    newProtocolDetail.i_InsertUserId = systemUserId;

                    _contextWin.Add(newProtocolDetail);                    
                }
                await _contextWin.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;     
            }

            return true;
            
        }
    }
}
