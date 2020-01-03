using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SL.Sigesoft.Common;
using SL.Sigesoft.Data.Contracts;
using SL.Sigesoft.Dtos;
using SL.Sigesoft.Models;

namespace SL.Sigesoft.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecuentialController : ControllerBase
    {
        private ISecuentialRespository _secuentialRespository;
        private readonly IMapper _mapper;

        public SecuentialController(ISecuentialRespository secuentialRespository, IMapper mapper)
        {
            this._secuentialRespository = secuentialRespository;
            this._mapper = mapper;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<int>>> Post(SecuentialDto secuentialDto)
        {
            var response = new Response<int>();
            try
            {
                var secuential = _mapper.Map<Secuential>(secuentialDto);

                var newSecuential = await _secuentialRespository.GetCode(secuentialDto.Process, secuentialDto.SystemUserId, secuentialDto.OwnerCompanyId);
                response.Data = newSecuential;
                response.IsSuccess = true;
                response.Message = "Se generó correctamente el secuencial";
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}