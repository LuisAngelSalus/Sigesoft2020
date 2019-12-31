using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SL.Sigesoft.Common;
using SL.Sigesoft.Data.Contracts;
using SL.Sigesoft.Data.Repositories;
using SL.Sigesoft.Dtos;

namespace SL.Sigesoft.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private IInfoRepository _infoRepository;
        private readonly IMapper _mapper;

        public InfoController(IInfoRepository infoRepository, IMapper mapper)
        {
            this._infoRepository = infoRepository;
            this._mapper = mapper;
        }

        // GET: api/usuarios/5
        [HttpGet("{ruc}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<InfoDto>>> Get(string ruc)
        {
            var response = new Response<InfoDto>();
            try
            {
                var info = await _infoRepository.GetInfo(ruc);
                if (info == null)
                {
                    return NotFound();
                }
                response.Data = _mapper.Map<InfoDto>(info);
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