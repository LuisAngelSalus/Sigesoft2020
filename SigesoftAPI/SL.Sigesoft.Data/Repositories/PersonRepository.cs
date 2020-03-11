using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SL.Sigesoft.Data.Contracts;
using SL.Sigesoft.Models;
using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SL.Sigesoft.Data.Repositories
{
    public class PersonRepository : IPersonsRepository
    {
        private SigesoftCoreContext _context;
        private readonly ILogger<PersonRepository> _logger;

        public PersonRepository(SigesoftCoreContext context,ILogger<PersonRepository> logger)
        {
            _context = context;
            this._logger = logger;
        }

        public async Task<Person> AddAsync(Person person)
        {
            #region AUDIT
            person.i_IsDeleted = YesNo.No;
            person.d_InsertDate = DateTime.UtcNow;
            person.i_InsertUserId = person.i_InsertUserId;
            #endregion

            _context.Person.Add(person);
            try
            {
               await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(AddAsync)}: {ex.Message}");
            }
            return person;
        }

        public async Task<bool> UpdateAsync(Person person)
        {
            var personDb = await GetPersonAsync(person.i_PersonId);
            personDb.v_FirstName = person.v_FirstName;
            personDb.v_FirstLastName = person.v_FirstLastName;
            personDb.v_SecondLastName = person.v_SecondLastName;

            #region AUDIT
            personDb.d_UpdateDate = DateTime.UtcNow;
            personDb.i_UpdateUserId = person.i_UpdateUserId;
            #endregion

            try
            {
                return await _context.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(UpdateAsync)}: {ex.Message}");
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int personId)
        {
            var person = await _context.Person
                                .SingleOrDefaultAsync(c => c.i_PersonId == personId);
            #region AUDIT
            person.i_IsDeleted = YesNo.No;
            person.d_UpdateDate = DateTime.UtcNow;
            //person.i_UpdateUserId = 11;
            #endregion

            try
            {
                return await _context.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(DeleteAsync)}: {ex.Message}");
            }
            return false;

        }

        public async Task<Person> GetPersonAsync(int personId)
        {
            return await _context.Person.SingleOrDefaultAsync(u => u.i_PersonId == personId && u.i_IsDeleted == YesNo.No);
        }

        public async Task<List<Person>> GetPersonsAsync()
        {
            return await _context.Person.OrderBy(u => u.i_PersonId).ToListAsync();
        }        
    }
}
