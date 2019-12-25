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
        public async Task<ActionResult<IEnumerable<ListCompanyHeadquarterDto>>> Get()
        {
            try
            {
                var companyHeadquarters = await _companyHeadquarterRepository.GetAllAsync();
                return _mapper.Map<List<ListCompanyHeadquarterDto>>(companyHeadquarters);
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
        public async Task<ActionResult<ListCompanyHeadquarterDto>> Get(int id)
        {
            try
            {
                var companyHeadquarter = await _companyHeadquarterRepository.GetAsync(id);
                if (companyHeadquarter == null)
                {
                    return NotFound();
                }
                return _mapper.Map<ListCompanyHeadquarterDto>(companyHeadquarter) ;
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
        public async Task<ActionResult<ListCompanyHeadquarterDto>> Post(CompanyHeadquarterRegisterDto companyDto)
        {
            try
            {
                var companyHeadquarter = _mapper.Map<CompanyHeadquarter>(companyDto);

                var newCompanyHeadquarter = await _companyHeadquarterRepository.AddAsync(companyHeadquarter);
                if (newCompanyHeadquarter == null)
                {
                    return BadRequest();
                }
                
                var newCompanyHeadquarterDto = _mapper.Map<ListCompanyHeadquarterDto>(newCompanyHeadquarter);
                return CreatedAtAction(nameof(Post), new { id = newCompanyHeadquarter.i_CompanyHeadquarterId}, newCompanyHeadquarterDto);

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
        public async Task<ActionResult<ListCompanyHeadquarterDto>> Put(int id, [FromBody]CompanyHeadquarterUpdateDataDto companyHeadquarterDto)
        {
            if (companyHeadquarterDto == null)
                return NotFound();

            var companyHeadquarter = _mapper.Map<CompanyHeadquarter>(companyHeadquarterDto);
            var result = await _companyHeadquarterRepository.UpdateAsync(companyHeadquarter);
            if (!result)
                return BadRequest();

            return _mapper.Map<ListCompanyHeadquarterDto>(companyHeadquarter);
        }

        // DELETE: api/usuarios/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _companyHeadquarterRepository.DeleteAsync(id);
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