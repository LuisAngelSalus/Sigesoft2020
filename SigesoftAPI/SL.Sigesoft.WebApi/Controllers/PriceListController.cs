using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SL.Sigesoft.Common;
using SL.Sigesoft.Data.Contracts;
using SL.Sigesoft.Dtos;
using SL.Sigesoft.Models;

namespace SL.Sigesoft.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceListController : ControllerBase
    {
        private IPriceListRepository _priceListRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PriceListController> _logger;

        public PriceListController(IPriceListRepository priceListRepository, IMapper mapper, ILogger<PriceListController> logger)
        {
            this._priceListRepository = priceListRepository;
            this._mapper = mapper;
            this._logger = logger;
        }

        [HttpGet("{id}/ListaPrecio")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<IEnumerable<PriceListDto>>>> GetAll(int id)
        {
            var response = new Response<IEnumerable<PriceListDto>>();
            try
            {
                var priceList = await _priceListRepository.GetAllByCompanyIdAsync(id);
                response.Data = _mapper.Map<List<PriceListDto>>(priceList);
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

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PriceListDto>> Put(int id, [FromBody]PriceListDto priceListDto)
        {
            try
            {
                if (priceListDto == null)
                {
                    return NotFound();
                }

                var priceList = _mapper.Map<PriceList>(priceListDto);

                var resultado = await _priceListRepository.UpdateAsync(priceList);
                if (!resultado)
                {
                    return BadRequest();
                }
                return priceListDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Put)}: {ex.Message}");
                return BadRequest();
            }

        }

    }
}