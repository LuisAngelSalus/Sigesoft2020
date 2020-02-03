using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SL.Sigesoft.Data.Contracts;
using SL.Sigesoft.Models;
using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace SL.Sigesoft.Data.Repositories
{
    public class ProtocolProfileRepository : IProtocolProfileRepository
    {
        private readonly SigesoftWinContext _contextWin;
        private readonly SigesoftCoreContext _context;
        private readonly ILogger<ProtocolProfileRepository> _logger;
        private DbSet<ProtocolProfile> _dbSet;

        public ProtocolProfileRepository(SigesoftCoreContext context, SigesoftWinContext contextWin,
          ILogger<ProtocolProfileRepository> logger)
        {
            this._context = context;
            this._contextWin = contextWin;
            this._logger = logger;
            this._dbSet = _context.Set<ProtocolProfile>();
        }


        public async Task<ProtocolProfile> AddAsync(ProtocolProfile entity)
        {
            #region AUDIT
            entity.i_IsDeleted = YesNo.No;
            entity.d_InsertDate = DateTime.UtcNow;
            entity.i_InsertUserId = 11;
            #endregion

            foreach (var item in entity.ProfileDetail)
            {
                #region AUDIT
                item.i_IsDeleted = YesNo.No;
                item.d_InsertDate = DateTime.UtcNow;
                item.i_InsertUserId = entity.i_InsertUserId;
                #endregion
            }

            _dbSet.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(AddAsync)}: {ex.Message}");
            }
            return entity;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProtocolProfile>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ProtocolProfile> GetAsync(int id)
        {
            return await _dbSet.Include(i => i.ProfileDetail).SingleOrDefaultAsync(c => c.i_ProtocolProfileId== id && c.i_IsDeleted == YesNo.No);
        }

        public async Task<ProtocolProfileModel> GetProfile(int protocolProfileId)
        {
            var profile = new ProtocolProfileModel();
            try
            {
                var protocolProfile = await _dbSet.Include(i => i.ProfileDetail).SingleOrDefaultAsync(c => c.i_ProtocolProfileId == protocolProfileId && c.i_IsDeleted == YesNo.No);            
            
            var selectedCategories = protocolProfile.ProfileDetail.AsEnumerable()
                       .Where(s => s.i_CategoryId != -1)
                       .GroupBy(x => x.i_CategoryId)
                       .Select(group => group.First());

            var query = await (from A in _contextWin.Component
                               join B in _contextWin.SystemParameter on new { a = A.i_CategoryId.Value, b = 116 }
                                            equals new { a = B.i_ParameterId, b = B.i_GroupId } into B_join
                               from B in B_join.DefaultIfEmpty()
                               where A.i_IsDeleted == YesNo.No
                               select new 
                               {
                                   v_ComponentId = A.v_ComponentId,
                                   v_Name = A.v_Name,
                                   i_CategoryId = A.i_CategoryId,
                                   v_CategoryName = B.v_Value1,
                                   r_CostPrice = A.r_CostPrice,
                                   r_BasePrice = A.r_BasePrice,
                                   r_SalePrice = A.r_SalePrice
                               }).ToListAsync();


            var unselectedCategories = query.AsEnumerable()
                       .Where(s => s.i_CategoryId != -1)
                       .GroupBy(x => x.i_CategoryId)
                       .Select(group => group.First());

            profile.ProtocolProfileId = protocolProfile.i_ProtocolProfileId;
            profile.ProtocolProfileName = protocolProfile.v_Name;

            var oCategories = new List<CategoryModel>();
            var oUnselectedCategories = new List<CategoryModel>();
            foreach (var category in selectedCategories)
            {
                #region Selecteds
                var oCategoryModel = new CategoryModel();
                oCategoryModel.CategoryId = category.i_CategoryId;
                oCategoryModel.CategoryName = category.v_CategoryName;

                var detail = protocolProfile.ProfileDetail.ToList().FindAll(p => p.i_CategoryId == category.i_CategoryId).ToList();

                var list = new List<ProfileDetailModel>();
                foreach (var item in detail)
                {
                    var o = new ProfileDetailModel();
                    o.CategoryId = category.i_CategoryId;
                    o.Active = true;
                    o.ComponentId = item.v_ComponentId;
                    o.ComponentName = query.Find(p => p.v_ComponentId == item.v_ComponentId).v_Name;
                    o.MinPrice = item.r_MinPrice == null? 0: float.Parse( item.r_MinPrice.ToString());
                    o.ListPrice = item.r_ListPrice == null ? 0 : float.Parse(item.r_ListPrice.ToString());
                    o.SalePrice = item.r_SalePrice == null ? 0 : float.Parse(item.r_SalePrice.ToString());

                    list.Add(o);
                }
                    list.Sort((x, y) => x.ComponentName.CompareTo(y.ComponentName));
                    oCategoryModel.Detail = list;
                oCategories.Add(oCategoryModel);
                #endregion
            }

            foreach (var unCategory in unselectedCategories)
            {
                #region Unselecteds
                var oUnselectedCategoryModel = new CategoryModel();
                oUnselectedCategoryModel.CategoryId = unCategory.i_CategoryId.Value;
                oUnselectedCategoryModel.CategoryName = unCategory.v_CategoryName;
                var unSeldetail = query.FindAll(p => p.i_CategoryId == unCategory.i_CategoryId).ToList();

                var unSellist = new List<ProfileDetailModel>();
                foreach (var item in unSeldetail)
                {
                    var res = protocolProfile.ProfileDetail.ToList().Find(p => p.v_ComponentId == item.v_ComponentId);
                    if (res == null)
                    {
                        var o = new ProfileDetailModel();
                        o.CategoryId = unCategory.i_CategoryId.Value;
                        o.Active = false;
                        o.ComponentId = item.v_ComponentId;
                        o.ComponentName = item.v_Name;
                        o.MinPrice = item.r_CostPrice == null ? 0 : item.r_CostPrice;
                        o.ListPrice = item.r_BasePrice == null ? 0 : item.r_BasePrice;
                        o.SalePrice = item.r_SalePrice == null ? 0 : item.r_SalePrice;

                        unSellist.Add(o);
                    }
                    

                }
                    unSellist.Sort((x, y) => x.ComponentName.CompareTo(y.ComponentName));
                    oUnselectedCategoryModel.Detail = unSellist;
                    oUnselectedCategories.Add(oUnselectedCategoryModel);

                #endregion
            }



            profile.categories = oCategories;
            profile.UnselectedCategories = oUnselectedCategories;

                return profile;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Task<bool> UpdateAsync(ProtocolProfile entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProtocolProfile>> DrowpDownList()
        {
            return await _dbSet.Where(c => c.i_IsDeleted == YesNo.No).ToListAsync();
        }

        public async Task<List<ProtocolProfile>> AutocompleteByName(string value)
        {
            return await _dbSet.Where(c => c.i_IsDeleted == YesNo.No && c.v_Name.Contains(value)).ToListAsync();
        }


    }
}
