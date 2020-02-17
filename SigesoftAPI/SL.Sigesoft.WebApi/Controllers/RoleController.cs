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

namespace SL.Sigesoft.WebApi.Controllers
{
    [Authorize(Roles = "Administrador,Sistemas")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private IRoleRepository _RoleRepository;
        private readonly IMapper _mapper;

        public RoleController(IRoleRepository RoleRepository, IMapper mapper)
        {
            this._RoleRepository = RoleRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<IEnumerable<ListRoleDto>>>> Get()
        {
            var response = new Response<IEnumerable<ListRoleDto>>();
            try
            {
                var roles = await _RoleRepository.GetAllAsync();
                response.Data = _mapper.Map<List<ListRoleDto>>(roles);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa";
                }

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return response;
        }

    }
}