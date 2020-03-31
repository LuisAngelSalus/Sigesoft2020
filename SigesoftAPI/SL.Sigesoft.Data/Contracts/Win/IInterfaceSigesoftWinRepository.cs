using SL.Sigesoft.Models.Win;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SL.Sigesoft.Data.Contracts.Win
{
   public interface IInterfaceSigesoftWinRepository
    {
        Task<List<SL.Sigesoft.Models.Win.Component>> GetAllAsync();
        Task<List<SL.Sigesoft.Models.Win.Component>> GetByNameAsync(string value);
        Task<int> GetNextSecuentialId(int pintNodeId, int pintTableId);
        Task<ProcessedOrganizationWin> ProcessOrganization(string ruc, int systemUserId);
        Task<bool> AddOrganizationFromSigesoft2020(OrganizationWinRegisterDto organizationWinRegisterDto);
    }
}
