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
    //[Authorize(Roles = "Administrador,Comercial")]
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private IWarehouseRepository _warehouseRepository;
        private readonly ILogger<WarehouseController> _logger;
        private readonly IMapper _mapper;

        public WarehouseController(IWarehouseRepository warehouseRepository, IMapper mapper, ILogger<WarehouseController> logger)
        {
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<IEnumerable<WarehouseDto>>>> Get()
        {
            var response = new Response<IEnumerable<WarehouseDto>>();
            try
            {
                var quotation = await _warehouseRepository.GetAllAsync();
                if (quotation == null)
                {
                    return NotFound();
                }
                response.Data = _mapper.Map<IEnumerable<WarehouseDto>>(quotation);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Post)}: " + ex.Message);
                return BadRequest();
            }
            return response;

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<WarehouseDto>>> Get(int id)
        {
            var response = new Response<WarehouseDto>();
            try
            {
                var quotation = await _warehouseRepository.GetAsync(id);
                if (quotation == null)
                {
                    return NotFound();
                }
                response.Data = _mapper.Map<WarehouseDto>(quotation);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Post)}: " + ex.Message);
                return BadRequest();
            }
            return response;

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<WarehouseInsertDto>>> Post(WarehouseInsertDto warehouseDto)
        {
            var response = new Response<WarehouseInsertDto>();
            try
            {
                var warehouse = _mapper.Map<Warehouse>(warehouseDto);
                var newWarehouse = await _warehouseRepository.AddAsync(warehouse);
                if (newWarehouse == null)
                {
                    response.Data = null;
                    response.IsSuccess = false;
                    response.Message = "the warehouse was not registered";
                    return BadRequest(response);
                }

                var newWarehouseDto = _mapper.Map<WarehouseInsertDto>(newWarehouse);
                response.Data = newWarehouseDto;
                response.IsSuccess = true;
                response.Message = "the warehouse was registered successful";

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Post)}: " + ex.Message);
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<bool>>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                var result = await _warehouseRepository.DeleteAsync(id);
                if (!result)
                {
                    return BadRequest();
                }
                response.Data = result;
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Eliminación Exitosa";
                }
                return response;
            }
            catch (Exception excepcion)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<WarehouseDto>>> Put([FromBody]WarehouseUpdateDataDto warehouseUpdateDataDto)
        {
            var response = new Response<WarehouseDto>();
            if (warehouseUpdateDataDto == null)
                return NotFound();

            var warehouse = _mapper.Map<Warehouse>(warehouseUpdateDataDto);
            var result = await _warehouseRepository.UpdateAsync(warehouse);
            if (!result)
            {
                response.Data = new WarehouseDto();
                response.IsSuccess = false;
                response.Message = "Error en la operación";
                return BadRequest(response);
            }

            response.Data = _mapper.Map<WarehouseDto>(warehouse);
            response.IsSuccess = true;
            response.Message = "Se actualizó correctamente";

            
            return response;
        }

    }
}