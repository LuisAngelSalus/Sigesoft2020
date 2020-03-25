using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SL.Sigesoft.Data.Contracts;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Globalization;
using SL.Sigesoft.Models.Enum;

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

        public async Task<List<ScheduleListModel>> Search(ParamsSearch paramsSearch)
        {
            
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "yyyy-MM-dd" };
            bool validfi = DateTime.TryParseExact(paramsSearch.StartDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fi);
            bool validff = DateTime.TryParseExact(paramsSearch.EndDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ff);
            var queryWorker = await( from A in _context.Schedule
                                     join B in _context.Service on A.i_ServiceId equals B.i_ServiceId
                                     join C in _context.Worker on B.i_WorkerId equals C.i_WorkerId
                                     join D in _context.Person on C.i_PersonId equals D.i_PersonId
                                     join E in _context.Protocol on B.i_ProtocolId equals E.i_ProtocolId
                                     join F in _context.Company on E.i_CompanyId equals F.i_CompanyId                                     
                                     where A.i_IsDeleted == YesNo.No
                                        && (!validfi || A.d_DateTimeCalendar >= fi)
                                        && (!validff || A.d_DateTimeCalendar <= ff)
                                     select new  { 
                                        CompanyName = F.v_Name,
                                        CurrentOccupation = C.v_CurrentPosition,
                                        Email = C.v_Email,
                                        Cell = C.v_MobileNumber,
                                        FullName = D.v_FirstName + " " + D.v_FirstLastName + " " + D.v_SecondLastName, 
                                        ProtocolName = E.v_ProtocolName                                        
                                     }).ToListAsync();

            var filterName = queryWorker.Where(x => x.FullName.ToLower().Contains(paramsSearch.Value.ToLower())).ToList();
            var filterCompany = queryWorker.Where(x => x.CompanyName.ToLower().Contains(paramsSearch.Value.ToLower())).ToList();
            var filterCurrentOccupation = queryWorker.Where(x => x.CurrentOccupation.ToLower().Contains(paramsSearch.Value.ToLower())).ToList();            
            var filterProtocol = queryWorker.Where(x => x.ProtocolName.ToLower().Contains(paramsSearch.Value.ToLower())).ToList();                       

            var combinedList = filterName.Concat(filterCompany).Concat(filterCurrentOccupation).Concat(filterProtocol);

            var result = new List<ScheduleListModel>();
            foreach (var item in combinedList)
            {
                var oScheduleListModel = new ScheduleListModel();
                oScheduleListModel.FullName = item.FullName;
                oScheduleListModel.CompanyName = item.CompanyName;
                oScheduleListModel.WorkerEmail = item.Email;
                oScheduleListModel.WorkerCell = item.Cell;
                oScheduleListModel.ProtocolName = item.ProtocolName;
                oScheduleListModel.CurrentOccupation = item.CurrentOccupation;

                result.Add(oScheduleListModel);
            }

            return result;
        }
    }
}
