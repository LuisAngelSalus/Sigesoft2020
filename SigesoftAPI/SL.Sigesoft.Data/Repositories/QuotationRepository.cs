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
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Quotation>> GetAllAsync()
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
                            Version  = A.i_Version,
                            UserCreatedId  = A.i_UserCreatedId,
                            UserName = C.v_UserName,
                            CompanyId  = A.i_CompanyId,
                            CompanyName = B.v_Name,
                            CompanyDistrictName = B.v_District,
                            CompanyAddress = B.v_Address,
                            CompanyHeadquarterId = A.i_CompanyHeadquarterId,
                            FullName = A.v_FullName,
                            Email  = A.v_Email,
                            TypeFormatId  = A.i_TypeFormatId,
                            CommercialTerms  = A.v_CommercialTerms,
                            QuotationProfiles = (from A1 in _context.QuotationProfile
                                                 join B1 in _context.SystemParameter on new {a = A1.i_ProfileId.Value, b = 100}
                                                                                    equals new { a = B1.i_ParameterId, b = B1.i_GroupId } into B1_join
                                                 from B1 in B1_join.DefaultIfEmpty()
                                                 join C1 in _context.SystemParameter on new { a = A1.i_ServiceTypeId.Value, b = 101 }
                                                                                    equals new { a = C1.i_ParameterId, b = C1.i_GroupId } into C1_join
                                                 from C1 in C1_join.DefaultIfEmpty()
                                                 where A1.i_QuotationId == A.i_QuotationId
                                                 select new QuotationProfileModel
                                                  {
                                                    ProfileId = A1.i_ProfileId.Value,
                                                    ProfileName = B1.v_Value1,
                                                    ServiceTypeId = A1.i_ServiceTypeId,
                                                    ServiceTypeName = C1.v_Value1,
                                                    ProfileComponents = (from A2 in _context.ProfileComponent
                                                                        where A2.i_QuotationProfileId == A1.i_QuotationProfileId
                                                                         select new ProfileComponentModel {
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
            throw new NotImplementedException();
        }

    }
}
