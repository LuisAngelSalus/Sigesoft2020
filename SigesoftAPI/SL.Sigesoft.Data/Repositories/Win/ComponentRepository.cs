using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SL.Sigesoft.Common;
using SL.Sigesoft.Data.Contracts;
using SL.Sigesoft.Data.Contracts.Win;
using SL.Sigesoft.Models;
using SL.Sigesoft.Models.Enum;
using SL.Sigesoft.Models.Win;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Sigesoft.Data.Repositories.Win
{
   public class ComponentRepository : IInterfaceSigesoftWinRepository
    {
        private readonly SigesoftWinContext _context;
        private readonly SigesoftCoreContext _contextWeb;
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<ComponentRepository> _logger;
        private DbSet<Component> _dbSet;

        public ComponentRepository(SigesoftWinContext context,
           ILogger<ComponentRepository> logger, SigesoftCoreContext contextWeb, ICompanyRepository companyRepository)
        {
            this._context = context;
            this._logger = logger;
            this._dbSet = _context.Set<Component>();
            this._contextWeb = contextWeb;
            this._companyRepository = companyRepository;            
        }

        public Task<bool> AddOrganizationFromSigesoft2020(OrganizationWinRegisterDto organizationWinRegisterDto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Component>> GetAllAsync()
        {
            var query = await (from A in _context.Component
                               join B in _context.SystemParameter on new { a = A.i_CategoryId.Value, b = 116 }
                                            equals new { a = B.i_ParameterId, b = B.i_GroupId } into B_join
                               from B in B_join.DefaultIfEmpty()
                               where A.i_IsDeleted == YesNo.No
                               select new Component
                               {
                                   v_ComponentId = A.v_ComponentId,
                                   v_Name = A.v_Name,
                                   i_CategoryId = A.i_CategoryId,
                                   v_CategoryName = B.v_Value1,
                                   r_CostPrice = A.r_CostPrice,
                                   r_BasePrice = A.r_BasePrice,
                                   r_SalePrice = A.r_SalePrice
                               }).ToListAsync();            

            return query;
            
        }

        public async Task<List<Component>> GetByNameAsync(string value)
        {
            var query = await (from A in _context.Component
                               join B in _context.SystemParameter on new { a = A.i_CategoryId.Value, b = 116 }
                                            equals new { a = B.i_ParameterId, b = B.i_GroupId } into B_join
                               from B in B_join.DefaultIfEmpty()
                               where A.i_IsDeleted == YesNo.No && A.v_Name.Contains(value)
                               select new Component
                               {
                                   v_ComponentId = A.v_ComponentId,
                                   v_Name = A.v_Name,
                                   i_CategoryId = A.i_CategoryId,
                                   v_CategoryName = B.v_Value1,
                                   r_CostPrice = A.r_CostPrice,
                                   r_BasePrice = A.r_BasePrice,
                                   r_SalePrice = A.r_SalePrice
                               }).ToListAsync();

            return query;

        }

        public async Task<int> GetNextSecuentialId(int pintNodeId, int pintTableId)
        {
            var objSecuential = (from a in _context.SecuentialWin
                                where a.i_TableId == pintTableId && a.i_NodeId == pintNodeId
                                select a).SingleOrDefault();

            // Actualizar el campo con el nuevo valor a efectos de reservar el ID autogenerado para evitar colisiones entre otros nodos
            if (objSecuential != null)
            {
                objSecuential.i_SecuentialId = objSecuential.i_SecuentialId + 1;
            }
            else
            {
                objSecuential = new SecuentialWin();
                objSecuential.i_NodeId = pintNodeId;
                objSecuential.i_TableId = pintTableId;
                objSecuential.i_SecuentialId = 0;
                _context.Add(objSecuential);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error en {nameof(GetNextSecuentialId)}: " + ex.Message);
                }
            }

            return objSecuential.i_SecuentialId;
        }

        public async Task<ProcessedOrganizationWin> ProcessOrganization(string ruc)
        {
            try
            {

                var organizationDbWin = await (from A in _context.OrganizationWin
                                               join B in _context.LocationWin on A.v_OrganizationId equals B.v_OrganizationId
                                               join C in _context.GroupOccupationWin on B.v_LocationId equals C.v_LocationId
                                               where A.v_IdentificationNumber == ruc
                                                   && A.i_IsDeleted == YesNo.No
                                                   && B.i_IsDeleted == YesNo.No
                                               select new ProcessedOrganizationWin
                                               {
                                                   v_OrganizationId = A.v_OrganizationId,
                                                   v_LocationId = B.v_LocationId,
                                                   v_GroupOccupationId = C.v_GroupOccupationId
                                               }).FirstOrDefaultAsync();
                if (organizationDbWin != null)
                    return organizationDbWin;

                var organizationDbWeb = await _companyRepository.GetCompanyByRuc(ruc);

                var oOrganizationWin = new OrganizationWin();
                oOrganizationWin.v_OrganizationId = Utils.GetNewIdWin(Constants.NODE_SIGESOFT2020, await GetNextSecuentialId(Constants.NODE_SIGESOFT2020, Constants.SIGESOFTWIN_TABLE_ORGANIZATION), "OO");
                oOrganizationWin.v_OrganizationPadreId = oOrganizationWin.v_OrganizationId;
                oOrganizationWin.i_OrganizationTypeId = 1; //Empresa Cliente    
                oOrganizationWin.i_SectorTypeId = 41;
                oOrganizationWin.v_IdentificationNumber = organizationDbWeb.v_IdentificationNumber;
                oOrganizationWin.v_Name = organizationDbWeb.v_Name;
                oOrganizationWin.v_Address = organizationDbWeb.v_Address;
                oOrganizationWin.v_PhoneNumber = organizationDbWeb.v_PhoneNumber;
                oOrganizationWin.v_Mail = organizationDbWeb.v_Mail;
                oOrganizationWin.v_ContacName = organizationDbWeb.v_ContactName;
                oOrganizationWin.i_IsDeleted = YesNo.No;
                oOrganizationWin.d_InsertDate = DateTime.Now;
                _context.Add(oOrganizationWin);
                await _context.SaveChangesAsync();

                var oLocationWin = new LocationWin();
                oLocationWin.v_LocationId = Utils.GetNewIdWin(Constants.NODE_SIGESOFT2020, await GetNextSecuentialId(Constants.NODE_SIGESOFT2020, Constants.SIGESOFTWIN_TABLE_ORGANIZATION), "OL");
                oLocationWin.v_OrganizationId = oOrganizationWin.v_OrganizationId;
                oLocationWin.v_Name = organizationDbWeb.CompanyHeadquarter.FirstOrDefault().v_Name;
                oLocationWin.i_IsDeleted = YesNo.No;
                oLocationWin.d_InsertDate = DateTime.Now;
                _context.Add(oLocationWin);
                await _context.SaveChangesAsync();

                var oGroupOccupation = new GroupOccupationWin();
                oGroupOccupation.v_GroupOccupationId = Utils.GetNewIdWin(Constants.NODE_SIGESOFT2020, await GetNextSecuentialId(Constants.NODE_SIGESOFT2020, Constants.SIGESOFTWIN_TABLE_GROUPOCCUPATION), "OG");
                oGroupOccupation.v_LocationId = oLocationWin.v_LocationId;
                oGroupOccupation.v_Name = "ADMIN/OPER";
                oGroupOccupation.i_IsDeleted = YesNo.No;
                oGroupOccupation.d_InsertDate = DateTime.Now;
                _context.Add(oGroupOccupation);
                await _context.SaveChangesAsync();
                
                return new ProcessedOrganizationWin { v_OrganizationId = oOrganizationWin.v_OrganizationId , v_LocationId = oLocationWin.v_LocationId, v_GroupOccupationId = oGroupOccupation.v_GroupOccupationId };

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(ProcessOrganization)}: " + ex.Message);
                return null;
            }                        
        }
    }
}
