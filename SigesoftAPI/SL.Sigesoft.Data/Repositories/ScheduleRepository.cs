using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SL.Sigesoft.Data.Contracts;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SL.Sigesoft.Data.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly SigesoftCoreContext _context;
        private readonly ILogger<ScheduleRepository> _logger;
        private DbSet<Schedule> _dbSet;        

        public ScheduleRepository(SigesoftCoreContext context,
         ILogger<ScheduleRepository> logger )
        {
            this._context = context; 
            this._logger = logger;
            this._dbSet = _context.Set<Schedule>();            
        }

        public async Task<bool> DoSchedule(List<Schedule> schedules)
        {            
            try
            {
                foreach (var item in schedules)
                {
                    _dbSet.Add(item);
                }

                return await _context.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(DoSchedule)}: " + ex.Message);
                return false;
            }
        }
    }
}
