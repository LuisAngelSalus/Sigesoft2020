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
using SL.Sigesoft.Models;

namespace SL.Sigesoft.WebApi.Controllers
{
    [Authorize(Roles = "Administrador,Sistemas,Médico,Recepción,Gerencia,Comercial,Cliente,Trabajador")]
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private IResultRepository _resultRepository;
        private readonly IMapper _mapper;
        public ResultsController(IResultRepository resultRepository, IMapper mapper)
        {
            this._resultRepository = resultRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<IEnumerable<ResultModel>>>> Get()
        {
            var response = new Response<IEnumerable<ResultModel>>();
            try
            {
                response.Data = await _resultRepository.GetAll();

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


        [HttpGet]
        [Route("Detail/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<IEnumerable<ResultDetailModel>>>> GetDetail(int id)
        {
            var response = new Response<IEnumerable<ResultDetailModel>>();
            try
            {
                response.Data = await _resultRepository.GetDetail(id);

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



    }
}