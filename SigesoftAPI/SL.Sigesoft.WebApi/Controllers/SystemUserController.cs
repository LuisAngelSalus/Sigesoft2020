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
    [Authorize(Roles = "Administrador,Sistemas,Médico,Recepción,Gerencia,Comercial")]
    [Route("api/[controller]")]
    [ApiController]
    public class SystemUserController : ControllerBase
    {
        private ISystemUserRepository _systemUserRepository;
        private readonly IMapper _mapper;

        public SystemUserController(ISystemUserRepository systemUserRepository, IMapper mapper)
        {
            this._systemUserRepository = systemUserRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<IEnumerable<SystemUserDto>>>> Get()
        {
            var response = new Response<IEnumerable<SystemUserDto>>();
            try
            {                
                var systemUsers = await _systemUserRepository.GetAllAsync();
                response.Data = _mapper.Map<List<SystemUserDto>>(systemUsers);
                
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa";
                }
                return response;
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // GET: api/usuarios/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ListSystemUserDto>> Get(int id)
        {
            try
            {
                var systemUser = await _systemUserRepository.GetAsync(id);
                if (systemUser == null)
                {
                    return NotFound();
                }
                return _mapper.Map<ListSystemUserDto>(systemUser); ;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        // POST: api/usuarios
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ListSystemUserDto>> Post(SystemUserRegisterDto usuarioDto)
        {
            try
            {
                var systemUser = _mapper.Map<SystemUser>(usuarioDto);

                var newSystemUser = await _systemUserRepository.AddAsync(systemUser);
                if (newSystemUser == null)
                {
                    return BadRequest();
                }

                var newSystemUserDto = _mapper.Map<ListSystemUserDto>(newSystemUser);
                return CreatedAtAction(nameof(Post), new { id = newSystemUser.i_SystemUserId }, newSystemUserDto);

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // PUT: api/usuarios/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ListSystemUserDto>> Put(int id, [FromBody]SystemUserUpdateDataDto systemUserDto)
        {
            if (systemUserDto == null)
                return NotFound();

            var systemUser = _mapper.Map<SystemUser>(systemUserDto);
            var result = await _systemUserRepository.UpdateAsync(systemUser);
            if (!result)
                return BadRequest();

            return _mapper.Map<ListSystemUserDto>(systemUser);
        }

        // DELETE: api/usuarios/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _systemUserRepository.DeleteAsync(id);
                if (!result)
                {
                    return BadRequest();
                }
                return NoContent();
            }
            catch (Exception excepcion)
            {
                return BadRequest();
            }
        }
                
        // POST: api/usuarios/cambiarperfil
        [HttpPost]
        [Route("cambiarperfil")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostCambiarPerfil(SystemUserProfileDto perfilUsuarioDto)
        {
            try
            {
                var systemUser = _mapper.Map<SystemUser>(perfilUsuarioDto);

                var result = await _systemUserRepository.ChangeProfile(systemUser);
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

        // POST: api/usuarios/validarusuario
        [HttpPost]
        [Route("validarusuario")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SystemUserRegisterDto>> PostValidatePassword(LoginModelDto loginModelDto)
        {
            try
            {
                var systemUser = _mapper.Map<SystemUser>(loginModelDto);

                var result = await _systemUserRepository.ValidatePassword(systemUser);
                if (!result)
                {
                    return BadRequest();
                }
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // POST: api/usuarios/cambiarcontrasena
        [HttpPost]
        [Route("cambiarcontrasena")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword(LoginModelDto loginModelDto)
        {
            try
            {
                var systemUser = _mapper.Map<SystemUser>(loginModelDto);
                var result = await _systemUserRepository.ChangePassword(systemUser);
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

        [HttpGet]
        [Route("{id}/accesousuario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AccessSysteUserModelDto>> UserAccess(int id)
        {
            try
            {
                var systemUser = await _systemUserRepository.GetAccess(id);
                if (systemUser == null)
                {
                    return NotFound();
                }
                return _mapper.Map<AccessSysteUserModelDto>(systemUser); ;
            }
            catch (Exception ex)  
            {

                throw;
            }

        }

    }
}