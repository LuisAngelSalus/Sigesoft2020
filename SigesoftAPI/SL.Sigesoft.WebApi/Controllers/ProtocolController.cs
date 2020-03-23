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
    [Authorize(Roles = "Administrador,Comercial,Cliente,Sistemas")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProtocolController : ControllerBase
    {
        private IProtocolRepository _protocolRepository;
        private readonly IMapper _mapper;

        public ProtocolController(IProtocolRepository protocolRepository, IMapper mapper)
        {
            this._protocolRepository = protocolRepository;
            this._mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<ProtocolDto>>> Get(int id)
        {
            var response = new Response<ProtocolDto>();
            try
            {
                var protocol = await _protocolRepository.GetAsync(id);
                if (protocol == null)
                {
                    return NotFound();
                }
                response.Data = _mapper.Map<ProtocolDto>(protocol);
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


        [HttpGet("{id}/ProcolosPorEmpresa")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<List<ProtocolListDto>>>> GetProtocolsByCompanyId(int id)
        {
            var response = new Response<List<ProtocolListDto>>();
            try
            {
                var protocols = await _protocolRepository.GetProtocolsByCompanyId(id);
                if (protocols == null)
                {
                    return NotFound();
                }
                response.Data = _mapper.Map<List<ProtocolListDto>>(protocols);
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
        public async Task<ActionResult<Response<ProtocolListDto>>> Post(ProtocolRegisterDto protocoltDto)
        {
            var response = new Response<ProtocolListDto>();
            try
            {
                var protocol = _mapper.Map<Protocol>(protocoltDto);

                var newProtocol = await _protocolRepository.AddAsync(protocol);
                if (newProtocol == null)
                {
                    return BadRequest();
                }

                var newProtocolDto = _mapper.Map<ProtocolListDto>(newProtocol);
                response.Data = newProtocolDto;
                response.IsSuccess = true;
                response.Message = "Se grabó correctamente";
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


        [HttpGet("{id}/ExamenesAdicionales")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<List<AdditionalComponentsModel>>>> GetAdditionalComponents(int id)
        {
            var response = new Response<List<AdditionalComponentsModel>>();
            try
            {
                var protocols = await _protocolRepository.GetAdditionalComponents(id);
                if (protocols == null)
                {
                    return NotFound();
                }
                response.Data = _mapper.Map<List<AdditionalComponentsModel>>(protocols);
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

    }
}