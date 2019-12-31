﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SL.Sigesoft.Common;
using SL.Sigesoft.Data.Contracts;
using SL.Sigesoft.Models;

namespace SL.Sigesoft.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProtocolProfileController : ControllerBase
    {
        private IProtocolProfileRepository _protocolProfileRepository;
        private readonly IMapper _mapper;

        public ProtocolProfileController(IProtocolProfileRepository protocolProfileRepository, IMapper mapper)
        {
            this._protocolProfileRepository = protocolProfileRepository;
            this._mapper = mapper;
        }

        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<ProtocolProfileModel>>> Get(int id)
        {
            var response = new Response<ProtocolProfileModel>();
            try
            {
                var profile = await _protocolProfileRepository.GetProfile(id);
                if (profile == null)
                {
                    return NotFound();
                }
                response.Data = _mapper.Map<ProtocolProfileModel>(profile);
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