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
    public class QuotationController : ControllerBase
    {
        private IQuotationRepository _quotationRepository;
        private readonly IMapper _mapper;

        public QuotationController(IQuotationRepository quotationRepository, IMapper mapper)
        {
            this._quotationRepository = quotationRepository;
            this._mapper = mapper;
        }

        [HttpPost]
        [Route("Filter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<IEnumerable<QuotationFilterDto>>>> Get(ParamsQuotationFilterDto parameters)
        {
            var response = new Response<IEnumerable<QuotationFilterDto>>();
            try
            {
                var quotations = await _quotationRepository.GetFilterAsync(parameters);
                response.Data = _mapper.Map<List<QuotationFilterDto>>(quotations);
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<QuotationRegisterDto>>> Post(QuotationRegisterDto quotationDto)
        {
            var response = new Response<QuotationRegisterDto>();
            try
            {
                var quotation = _mapper.Map<Quotation>(quotationDto);

                var newQuotation = await _quotationRepository.AddAsync(quotation);
                if (newQuotation == null)
                {
                    return BadRequest();
                }

                var newQuotationDto = _mapper.Map<QuotationRegisterDto>(newQuotation);
                response.Data = newQuotationDto;
                response.IsSuccess = true;
                response.Message = "Se grabó correctamente";
                
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<QuotationRegisterDto>>> Put([FromBody]QuotationUpdateDto quotationDto)
        {
            var response = new Response<QuotationRegisterDto>();
            if (quotationDto == null)
                return NotFound();

            var quotation = _mapper.Map<Quotation>(quotationDto);
            var result = await _quotationRepository.UpdateAsync(quotation);
            if (!result)
                return BadRequest();

            response.Data = _mapper.Map<QuotationRegisterDto>(quotation);
            response.IsSuccess = true;
            response.Message = "Se actualizó correctamente";

            //return _mapper.Map<ListCompanyDto>(company);
            return response;
        }


        [HttpPost]
        [Route("NewVersion")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<QuotationRegisterDto>>> NewVersion(QuotationNewVersionDto quotationDto)
        {
            var response = new Response<QuotationRegisterDto>();
            try
            {
                var quotation = _mapper.Map<Quotation>(quotationDto);

                var newQuotation = await _quotationRepository.NewVersion(quotation);
                if (newQuotation == null)
                {
                    return BadRequest();
                }

                var newQuotationDto = _mapper.Map<QuotationRegisterDto>(newQuotation);
                response.Data = newQuotationDto;
                response.IsSuccess = true;
                response.Message = "Se grabó correctamente";

                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("Versions/{code}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<IEnumerable<QuotationVersionDto>>>> GetVersions(string code)
        {
            var response = new Response<IEnumerable<QuotationVersionDto>>();
            try
            {
                var quotations = await _quotationRepository.GetVersions(code);
                response.Data = _mapper.Map<List<QuotationVersionDto>>(quotations);
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


        [HttpPut]
        [Route("Process")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<bool>>> UpdateProccess([FromBody]QuotationUpdateProcess quotationDto)
        {
            var response = new Response<bool>();
            if (quotationDto == null)
                return NotFound();

            var quotation = _mapper.Map<Quotation>(quotationDto);
            var result = await _quotationRepository.UpdateIsProccess(quotation.v_Code,quotation.i_QuotationId);
            if (!result)
                return BadRequest();

            response.Data = result;
            response.IsSuccess = true;
            response.Message = "Se actualizó correctamente";

            return response;
        }

        [HttpPost]
        [Route("MigrateToProtocols")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<bool>>> MigrateQuotationToProtocols(QuotationMigrateDto quotationMigrateDto)
        {
            var response = new Response<bool>();
            if (quotationMigrateDto == null)
                return NotFound();

            var quotation = _mapper.Map<Quotation>(quotationMigrateDto);
            var result = await _quotationRepository.MigrateQuotationToProtocols(quotation.i_QuotationId);
            if (!result)
                return BadRequest();

            response.Data = result;
            response.IsSuccess = true;
            response.Message = "Se migró a protocolos correctamente";

            return response;
        }

    }
}