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
    [Authorize(Roles = "Administrador,Comercial")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProtocolDetailController : ControllerBase
    {
        private IProtocolDetailRepository _protocolDetailRepository;
        private readonly IMapper _mapper;

        public ProtocolDetailController(IProtocolDetailRepository protocolDetailRepository, IMapper mapper)
        {
            this._protocolDetailRepository = protocolDetailRepository;
            this._mapper = mapper;
        }


        [HttpGet("{id}/DetalleProtocolo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<List<ProtocolDetailListDto>>>> GetAllByProtocolIdAsync(int id)
        {
            var response = new Response<List<ProtocolDetailListDto>>();
            try
            {
                var profile = await _protocolDetailRepository.GetAllByProtocolIdAsync(id);
                if (profile == null)
                {
                    return NotFound();
                }
                response.Data = _mapper.Map<List<ProtocolDetailListDto>>(profile);
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
        public async Task<ActionResult<Response<ProtocolDetailListDto>>> Post(ProtocolDetailRegisterDto protocolDetailRegisterDto)
        {
            var response = new Response<ProtocolDetailListDto>();
            try
            {
                var protocolDetail = _mapper.Map<ProtocolDetail>(protocolDetailRegisterDto);

                var newProtocolDetail = await _protocolDetailRepository.AddAsync(protocolDetail);
                if (newProtocolDetail == null)
                {
                    return BadRequest();
                }

                var newProtocolDetailDto = _mapper.Map<ProtocolDetailListDto>(newProtocolDetail);
                response.Data = newProtocolDetailDto;
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