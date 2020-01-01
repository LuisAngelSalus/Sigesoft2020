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

namespace SL.Sigesoft.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotationController : ControllerBase
    {
        private IQuotationRepository _quotationRepository;
        private readonly IMapper _mapper;

        public QuotationController(IQuotationRepository quotationRepository, IMapper mapper)
        {
            this._quotationRepository = quotationRepository;
            this._mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<QuotationDto>>> Get(int id)
        {
            var response = new Response<QuotationDto>();
            try
            {
                var quotation = await _quotationRepository.GetQuotationAsync(id);
                if (quotation == null)
                {
                    return NotFound();
                }
                response.Data = _mapper.Map<QuotationDto>(quotation);
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