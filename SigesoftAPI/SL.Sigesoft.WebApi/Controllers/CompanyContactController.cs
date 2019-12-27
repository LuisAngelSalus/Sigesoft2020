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
    public class CompanyContactController : ControllerBase
    {
        private ICompanyContactRepository _companyContactRepository;
        private readonly IMapper _mapper;

        public CompanyContactController(ICompanyContactRepository companyContactRepository, IMapper mapper)
        {
            this._companyContactRepository = companyContactRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<IEnumerable<ListCompanyContactDto>>>> Get()
        {
            var response = new Response<IEnumerable<ListCompanyContactDto>>();
            try
            {
                var contacts = await _companyContactRepository.GetAllAsync();
                response.Data = _mapper.Map<List<ListCompanyContactDto>>(contacts);
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

        // GET: api/usuarios/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<ListCompanyContactDto>>> Get(int id)
        {
            var response = new Response<ListCompanyContactDto>();
            try
            {
                var contact = await _companyContactRepository.GetAsync(id);
                if (contact == null)
                {
                    return NotFound();
                }
                response.Data = _mapper.Map<ListCompanyContactDto>(contact);
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

        // POST: api/usuarios
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<ListCompanyContactDto>>> Post(CompanyRegisterDto companyContactDto)
        {
            var response = new Response<ListCompanyContactDto>();
            try
            {
                var contact = _mapper.Map<CompanyContact>(companyContactDto);

                var newContact = await _companyContactRepository.AddAsync(contact);
                if (newContact == null)
                {
                    return BadRequest();
                }

                var newContactDto = _mapper.Map<ListCompanyContactDto>(newContact);
                response.Data = newContactDto;
                response.IsSuccess = true;
                response.Message = "Se grabó correctamente";
                return Ok(response);

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
        public async Task<ActionResult<Response<ListCompanyContactDto>>> Put([FromBody]CompanyContactUpdateDataDto companyContactDto)
        {
            var response = new Response<ListCompanyContactDto>();
            if (companyContactDto == null)
                return NotFound();

            var company = _mapper.Map<CompanyContact>(companyContactDto);
            var result = await _companyContactRepository.UpdateAsync(company);
            if (!result)
                return BadRequest();

            response.Data = _mapper.Map<ListCompanyContactDto>(company);
            response.IsSuccess = true;
            response.Message = "Se actualizó correctamente";

            //return _mapper.Map<ListCompanyContactDto>(company);
            return response;
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
                var result = await _companyContactRepository.DeleteAsync(id);
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

        [HttpGet("{id}/contactos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<List<ListCompanyContactDto>>>> GetAllByCompanyId(int id)
        {
            var response = new Response<List<ListCompanyContactDto>>();
            var contacts = await _companyContactRepository.GetAllByCompanyId(id);
            if (contacts == null)
            {
                return NotFound();
            }

            response.Data = _mapper.Map<List<ListCompanyContactDto>>(contacts);
            if (response.Data != null)
            {
                response.IsSuccess = true;
                response.Message = "Consulta Exitosa";
            }
            return response;
        }




    }
}