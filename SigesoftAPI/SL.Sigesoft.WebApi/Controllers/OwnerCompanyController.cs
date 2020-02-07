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
    public class OwnerCompanyController : ControllerBase
    {
        private IOwnerCompanyRepository _OwnerCompanyRepository;
        private readonly IMapper _mapper;

        public OwnerCompanyController(IOwnerCompanyRepository OwnerCompanyRepository, IMapper mapper)
        {
            this._OwnerCompanyRepository = OwnerCompanyRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<IEnumerable<ListOwnerCompanyDto>>>> Get()
        {
            var response = new Response<IEnumerable<ListOwnerCompanyDto>>();
            try
            {
                var OwnerCompanys = await _OwnerCompanyRepository.GetAllAsync();
                response.Data = _mapper.Map<List<ListOwnerCompanyDto>>(OwnerCompanys);
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