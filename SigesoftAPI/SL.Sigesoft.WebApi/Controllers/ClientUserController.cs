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
    [Authorize(Roles = "Cliente")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientUserController : ControllerBase
    {
        private IClientUserRepository _clientUserRepository;        
        private readonly IMapper _mapper;

        public ClientUserController(IClientUserRepository clientUserRepository, IMapper mapper)
        {            
            this._clientUserRepository = clientUserRepository;
            this._mapper = mapper;
        }

        [HttpGet("{clientUserId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<ClientUserDto>>> Get(int clientUserId)
        {
            var response = new Response<ClientUserDto>();
            try
            {
                var clientUser = await _clientUserRepository.GetAsync(clientUserId);
                if (clientUser == null)
                {
                    response.Data = null;
                    response.IsSuccess = true;
                    response.Message = "No se encontró registro";
                    return NotFound(response);
                }
                response.Data = _mapper.Map<ClientUserDto>(clientUser);
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
               
        [HttpGet("{companyId}/UsuariosClientes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<IEnumerable<ClientUserDto>>>> GetAllAsyncByCompany(int companyId)
        {
            var response = new Response<IEnumerable<ClientUserDto>>();
            try
            {
                var clientUsers = await _clientUserRepository.GetAllAsyncByCompany(companyId);
                if (clientUsers == null)
                {
                    response.Data = null;
                    response.IsSuccess = true;
                    response.Message = "No se encontró registro";
                    return NotFound(response);
                }
                response.Data = _mapper.Map<List<ClientUserDto>>(clientUsers);
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
        public async Task<ActionResult<Response<ClientUserDto>>> Post(ClientUserRegisterDto clientUserDto)
        {
            var response = new Response<ClientUserDto>();
            try
            {
                var clientUser = _mapper.Map<ClientUser>(clientUserDto);

                var newClientUser = await _clientUserRepository.AddAsync(clientUser);
                if (newClientUser == null)
                {
                    return BadRequest();
                }

                var newClientUserDto = _mapper.Map<ClientUserDto>(newClientUser);
                response.Data = newClientUserDto;
                response.IsSuccess = true;
                response.Message = "Se grabó correctamente";
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<ClientUserDto>>> Put([FromBody]ClientUserUpdateDto clientUserDto)
        {
            var response = new Response<ClientUserDto>();
            if (clientUserDto == null)
                return NotFound();

            var clientUser = _mapper.Map<ClientUser>(clientUserDto);
            var result = await _clientUserRepository.UpdateAsync(clientUser);
            if (!result)
                return BadRequest();

            response.Data = _mapper.Map<ClientUserDto>(clientUser);
            response.IsSuccess = true;
            response.Message = "Se actualizó correctamente";
            
            return response;
        }

        [HttpPost]
        [Route("cambiarcontrasena")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword(ClientUserPasswordDto clientUserDto)
        {
            try
            {
                var clientUser = _mapper.Map<ClientUser>(clientUserDto);
                var result = await _clientUserRepository.ChangePassword(clientUser);
                if (!result)
                {
                    return BadRequest();
                }
                return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}/actualizarEmpresa")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<ListCompanyDto>>> UpdateCompany([FromBody]CompanyUpdateDataDto companyDto)
        {
            var response = new Response<ListCompanyDto>();
            if (companyDto == null)
                return NotFound();

            var company = _mapper.Map<Company>(companyDto);
            var result = await _clientUserRepository.UpdateCompany(company);
            if (!result)
            {
                response.Data = new ListCompanyDto();
                response.IsSuccess = false;
                response.Message = "Error en la operación";
                return BadRequest(response);
            }

            response.Data = _mapper.Map<ListCompanyDto>(company);
            response.IsSuccess = true;
            response.Message = "Se actualizó correctamente";

            return response;
        }

    }
}