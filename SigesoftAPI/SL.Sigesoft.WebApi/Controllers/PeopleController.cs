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
using SL.Sigesoft.Common;
using SL.Sigesoft.Data;
using SL.Sigesoft.Data.Contracts;
using SL.Sigesoft.Dtos;
using SL.Sigesoft.Models;

namespace SL.Sigesoft.WebApi.Controllers
{
    [Authorize(Roles = "Administrador,Sistemas")]
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
        public async Task<ActionResult<Response<PersonDto>>> Get(int id)
        {
            var response = new Response<PersonDto>();
            try
            {
                var person = await _personsRepository.GetPersonAsync(id);
                response.Data = _mapper.Map<PersonDto>(person);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa";
                }

                return response;
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
        public async Task<ActionResult<Response<PersonDto>>> Post(PersonRegistertDto personDto)
        {
            var response = new Response<PersonDto>();
            try
            {
                var person = _mapper.Map<Person>(personDto);

                var newPerson = await _personsRepository.AddAsync(person);

                if (newPerson == null)
                {
                    return BadRequest();
                }

                var newPersonDto = _mapper.Map<PersonDto>(newPerson);
                response.Data = newPersonDto;
                response.IsSuccess = true;
                response.Message = "Se grabó correctamente";

                return Ok(response);
                
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
        public async Task<ActionResult<Response<PersonRegistertDto>>> Put(int id, [FromBody]PersonUpdateDto personDto)
        {
            var response = new Response<PersonRegistertDto>();
            try
            {
                if (personDto == null)
                    return NotFound();

                var person = _mapper.Map<Person>(personDto);
                var resultado = await _personsRepository.UpdateAsync(person);
                if (!resultado)
                    return BadRequest();

                response.Data = _mapper.Map<PersonRegistertDto>(person);
                response.IsSuccess = true;
                response.Message = "Se actualizó correctamente";

                return response;
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
