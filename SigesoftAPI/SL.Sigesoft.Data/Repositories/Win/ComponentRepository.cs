using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
   public class ComponentRepository : IComponentRepository
    {
        private readonly SigesoftWinContext _context;
        private readonly ILogger<ComponentRepository> _logger;
        private DbSet<Component> _dbSet;

        public ComponentRepository(SigesoftWinContext context,
           ILogger<ComponentRepository> logger)
        {
            this._context = context;
            this._logger = logger;
            this._dbSet = _context.Set<Component>();
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
    }
}
