using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SL.Sigesoft.Data.Contracts
{
   public interface IProtocolProfileRepository : IGenericRepository<ProtocolProfile>
    {
        Task<ProtocolProfileModel> GetProfile(int protocolProfileId);
        Task<List<ProtocolProfile>> DrowpDownList();
        Task<List<ProtocolProfile>> AutocompleteByName(string value);
    }
}
