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
    public class SystemUserRepository : ISystemUserRepository
    {
        private readonly SigesoftCoreContext _context;
        private readonly ILogger<SystemUserRepository> _logger;
        private readonly IPasswordHasher<SystemUser> _passwordHasher;
        private DbSet<SystemUser> _dbSet;

        public SystemUserRepository(SigesoftCoreContext context,
            ILogger<SystemUserRepository> logger,
            IPasswordHasher<SystemUser> passwordHasher)
        {
            this._context = context;
            this._logger = logger;
            this._passwordHasher = passwordHasher;
            this._dbSet = _context.Set<SystemUser>();
        }

        #region CRUD
        public async Task<SystemUser> AddAsync(SystemUser entity)
        {
            entity.i_IsDeleted = YesNo.No;
            entity.v_Password = _passwordHasher.HashPassword(entity, entity.v_Password);
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
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.SingleOrDefaultAsync(u => u.i_SystemUserId == id);
            entity.i_IsDeleted = YesNo.Yes;
            try
            {
                return (await _context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(DeleteAsync)}: " + ex.Message);
            }
            return false;
        }
        public async Task<IEnumerable<SystemUser>> GetAllAsync()
        {
            return await _dbSet.Include(su => su.Permissions)
                               .Where(u => u.i_IsDeleted == YesNo.No)
                               .ToListAsync();
        }
        public async Task<SystemUser> GetAsync(int id)
        {
            return await _dbSet.Include(per => per.Person)                               
                                .SingleOrDefaultAsync(c => c.i_SystemUserId == id && c.i_IsDeleted == YesNo.No);
        }
        public async Task<bool> UpdateAsync(SystemUser systemUser)
        {
            var systemUserDb = await _dbSet.FirstOrDefaultAsync(u => u.i_SystemUserId == systemUser.i_SystemUserId);

            if (systemUserDb == null)
            {
                _logger.LogError($"Error en {nameof(UpdateAsync)}: No existe el usuario con Id: {systemUser.i_SystemUserId}");
                return false;
            }

            systemUserDb.v_UserName = systemUser.v_UserName;
            systemUserDb.v_Email = systemUser.v_Email;
            systemUserDb.v_Phone = systemUser.v_Phone;
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
        #endregion

        public async Task<bool> ChangePassword(SystemUser systemUser)
        {
            var systemUserDb = await _dbSet.FirstOrDefaultAsync(u => u.i_SystemUserId == systemUser.i_SystemUserId);
            systemUserDb.v_Password = _passwordHasher.HashPassword(systemUserDb, systemUser.v_Password);
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
        public Task<bool> ChangeProfile(SystemUser systemUser)
        {
            throw new NotImplementedException();
        }        
        public Task<bool> ValidatePassword(SystemUser systemUser)
        {
            throw new NotImplementedException();
        }
        public async Task<(bool result, SystemUser systemUser)> ValidateLogin_(SystemUser systemUser)
        {
            var systemUserDb = await _dbSet.Include(u => u.Permissions).FirstOrDefaultAsync(u => u.v_UserName == systemUser.v_UserName);
            if (systemUserDb != null)
            {
                try
                {
                    var resultado = _passwordHasher.VerifyHashedPassword(systemUserDb, systemUserDb.v_Password, systemUser.v_Password);
                    return (resultado == PasswordVerificationResult.Success ? true : false, systemUserDb);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error en {nameof(ValidateLogin_)}: " + ex.Message);
                }

            }
            return (false, null);
        }
        public async Task<AccessSysteUserModelDto> GetAccess(int id)
        {           
            var query = await  (from A in _context.SystemUser
                             join B in _context.Permission on A.i_SystemUserId equals B.i_SystemUserId
                             join C in _context.Access on B.i_PermissionId equals C.i_PermissionId
                             join D in _context.OwnerCompany on C.i_OwnerCompanyId equals D.i_OwnerCompanyId
                             join E in _context.Role on B.i_RoleId equals E.i_RoleId
                             join F in  _context.Profile on E.i_RoleId equals F.i_RoleId
                             join G in _context.ApplicationHierarchy on F.i_ApplicationHierarchyId equals   G.i_ApplicationHierarchyId
                             join H in _context.Person on A.i_PersonId equals H.i_PersonId
                             where A.i_SystemUserId == id
                             select new
                             {
                                 SystemUserId = A.i_SystemUserId,
                                 UserName = A.v_UserName,
                                 FullName = H.v_FirstName + " " + H.v_FirstLastName + " " + H.v_SecondLastName,
                                 CompanyId = D.i_OwnerCompanyId,
                                 CompanyName = D.v_BusinessName,
                                 RolId = E.i_RoleId,
                                 RolName = E.v_Description,
                                 ApplicationHierarchyId = G.i_ApplicationHierarchyId,
                                 ApplicationHierarchyName = G.v_Description,
                                 ParentId = G.i_ParentId
                             }
                            ).ToListAsync();

            var oAccessSysteUserModelDto = new AccessSysteUserModelDto();
            oAccessSysteUserModelDto.SystemUserId = query[0].SystemUserId;
            oAccessSysteUserModelDto.UserName = query[0].UserName;
            oAccessSysteUserModelDto.FullName = query[0].FullName;

            var companiesDb = query.GroupBy(g => g.CompanyId).Select(s => s.First()).ToList();
            var companies = new List<Companies>();
            foreach (var itemComp in companiesDb)
            {
                var oCompanies = new Companies{CompanyId = itemComp.CompanyId, CompanyName = itemComp.CompanyName };                

                var rolesDb = query.Where(w => w.CompanyId == oCompanies.CompanyId).GroupBy(g => g.RolId).Select(s => s.First()).ToList();
                var roles = new List<Roles>();
                foreach (var itemRol in rolesDb)
                {
                    var oRole = new Roles { RolId = itemRol.RolId, RolName= itemRol.RolName };
                    roles.Add(oRole);

                    var ModulesDb = query.Where(w => w.RolId == oRole.RolId && w.ParentId == -1).GroupBy(g => g.RolId).Select(s => s.First()).ToList();
                    var modules = new List<Module>();
                    foreach (var itemModule in ModulesDb)
                    {
                        var oModule = new Module { ModuleId = itemModule.ApplicationHierarchyId , ModuleName = itemModule.ApplicationHierarchyName};
                        modules.Add(oModule);

                        var OptionsDb = query.Where(w => w.RolId == oRole.RolId && w.ParentId != -1).GroupBy(g => g.ApplicationHierarchyId).Select(s => s.First()).ToList();
                        var Options = new List<Option>();

                        foreach (var itemOption in OptionsDb)
                        {
                            var oOption = new Option { OptionId = itemOption.ApplicationHierarchyId, OptionName= itemOption.ApplicationHierarchyName };
                            Options.Add(oOption);
                        }
                        oModule.Options = Options;
                    }
                    oRole.Modules = modules;

                }

                oCompanies.Roles = roles;
                companies.Add(oCompanies);
            }
            oAccessSysteUserModelDto.Companies = companies;


            

            return oAccessSysteUserModelDto;
        }

       public async Task<(bool result, SystemUserLoginModel systemUser)> ValidateLogin(SystemUser systemUser)
        {
            var systemUserDb = await _dbSet.Include(u => u.Permissions).FirstOrDefaultAsync(u => u.v_UserName == systemUser.v_UserName);
            
            if (systemUserDb != null)
            {
                try
                {
                    var resultado = _passwordHasher.VerifyHashedPassword(systemUserDb, systemUserDb.v_Password, systemUser.v_Password);

                    var systemUserModel = await (from A in _context.SystemUser  
                                                 where A.v_UserName == systemUser.v_UserName && A.i_IsDeleted == YesNo.No
                                              select new SystemUserLoginModel
                                              {
                                                  UserName = A.v_UserName,
                                                  SystemUserId = A.i_SystemUserId,
                                                  Roles =  (from A1 in _context.Permission 
                                                            join B1  in _context.Role on A1.i_RoleId equals B1.i_RoleId
                                                            where A1.i_IsDeleted == YesNo.No && A1.i_SystemUserId == A.i_SystemUserId
                                                            group B1.v_Description by B1.v_Description into g
                                                            select new RoleModel { 
                                                                RolName =g.Key
                                                            }).ToList()
                                              }).FirstOrDefaultAsync();

                    return (resultado == PasswordVerificationResult.Success ? true : false, systemUserModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error en {nameof(ValidateLogin)}: " + ex.Message);
                }

            }
            return (false, null);
        }
    }
}
