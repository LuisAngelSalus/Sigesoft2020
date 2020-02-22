using SL.Sigesoft.Data.Contracts;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SL.Sigesoft.Data.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        public Task<Notification> AddAsync(Notification entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Notification>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Notification> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Notification entity)
        {
            throw new NotImplementedException();
        }
    }
}
