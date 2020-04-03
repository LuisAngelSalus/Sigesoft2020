using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SL.Sigesoft.Data.Contracts;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SL.Sigesoft.Data.Repositories
{
    public class ResultRepository : IResultRepository
    {
        private readonly SigesoftCoreContext _context;
        private readonly ILogger<SystemUserRepository> _logger;
        private readonly IPasswordHasher<SystemUser> _passwordHasher;

        public ResultRepository(SigesoftCoreContext context,
            ILogger<SystemUserRepository> logger,
            IPasswordHasher<SystemUser> passwordHasher)
        {
            this._context = context;
            this._logger = logger;
            this._passwordHasher = passwordHasher;
        }


        public async Task<List<ResultDetailModel>> GetDetail(int id)
        {
            var query = await (from SC in _context.ServiceComponent
                               join CO in _context.Component on SC.v_ComponentId equals CO.v_ComponentId
                               where
                               SC.i_ServiceId == id
                               select new ResultDetailModel
                               {
                                   i_ServiceComponentId = SC.i_ServiceComponentId,
                                   i_ServiceId = SC.i_ServiceId,
                                   v_ComponentId = SC.v_ComponentId,
                                   v_Name = CO.v_Name,
                                   i_ServiceComponentStatusId = SC.i_ServiceComponentStatusId
                               }).ToListAsync();
            return query;
        }

        public async Task<List<ResultModel>> GetAll()
        {
            var query = await (from SE in _context.Service
                               join PR in _context.Protocol on SE.i_ProtocolId equals PR.i_ProtocolId
                               join WO in _context.Worker on SE.i_WorkerId equals WO.i_WorkerId
                               join PE in _context.Person on WO.i_PersonId equals PE.i_PersonId
                               join CO in _context.Company on PR.i_CompanyId equals CO.i_CompanyId
                               join SPO in _context.SystemParameter on SE.i_ServiceStatusId equals SPO.i_ParameterId
                               join SPT in _context.SystemParameter on SE.i_AptitudeStatusId equals SPT.i_ParameterId
                               where
                               SPO.i_GroupId == 122 &&
                               SPT.i_GroupId == 124
                               select new ResultModel
                               {
                                   i_ServiceId = SE.i_ServiceId,
                                   d_ServiceDate = SE.d_ServiceDate,
                                   i_ProtocolId = PR.i_ProtocolId,
                                   v_ProtocolName = PR.v_ProtocolName,
                                   i_PersonId = PE.i_PersonId,
                                   v_FirstName = PE.v_FirstName,
                                   v_FirstLastName = PE.v_FirstLastName,
                                   v_SecondLastName = PE.v_SecondLastName,
                                   FullName = string.Format("{0} {1} {2}", PE.v_FirstName, PE.v_FirstLastName, PE.v_SecondLastName),
                                   i_CompanyId = CO.i_CompanyId,
                                   v_Name = CO.v_Name,
                                   v_CurrentPosition = WO.v_CurrentPosition,
                                   i_ServiceStatusId = SE.i_ServiceStatusId,
                                   ServiceStatusClass = GetClassServiceStatus(SE.i_ServiceStatusId),
                                   v_ValueService = SPO.v_Value1,
                                   i_AptitudeStatusId = SE.i_AptitudeStatusId,
                                   v_ValueAptitude = SPT.v_Value1

                               }).ToListAsync();
            return query;
        }

        public string GetClassServiceStatus(int id)
        {
            string response = string.Empty;
            switch (id)
            {
                case 1:
                    response = "btn-red";
                    break;
                case 2:
                    response = "btn-yellow";
                    break;
                case 3:
                    response = "btn-green";
                    break;
                case 4:
                    response = "btn-magenta";
                    break;
                case 5:
                    response = "btn-orange";
                    break;
            }
            return response;
        }

    }
}
