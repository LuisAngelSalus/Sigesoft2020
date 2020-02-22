using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SL.Sigesoft.Common;
using SL.Sigesoft.Data.Contracts;
using SL.Sigesoft.Models;
using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SL.Sigesoft.Data.Repositories
{
    public class SuscriptionRespository : ISubscriptionRepository
    {
        private readonly SigesoftCoreContext _context;
        private readonly ILogger<SuscriptionRespository> _logger;
        private DbSet<Suscription> _dbSet;

        public SuscriptionRespository(SigesoftCoreContext context,
            ILogger<SuscriptionRespository> logger)
        {
            this._context = context;
            this._logger = logger;
            this._dbSet = _context.Set<Suscription>();
        }

        public async Task<Suscription> AddAsync(Suscription entity)
        {
            entity.i_IsDeleted = YesNo.No;                        
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

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Suscription>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public Task<Suscription> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public string GetKeyPublic()
        {
            return Constants.KEY_PUBLIC;
        }

        public Task<bool> UpdateAsync(Suscription entity)
        {
            throw new NotImplementedException();
        }
    }
}
