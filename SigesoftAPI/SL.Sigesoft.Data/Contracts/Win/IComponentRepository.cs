using SL.Sigesoft.Models.Win;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SL.Sigesoft.Data.Contracts.Win
{
   public interface IComponentRepository
    {
        Task<List<Component>> GetAllAsync();
    }
}
