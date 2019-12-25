using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SL.Sigesoft.Data.Contracts;
using SL.Sigesoft.Dtos;
using SL.Sigesoft.Models;

namespace SL.Sigesoft.WebApi.Controllers
{
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ListCompanyDto>>> Get()
        {
            try
            {
                var companies = await _companyRepository.GetAllAsync();
                return _mapper.Map<List<ListCompanyDto>>(companies);
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
        public async Task<ActionResult<ListCompanyDto>> Get(int id)
        {
            try
            {
                var company = await _companyRepository.GetAsync(id);
                if (company == null)
                {
                    return NotFound();
                }
                return _mapper.Map<ListCompanyDto>(company); ;
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
        public async Task<ActionResult<ListCompanyDto>> Post(CompanyRegisterDto companyDto)
        {
            try
            {
                var company = _mapper.Map<Company>(companyDto);

                var newCompany = await _companyRepository.AddAsync(company);
                if (newCompany == null)
                {
                    return BadRequest();
                }

                var newCompanyDto = _mapper.Map<ListCompanyDto>(newCompany);
                return CreatedAtAction(nameof(Post), new { id = newCompany.i_CompanyId }, newCompanyDto);

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
        public async Task<ActionResult<ListCompanyDto>> Put(int id, [FromBody]CompanyUpdateDataDto companyDto)
        {
            if (companyDto == null)
                return NotFound();

            var company = _mapper.Map<Company>(companyDto);
            var result = await _companyRepository.UpdateAsync(company);
            if (!result)
                return BadRequest();

            return _mapper.Map<ListCompanyDto>(company);
        }

        // DELETE: api/usuarios/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _companyRepository.DeleteAsync(id);
                if (!result)
                {
                    return BadRequest();
                }
                return NoContent();
            }
            catch (Exception excepcion)
            {
                return BadRequest();
            }
        }

    }
}