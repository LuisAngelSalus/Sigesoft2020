using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SL.Sigesoft.Common;
using SL.Sigesoft.Data.Contracts;
using SL.Sigesoft.Dtos;
using SL.Sigesoft.Models;

namespace SL.Sigesoft.WebApi.Controllers
{
    [Authorize(Roles = "Sistemas,Médico,Recepción,Gerencia,Comercial,Administrador,Cliente,Trabajador")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountSettingController : ControllerBase
    {
        private IAccountSettingRepository _accountSettingRepository;
        private readonly IMapper _mapper;

        public AccountSettingController(IAccountSettingRepository accountSettingRepository, IMapper mapper)
        {
            this._accountSettingRepository = accountSettingRepository;
            this._mapper = mapper;
        }

        [HttpGet("{systemUserId}/configuracionCuenta")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<ListAccountSettingDto>>> GetAccountSettingBySystemUserId(int systemUserId)
        {
            var response = new Response<ListAccountSettingDto>();
            try
            {
                var accountSetting = await _accountSettingRepository.GetAccountSettingBySystemUserId(systemUserId);
                if (accountSetting == null)
                {
                    response.Data = null;
                    response.IsSuccess = true;
                    response.Message = "No se encontró registro";
                    return NotFound(response);
                }
                response.Data = _mapper.Map<ListAccountSettingDto>(accountSetting);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa";
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<ListAccountSettingDto>>> Post(AccountSettingRegisterDto accountSettingDto)
        {
            var response = new Response<ListAccountSettingDto>();

            var accountSetting = _mapper.Map<AccountSetting>(accountSettingDto);

            var newAccountSetting = await _accountSettingRepository.AddAsync(accountSetting);
            if (newAccountSetting == null)
            {
                response.IsSuccess = false;
                response.Message = "Error en la operación";
                return BadRequest(response);
            }

            var newAccountSettingDto = _mapper.Map<ListAccountSettingDto>(newAccountSetting);
            response.Data = newAccountSettingDto;
            response.IsSuccess = true;
            response.Message = "Se grabó correctamente";
            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<ListAccountSettingDto>>> Put([FromBody]AccountSettingUpdateDto accountSettingDto)
        {
            var response = new Response<ListAccountSettingDto>();
            if (accountSettingDto == null)
            {
                response.IsSuccess = false;
                response.Message = "Entidad vacía";
                return NotFound(response);
            }

            var accountSetting = _mapper.Map<AccountSetting>(accountSettingDto);
            var result = await _accountSettingRepository.UpdateAsync(accountSetting);
            if (!result)
            {
                response.IsSuccess = false;
                response.Message = "No se encuentra registro";
                return BadRequest(response);
            }

            response.Data = _mapper.Map<ListAccountSettingDto>(accountSetting);
            response.IsSuccess = true;
            response.Message = "Se actualizó correctamente";

            return response;
        }


    }
}