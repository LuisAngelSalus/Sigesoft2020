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
        private DbSet<Permission> _dbSetPermission;
        private DbSet<Access> _dbSetAccess;

        public SystemUserRepository(SigesoftCoreContext context,
            ILogger<SystemUserRepository> logger,
            IPasswordHasher<SystemUser> passwordHasher)
        {
            this._context = context;
            this._logger = logger;
            this._passwordHasher = passwordHasher;
            this._dbSet = _context.Set<SystemUser>();
            this._dbSetPermission = _context.Set<Permission>();
            this._dbSetAccess = _context.Set<Access>();
        }

        #region CRUD
        public async Task<SystemUser> AddAsync(SystemUser entity)
        {
            #region AUDIT
            entity.i_IsDeleted = YesNo.No;
            entity.d_InsertDate = DateTime.UtcNow;
            entity.i_InsertUserId = entity.i_InsertUserId;
            #endregion

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

            #region AUDIT
            entity.i_IsDeleted = YesNo.Yes;
            entity.d_UpdateDate = DateTime.UtcNow;
            //entity.i_UpdateUserId = entity.i_UpdateUserId;
            #endregion

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
            return await _dbSet.Include(su => su.Permission)
                               .Include(su => su.Company)
                               .Include(su => su.Notification)                               
                               .Include(su => su.Person)
                               .Include(su => su.Quotation)
                               .Include(su => su.Secuential)
                               .Include(su => su.Suscription)
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
            systemUserDb.v_Password = _passwordHasher.HashPassword(systemUser, systemUser.v_Password);

            #region AUDIT            
            systemUserDb.d_UpdateDate = DateTime.UtcNow;
            systemUserDb.i_UpdateUserId = systemUser.i_UpdateUserId;
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

        #endregion

        public async Task<bool> ChangePassword(SystemUser systemUser)
        {
            var systemUserDb = await _dbSet.FirstOrDefaultAsync(u => u.i_SystemUserId == systemUser.i_SystemUserId);
            systemUserDb.v_Password = _passwordHasher.HashPassword(systemUserDb, systemUser.v_Password);

            #region AUDIT            
            systemUserDb.d_UpdateDate = DateTime.UtcNow;
            systemUserDb.i_UpdateUserId = systemUser.i_UpdateUserId;
            #endregion

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
            var systemUserDb = await _dbSet.Include(u => u.Permission).FirstOrDefaultAsync(u => u.v_UserName == systemUser.v_UserName);
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
            try
            {
                var query = await (from A in _context.SystemUser
                                   join B in _context.Permission on A.i_SystemUserId equals B.i_SystemUserId into B_join
                                   from B in B_join.DefaultIfEmpty()
                                   join C in _context.Access on B.i_PermissionId equals C.i_PermissionId into C_join
                                   from C in C_join.DefaultIfEmpty()
                                   join D in _context.OwnerCompany on C.i_OwnerCompanyId equals D.i_OwnerCompanyId into D_join
                                   from D in D_join.DefaultIfEmpty()
                                   join E in _context.Role on B.i_RoleId equals E.i_RoleId into E_join
                                   from E in E_join.DefaultIfEmpty()
                                   join F in _context.Profile on E.i_RoleId equals F.i_RoleId into F_join
                                   from F in F_join.DefaultIfEmpty()
                                   join G in _context.ApplicationHierarchy on F.i_ApplicationHierarchyId equals G.i_ApplicationHierarchyId into G_join
                                   from G in G_join.DefaultIfEmpty()
                                   join H in _context.Person on A.i_PersonId equals H.i_PersonId
                                   where A.i_SystemUserId == id
                                          && (B.i_IsDeleted == YesNo.No)
                                          && (C.i_IsDeleted == YesNo.No)
                                          && (D.i_IsDeleted == YesNo.No)
                                          && (E.i_IsDeleted == YesNo.No)
                                          && (F.i_IsDeleted == YesNo.No)
                                          && (G.i_IsDeleted == YesNo.No)
                                          && (H.i_IsDeleted == YesNo.No)
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
                                       ParentId = G.i_ParentId,
                                       Path = G.v_Path,
                                       PathDashboard = E.v_PathDashboard,
                                       CustomerCompanyId = A.i_CustomerCompanyId
                                   }
                            ).ToListAsync();

                if (query.Count == 0)
                {
                    var queryOpt = await (from A in _context.SystemUser
                                          join H in _context.Person on A.i_PersonId equals H.i_PersonId
                                          where A.i_SystemUserId == id
                                          select new
                                          {
                                              SystemUserId = A.i_InsertUserId,
                                              UserName = A.v_UserName,
                                              FullName = H.v_FirstName + " " + H.v_FirstLastName + " " + H.v_SecondLastName
                                          }).ToListAsync();

                    var oAccessSysteUserModelDtoOpt = new AccessSysteUserModelDto();

                    oAccessSysteUserModelDtoOpt.SystemUserId = id;
                    oAccessSysteUserModelDtoOpt.UserName = queryOpt[0].UserName;
                    oAccessSysteUserModelDtoOpt.FullName = queryOpt[0].FullName;

                    return oAccessSysteUserModelDtoOpt;

                }

                var oAccessSysteUserModelDto = new AccessSysteUserModelDto();

                oAccessSysteUserModelDto.SystemUserId = query[0].SystemUserId;
                oAccessSysteUserModelDto.UserName = query[0].UserName;
                oAccessSysteUserModelDto.FullName = query[0].FullName;
                oAccessSysteUserModelDto.CustomerCompanyId = query[0].CustomerCompanyId;

                var companiesDb = query.GroupBy(g => g.CompanyId).Select(s => s.First()).ToList();
                var companies = new List<Companies>();
                foreach (var itemComp in companiesDb)
                {
                    var oCompanies = new Companies { CompanyId = itemComp.CompanyId, CompanyName = itemComp.CompanyName };

                    var rolesDb = query.Where(w => w.CompanyId == oCompanies.CompanyId).GroupBy(g => g.RolId).Select(s => s.First()).ToList();
                    var roles = new List<Roles>();
                    foreach (var itemRol in rolesDb)
                    {
                        var oRole = new Roles { RolId = itemRol.RolId, RolName = itemRol.RolName, PathDashboard = itemRol.PathDashboard };
                        roles.Add(oRole);

                        var ModulesDb = query.Where(w => w.RolId == oRole.RolId && w.ParentId == -1).GroupBy(g => g.RolId).Select(s => s.First()).ToList();
                        var modules = new List<Module>();
                        foreach (var itemModule in ModulesDb)
                        {
                            var oModule = new Module { ModuleId = itemModule.ApplicationHierarchyId, ModuleName = itemModule.ApplicationHierarchyName };
                            modules.Add(oModule);

                            var OptionsDb = query.Where(w => w.RolId == oRole.RolId && w.ParentId != -1).GroupBy(g => g.ApplicationHierarchyId).Select(s => s.First()).ToList();
                            var Options = new List<Option>();

                            foreach (var itemOption in OptionsDb)
                            {
                                var oOption = new Option { OptionId = itemOption.ApplicationHierarchyId, OptionName = itemOption.ApplicationHierarchyName, Path = itemOption.Path };
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
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<(bool result, SystemUserLoginModel systemUser)> ValidateLogin(SystemUser systemUser)
        {
            try
            {
                var systemUserDb = await _dbSet.Include(u => u.Permission).FirstOrDefaultAsync(u => u.v_UserName == systemUser.v_UserName);


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
                                                         Roles = (from A1 in _context.Permission
                                                                  join B1 in _context.Role on A1.i_RoleId equals B1.i_RoleId
                                                                  where A1.i_IsDeleted == YesNo.No && A1.i_SystemUserId == A.i_SystemUserId
                                                                  group B1.v_Description by B1.v_Description into g
                                                                  select new RoleModel
                                                                  {
                                                                      RolName = g.Key
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
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<bool> UpdateAccess(List<UpdateAccessModel> updateAccessDto)
        {
            try
            {
                if (updateAccessDto.Count == 0) return true;

                var userId = updateAccessDto[0].UserId;

                //CAMBIAR DE ESTADO A LOS REGISTROS ANTIGUOS
                var permissionsDB = await _dbSetPermission.Include(acc => acc.Access).Where(i => i.i_SystemUserId == userId).ToListAsync();
                foreach (var permiDB in permissionsDB)
                {
                    permiDB.i_IsDeleted = YesNo.Yes;

                    foreach (var acceDB in permiDB.Access)
                    {
                        acceDB.i_IsDeleted = YesNo.Yes;
                    }
                }
                await _context.SaveChangesAsync();
                //AGREGAR NUEVOS PERMISOS
                foreach (var permi in updateAccessDto)
                {
                    var roles = permi.Roles;

                    foreach (var rol in roles)
                    {
                        var newPerm = new Permission();
                        newPerm.i_SystemUserId = userId;
                        newPerm.i_RoleId = rol;
                        newPerm.i_UpdateUserId = permi.UpdateUserId;
                        newPerm.d_UpdateDate = DateTime.Now;

                        _dbSetPermission.Add(newPerm);

                        await _context.SaveChangesAsync();
                        InsertAccess(newPerm.i_PermissionId, permi);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        private void InsertAccess(int permissionId, UpdateAccessModel permi)
        {
            var access = new Access();
            access.i_PermissionId = permissionId;
            access.i_OwnerCompanyId = permi.OwnerCompanyId;
            access.i_UpdateUserId = permi.UpdateUserId;
            access.d_UpdateDate = DateTime.Now;

            #region AUDIT
            access.i_IsDeleted = YesNo.No;
            access.d_InsertDate = DateTime.UtcNow;
            access.i_InsertUserId = permi.InsertUserId;
            #endregion

            _dbSetAccess.Add(access);

            _context.SaveChanges();
        }
    }
}
