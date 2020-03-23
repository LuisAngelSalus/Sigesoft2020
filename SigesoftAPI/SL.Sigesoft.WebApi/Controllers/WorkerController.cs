using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SL.Sigesoft.Common;
using SL.Sigesoft.Data.Contracts;
using SL.Sigesoft.Dtos;
using SL.Sigesoft.Models;

namespace SL.Sigesoft.WebApi.Controllers
{
    [Authorize(Roles = "Sistemas,Trabajador,Cliente")]
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private IWorkerRepository _workerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<WorkerController> _logger;

        public WorkerController(IWorkerRepository workerRepository, IMapper mapper, ILogger<WorkerController> logger)
        {
            this._workerRepository = workerRepository;
            this._mapper = mapper;
            this._logger = logger;
        }

        [HttpGet("{id}/ObtenerDataTrabajador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<WorkerDto>>> GetAsyncByPersonId(int id)
        {
            var response = new Response<WorkerDto>();
            try
            {
                var person = await _workerRepository.GetAsyncByPersonId(id);
                response.Data = _mapper.Map<WorkerDto>(person);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa";
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error {nameof(GetAsyncByPersonId)}: {ex.Message}");
                throw;
            }

        }

        [HttpGet("{document}/ObtenerDataTrabajadorPorDoc")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<WorkerDto>>> GetAsyncByDoc(string document)
        {
            var response = new Response<WorkerDto>();
            try
            {
                var person = await _workerRepository.GetAsyncByDoc(document);
                response.Data = _mapper.Map<WorkerDto>(person);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa";
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error {nameof(GetAsyncByPersonId)}: {ex.Message}");
                throw;
            }

        }

        [HttpPost("ActualizarDataTrabajador")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<WorkerDto>>> UpdateWorkerData(DataWorkerModel dataWorkerModel)
        {
            var response = new Response<WorkerDto>();
            try
            {               
                var newPerson = await _workerRepository.UpdateWorkerData(dataWorkerModel);

                if (newPerson == null)
                {
                    response.Data = null;
                    response.IsSuccess = false;
                    response.Message = "Ocurrió un error en la operación";
                    return BadRequest(response);
                }

                var newPersonDto = _mapper.Map<WorkerDto>(newPerson);
                response.Data = newPersonDto;
                response.IsSuccess = true;
                response.Message = "Se grabó correctamente";

                return Ok(response);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error {nameof(UpdateWorkerData)}: {ex.Message}");
                return BadRequest();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<WorkerDto>>> Post(WorkerRegisterDto workerRegister)
        {
            var response = new Response<WorkerDto>();
            try
            {
                var worker = _mapper.Map<Worker>(workerRegister);
                var newWorker = await _workerRepository.AddAsync(worker);

                if (newWorker == null)
                {
                    return BadRequest();
                }

                var newWorkerDto = _mapper.Map<WorkerDto>(newWorker);
                response.Data = newWorkerDto;
                response.IsSuccess = true;
                response.Message = "Se grabó correctamente";
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}