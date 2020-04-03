using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SL.Sigesoft.Data.Contracts
{
    public interface IResultRepository
    {
        Task<List<ResultModel>> GetAll();
        Task<List<ResultDetailModel>> GetDetail(int id);
    }
}
