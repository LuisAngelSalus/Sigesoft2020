using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SL.Sigesoft.Data;
using SL.Sigesoft.Data.Contracts;
using SL.Sigesoft.Dtos;
using SL.Sigesoft.Models;

namespace SL.Sigesoft.WebApi.Controllers
{
    [Authorize(Roles = "Administrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {        
        private IPersonsRepository _personsRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PeopleController> _logger;
        public PeopleController(IPersonsRepository personsRepository, IMapper mapper, ILogger<PeopleController> logger)
        {
            _personsRepository = personsRepository;
            this._mapper = mapper;
            this._logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PersonDto>>> Get()
        {
            try
            {
                var people = await _personsRepository.GetPersonsAsync();
                return _mapper.Map<List<PersonDto>>(people);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Get)}: {ex.Message}");
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PersonDto>> Get(int id)
        {
            try
            {
                var person = await _personsRepository.GetPersonAsync(id);

                if (person == null)
                {
                    return NotFound();
                }

                return _mapper.Map<PersonDto>(person);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error {nameof(Post)}: {ex.Message}");
                throw;
            }

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PersonDto>> Post(PersonDto personDto)
        {
            try
            {
                var person = _mapper.Map<Person>(personDto);
                var nuevoProducto = await _personsRepository.AddAsync(person);

                if (nuevoProducto == null)
                {
                    return BadRequest();
                }
                var newPersonDto = _mapper.Map<PersonDto>(nuevoProducto);

                return CreatedAtAction(nameof(Post), new { id = newPersonDto.i_PersonId }, newPersonDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error {nameof(Post)}: {ex.Message}");
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PersonDto>> Put(int id, [FromBody]PersonDto personDto)
        {
            try
            {
                if (personDto == null)
                {
                    return NotFound();
                }

                var person = _mapper.Map<Person>(personDto);

                var resultado = await _personsRepository.UpdateAsync(person);
                if (!resultado)
                {
                    return BadRequest();
                }
                return personDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Put)}: {ex.Message}");
                return BadRequest();
            }

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Person>> Delete(int id)
        {
            try
            {
                var resultado = await _personsRepository.DeleteAsync(id);
                if (!resultado)
                {
                    return BadRequest();
                }
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Delete)}: {ex.Message}");
                return BadRequest();

            }

        }
        
    }
}
