using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SL.Sigesoft.Data.Contracts;
using SL.Sigesoft.Models;
using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Sigesoft.Data.Repositories
{
    public class WorkerRepository : IWorkerRepository
    {
        private SigesoftCoreContext _context;
        private IPersonsRepository _personsRepository;
        private readonly ILogger<WorkerRepository> _logger;
        private DbSet<Worker> _dbSet;

        public WorkerRepository(SigesoftCoreContext context, ILogger<WorkerRepository> logger, IPersonsRepository personsRepository)
        {
            _context = context;
            _personsRepository = personsRepository;
            this._logger = logger;
            this._dbSet = _context.Set<Worker>();
        }

        public async Task<Worker> AddAsync(Worker entity)
        {
            #region AUDIT
            entity.i_IsDeleted = YesNo.No;
            entity.d_InsertDate = DateTime.UtcNow;
            entity.i_InsertUserId = entity.i_InsertUserId;
            #endregion

            _context.Worker.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(AddAsync)}: {ex.Message}");
            }
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var worker = await _context.Worker
                                .SingleOrDefaultAsync(c => c.i_WorkerId== id);
            #region AUDIT
            worker.i_IsDeleted = YesNo.No;
            worker.d_UpdateDate = DateTime.UtcNow;
            //worker.i_UpdateUserId = 11;
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

        public async Task<IEnumerable<Worker>> GetAllAsync()
        {
            return await _context.Worker.OrderBy(u => u.i_WorkerId).ToListAsync();
        }

        public async Task<Worker> GetAsync(int id)
        {
            return await _context.Worker.SingleOrDefaultAsync(u => u.i_WorkerId == id && u.i_IsDeleted == YesNo.No);
        }

        public async Task<DataWorkerModel> GetAsyncByPersonId(int sysemtUserId)
        {
            var person = await _context.SystemUser.Where(w => w.i_SystemUserId == sysemtUserId).SingleAsync();
            
            var worker = await _context.Person.Include(i => i.Worker).SingleOrDefaultAsync(u => u.i_PersonId == person.i_PersonId && u.i_IsDeleted == YesNo.No);
            if (worker == null) return null;

            var oDataWorkerModel = new DataWorkerModel();
            oDataWorkerModel.PersonId = worker.i_PersonId;
            oDataWorkerModel.FirstName = worker.v_FirstName;
            oDataWorkerModel.FirstName = worker.v_FirstName;
            oDataWorkerModel.FirstLastName = worker.v_FirstLastName;
            oDataWorkerModel.SecondLastName = worker.v_SecondLastName;

            oDataWorkerModel.TypeDocumentId = worker.Worker == null ? -1 : (int)worker.Worker.i_TypeDocumentId;
            oDataWorkerModel.NroDocument = worker.Worker == null ? "" : worker.Worker.v_NroDocument;            
            oDataWorkerModel.WorkerId = worker.Worker?.i_WorkerId;
            oDataWorkerModel.CurrentPosition = worker.Worker == null ? "" : worker.Worker.v_CurrentPosition;
            oDataWorkerModel.HomeAddress = worker.Worker == null ? "" : worker.Worker.v_HomeAddress;
            oDataWorkerModel.DateOfBirth = worker.Worker?.d_DateOfBirth;
            oDataWorkerModel.GenderId = worker.Worker == null ? -1: worker.Worker.i_GenderId;
            oDataWorkerModel.Email = worker.Worker == null ? "" : worker.Worker.v_Email;
            oDataWorkerModel.MobileNumber = worker.Worker == null ? "" : worker.Worker.v_MobileNumber;

            return oDataWorkerModel;

        }

        public async Task<bool> UpdateAsync(Worker entity)
        {
            var entityDb = await GetAsync(entity.i_WorkerId);
            entityDb.v_CurrentPosition = entity.v_CurrentPosition;
            entityDb.v_HomeAddress = entity.v_HomeAddress;
            entityDb.d_DateOfBirth = entity.d_DateOfBirth;
            entityDb.i_GenderId = entity.i_GenderId;
            entityDb.v_Email = entity.v_Email;
            entityDb.v_MobileNumber = entity.v_MobileNumber;
            entityDb.v_NroDocument = entity.v_NroDocument;
            #region AUDIT
            entityDb.d_UpdateDate = DateTime.UtcNow;
            entityDb.i_UpdateUserId = entity.i_UpdateUserId;
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

        public async Task<DataWorkerModel> UpdateWorkerData(DataWorkerModel oDataWorkerModel)
        {
            try
            {
                var person = new Person();
                person.i_PersonId = oDataWorkerModel.PersonId;
                person.v_FirstName = oDataWorkerModel.FirstName;
                person.v_FirstLastName = oDataWorkerModel.FirstLastName;
                person.v_SecondLastName = oDataWorkerModel.SecondLastName;

                if (await _personsRepository.UpdateAsync(person))
                {
                    if (oDataWorkerModel.WorkerId != null)
                    {
                        var worker = new Worker();
                        worker.i_WorkerId = oDataWorkerModel.WorkerId.Value;
                        worker.i_PersonId = oDataWorkerModel.PersonId;
                        worker.v_CurrentPosition = oDataWorkerModel.CurrentPosition;
                        worker.v_HomeAddress = oDataWorkerModel.HomeAddress;
                        worker.d_DateOfBirth = oDataWorkerModel.DateOfBirth.Value;
                        worker.i_GenderId = oDataWorkerModel.GenderId.Value;
                        worker.v_Email = oDataWorkerModel.Email;
                        worker.v_MobileNumber = oDataWorkerModel.MobileNumber;
                        worker.i_TypeDocumentId = oDataWorkerModel.TypeDocumentId.Value;
                        worker.v_NroDocument = oDataWorkerModel.NroDocument;

                        if (await UpdateAsync(worker)) return oDataWorkerModel;
                    }
                    else
                    {
                        var worker = new Worker();
                        worker.i_PersonId = oDataWorkerModel.PersonId;
                        worker.v_CurrentPosition = oDataWorkerModel.CurrentPosition;
                        worker.v_HomeAddress = oDataWorkerModel.HomeAddress;
                        worker.d_DateOfBirth = oDataWorkerModel.DateOfBirth.Value;
                        worker.i_GenderId = oDataWorkerModel.GenderId.Value;
                        worker.v_Email = oDataWorkerModel.Email;
                        worker.v_MobileNumber = oDataWorkerModel.MobileNumber;
                        worker.i_TypeDocumentId = oDataWorkerModel.TypeDocumentId.Value;
                        worker.v_NroDocument = oDataWorkerModel.NroDocument;

                        var newWorker = await AddAsync(worker);

                        if (newWorker.i_WorkerId > 0)
                        {
                            oDataWorkerModel.WorkerId = newWorker.i_WorkerId;
                            return oDataWorkerModel;
                        };

                        return null;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }

        //public async Task<Worker> DoWorker(Worker worker)
        //{
        //    try
        //    {
        //        //foreach (var item in listWorkers)
        //        //{
        //            _dbSet.Add(worker);
        //        //}
        //         await _context.SaveChangesAsync() ;

        //        return worker;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Error en {nameof(DoWorker)}: " + ex.Message);
        //        return null;
        //    }
            
        //}

        public async Task<Worker> GetAsyncByDoc(string document)
        {
            try
            {
                var worker = await _context.Worker.Where(p => p.v_NroDocument == document).FirstOrDefaultAsync();
                return worker;
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
    }
}
