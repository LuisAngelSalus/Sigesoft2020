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


        public Task<ProtocolProfile> AddAsync(ProtocolProfile entity)
        {
            throw new NotImplementedException();
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
            var protocolProfile = await _dbSet.Include(i => i.ProfileDetail).SingleOrDefaultAsync(c => c.i_ProtocolProfileId == protocolProfileId && c.i_IsDeleted == YesNo.No);
            
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


            var categories = query.AsEnumerable()
                       .Where(s => s.i_CategoryId != -1)
                       .GroupBy(x => x.i_CategoryId)
                       .Select(group => group.First());

            profile.ProtocolProfileId = protocolProfile.i_ProtocolProfileId;
            profile.ProtocolProfileName = protocolProfile.v_Name;

            var oCategories = new List<CategoryModel>();
            foreach (var category in categories)
            {
                var oCategoryModel = new CategoryModel();
                oCategoryModel.CategoryId = category.i_CategoryId.Value;
                oCategoryModel.CategoryName = category.v_CategoryName;

                var detail = query.FindAll(p => p.i_CategoryId == category.i_CategoryId).ToList();

                var list = new List<ProfileDetailModel>();
                foreach (var item in detail)
                {
                    var o = new ProfileDetailModel();
                    var x = protocolProfile.ProfileDetail;
                    var y = x.ToList().Find(p => p.v_ComponentId == item.v_ComponentId);
                    o.Active = y == null ? false : true;
                    o.ComponentId = item.v_ComponentId;
                    o.ComponentName = item.v_CategoryName;
                    o.CostPrice = item.r_CostPrice;
                    o.BasePrice = item.r_BasePrice;
                    o.SalePrice = item.r_SalePrice;

                    list.Add(o);
                }

                oCategoryModel.Detail = list;
                oCategories.Add(oCategoryModel);
            }

            profile.categories = oCategories;

            return profile;
        }

        public Task<bool> UpdateAsync(ProtocolProfile entity)
        {
            throw new NotImplementedException();
        }
    }
}
