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
    [Authorize(Roles = "Administrador,Comercial,Cliente")]
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyRepository companyRepository, IMapper mapper)
        {
            this._companyRepository = companyRepository;
            this._mapper = mapper;
        }

        [HttpPost]
        [Route("Filter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<IEnumerable<ListCompanyDto>>>> Get(ParamsCompanyFilterModel paramsCompany)
        {
            var response = new Response<IEnumerable<ListCompanyDto>>();
            try
            {
                var companies = await _companyRepository.GetAllFilterAsync(paramsCompany);
                response.Data = _mapper.Map<List<ListCompanyDto>>(companies);
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

        //// GET: api/usuarios/5
        //[HttpGet("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<Response<ListCompanyDto>>> Get(int id)
        //{
        //    var response = new Response<ListCompanyDto>();
        //    try
        //    {
        //        var company = await _companyRepository.GetAsync(id);
        //        if (company == null)
        //        {
        //            return NotFound();
        //        }
        //        response.Data = _mapper.Map<ListCompanyDto>(company);
        //        if (response.Data != null)
        //        {
        //            response.IsSuccess = true;
        //            response.Message = "Consulta Exitosa";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }   
        //    return response;

        //}

        // POST: api/usuarios
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<ListCompanyDto>>> Post(CompanyRegisterDto companyDto)
        {
            var response = new Response<ListCompanyDto>();
            try
            {
                var company = _mapper.Map<Company>(companyDto);

                var newCompany = await _companyRepository.AddAsync(company);
                if (newCompany == null)
                {
                    response.IsSuccess = false;
                    response.Message = "error al grabar empresa";
                    return BadRequest(response);
                }

                var newCompanyDto = _mapper.Map<ListCompanyDto>(newCompany);
                response.Data = newCompanyDto;
                response.IsSuccess = true;
                response.Message = "Se grabó correctamente";                
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<ListCompanyDto>>> Put([FromBody]CompanyUpdateDataDto companyDto)
        {
            var response = new Response<ListCompanyDto>();
            if (companyDto == null)
                return NotFound();

            var company = _mapper.Map<Company>(companyDto);
            var result = await _companyRepository.UpdateAsync(company);
            if (!result) {
                response.Data = new ListCompanyDto();
                response.IsSuccess = false;
                response.Message = "Error en la operación";
                return BadRequest(response);
            }                

            response.Data = _mapper.Map<ListCompanyDto>(company);
            response.IsSuccess = true;
            response.Message = "Se actualizó correctamente";

            //return _mapper.Map<ListCompanyDto>(company);
            return response;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<bool>>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                var result = await _companyRepository.DeleteAsync(id);
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

        [HttpGet("{id}/sedes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<CompanyDto>>> GetCompanyWithHeadquarter(int id)
        {
            var response = new Response<CompanyDto>();
            var orden = await _companyRepository.GetCompanyWithHeadquarter(id);
            if (orden == null)
            {
                return NotFound();
            }

            response.Data = _mapper.Map<CompanyDto>(orden);
            if (response.Data != null)
            {
                response.IsSuccess = true;
                response.Message = "Consulta Exitosa";
            }
            return response;
        }

        [HttpGet("{ruc}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<CompanyDto>>> Get(string ruc)
        {
            var response = new Response<CompanyDto>();
            try
            {
                var company = await _companyRepository.GetCompanyByRuc(ruc);
                if (company == null)
                {
                    return NotFound();
                }
                response.Data = _mapper.Map<CompanyDto>(company);
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

        [HttpGet("{id}/{ruc}/ValidarResponsable")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> ValidateUserCompany(int id, string ruc)
        {
            try
            {
                var result = await _companyRepository.ValidateCompanyIsMine(id, ruc);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }           
        }

        [HttpGet("{value}/AutocompleteByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<List<CompanyDto>>>> AutocompleteByName(string value)
        {
            var response = new Response<List<CompanyDto>>();
            try
            {
                var companies = await _companyRepository.AutocompleteByName(value);
                if (companies == null)
                {
                    return NotFound();
                }
                response.Data = _mapper.Map<List<CompanyDto>>(companies);
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