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
    public class CompanyHeadquarterController : ControllerBase
    {
        //CompanyHeadquarter
        private ICompanyHeadquarterRepository _companyHeadquarterRepository;
        private readonly IMapper _mapper;

        public CompanyHeadquarterController(ICompanyHeadquarterRepository companyHeadquarterRepository, IMapper mapper)
        {
            this._companyHeadquarterRepository = companyHeadquarterRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<IEnumerable<ListCompanyHeadquarterDto>>>> Get()
        {
            var response = new Response<IEnumerable<ListCompanyHeadquarterDto>>();
            try
            {
                var companyHeadquarters = await _companyHeadquarterRepository.GetAllAsync();
                response.Data = _mapper.Map<List<ListCompanyHeadquarterDto>>(companyHeadquarters);
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
        public async Task<ActionResult<Response<ListCompanyHeadquarterDto>>> Get(int id)
        {
            var response = new Response<ListCompanyHeadquarterDto>();
            try
            {
                var companyHeadquarter = await _companyHeadquarterRepository.GetAsync(id);
                if (companyHeadquarter == null)
                {
                    return NotFound();
                }
                response.Data = _mapper.Map<ListCompanyHeadquarterDto>(companyHeadquarter);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa";
                }
                return response;
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
        public async Task<ActionResult<Response<ListCompanyHeadquarterDto>>> Post(CompanyHeadquarterRegisterDto companyHeadquarterDto)
        {
            var response = new Response<ListCompanyHeadquarterDto>();
            try
            {
                var companyHeadquarter = _mapper.Map<CompanyHeadquarter>(companyHeadquarterDto);

                var newCompanyHeadquarter = await _companyHeadquarterRepository.AddAsync(companyHeadquarter);
                if (newCompanyHeadquarter == null)
                {
                    return BadRequest();
                }
                
                var newCompanyHeadquarterDto = _mapper.Map<ListCompanyHeadquarterDto>(newCompanyHeadquarter);
                
                response.Data = newCompanyHeadquarterDto;
                response.IsSuccess = true;
                response.Message = "Se grabó correctamente";
                //return CreatedAtAction(nameof(Post), new { id = newCompany.i_CompanyId }, newCompanyDto);
                return response;

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
        public async Task<ActionResult<Response<ListCompanyHeadquarterDto>>> Put(int id, [FromBody]CompanyHeadquarterUpdateDataDto companyHeadquarterDto)
        {
            var response = new Response<ListCompanyHeadquarterDto>();
            if (companyHeadquarterDto == null)
                return NotFound();

            var companyHeadquarter = _mapper.Map<CompanyHeadquarter>(companyHeadquarterDto);
            var result = await _companyHeadquarterRepository.UpdateAsync(companyHeadquarter);
            if (!result)
                return BadRequest();

            response.Data = _mapper.Map<ListCompanyHeadquarterDto>(companyHeadquarter);
            response.IsSuccess = true;
            response.Message = "Se actualizó correctamente";
            
            return response;
            //return _mapper.Map<ListCompanyHeadquarterDto>(companyHeadquarter);
        }

        // DELETE: api/usuarios/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<bool>>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                var result = await _companyHeadquarterRepository.DeleteAsync(id);
                if (!result)
                {
                    return BadRequest();
                }
                response.Data = result;
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Eliminación Exitosa";
                }
                return response;
            }
            catch (Exception excepcion)
            {
                return BadRequest();
            }
        }

    }
}