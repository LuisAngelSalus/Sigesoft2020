using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SL.Sigesoft.Data.Contracts
{
   public interface ISystemUserRepository :IGenericRepository<SystemUser>
    {
        Task<bool> ChangePassword(SystemUser systemUser);
        Task<bool> ChangeProfile(SystemUser systemUser);
        Task<bool> ValidatePassword(SystemUser systemUser);
        Task<(bool result, SystemUserLoginModel systemUser)> ValidateLogin(SystemUser systemUser);
        Task<AccessSysteUserModelDto> GetAccess(int id);
        Task<bool> UpdateAccess(List<UpdateAccessModel> updateAccessDto);
    }
}
