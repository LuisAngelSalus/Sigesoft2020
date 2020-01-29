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
    [Authorize(Roles = "Administrador,Comercial")]
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteTrackingController : ControllerBase
    {
        private IQuoteTrackingRepository _quoteTrackingRepository;
        private readonly IMapper _mapper;

        public QuoteTrackingController(IQuoteTrackingRepository quoteTrackingRepository, IMapper mapper)
        {
            this._quoteTrackingRepository = quoteTrackingRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] 
        public async Task<ActionResult<Response<IEnumerable<ListQuoteTrackingDto>>>> Get()
        {
            var response = new Response<IEnumerable<ListQuoteTrackingDto>>();
            try
            {
                var trackings = await _quoteTrackingRepository.GetAllAsync();
                response.Data = _mapper.Map<List<ListQuoteTrackingDto>>(trackings);
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
        public async Task<ActionResult<Response<ListQuoteTrackingDto>>> Get(int id)
        {
            var response = new Response<ListQuoteTrackingDto>();
            try
            {
                var quoteTracking = await _quoteTrackingRepository.GetAsync(id);
                if (quoteTracking == null)
                {
                    return NotFound();
                }
                response.Data = _mapper.Map<ListQuoteTrackingDto>(quoteTracking);
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
        public async Task<ActionResult<Response<ListQuoteTrackingDto>>> Post(QuoteTrackingRegisterDto quoteTrackingDto)
        {
            var response = new Response<ListQuoteTrackingDto>();
            try
            {
                var quoteTracking = _mapper.Map<QuoteTracking>(quoteTrackingDto);

                var newTracking = await _quoteTrackingRepository.AddAsync(quoteTracking);
                if (newTracking == null)
                {
                    return BadRequest();
                }

                var newTrackingDto = _mapper.Map<ListQuoteTrackingDto>(newTracking);
                response.Data = newTrackingDto;
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
        public async Task<ActionResult<Response<ListQuoteTrackingDto>>> Put([FromBody]QuoteTrackingUpdateDto quoteTrackingDto)
        {
            var response = new Response<ListQuoteTrackingDto>();
            if (quoteTrackingDto == null)
                return NotFound();

            var quoteTracking = _mapper.Map<QuoteTracking>(quoteTrackingDto);
            var result = await _quoteTrackingRepository.UpdateAsync(quoteTracking);
            if (!result)
                return BadRequest();

            response.Data = _mapper.Map<ListQuoteTrackingDto>(quoteTracking);
            response.IsSuccess = true;
            response.Message = "Se actualizó correctamente";

            //return _mapper.Map<ListQuoteTrackingDto>(quoteTracking);
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
                var result = await _quoteTrackingRepository.DeleteAsync(id);
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