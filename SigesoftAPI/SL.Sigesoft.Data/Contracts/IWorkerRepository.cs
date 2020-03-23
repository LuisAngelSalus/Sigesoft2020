using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SL.Sigesoft.Data.Contracts
{
    public interface IWorkerRepository : IGenericRepository<Worker>
    {
        Task<DataWorkerModel> GetAsyncByPersonId(int personId);
        Task<Worker> GetAsyncByDoc(string document);
        Task<DataWorkerModel> UpdateWorkerData(DataWorkerModel oDataWorkerModel);
        //Task<Worker> DoWorker(Worker listWorkers);
    }
}
