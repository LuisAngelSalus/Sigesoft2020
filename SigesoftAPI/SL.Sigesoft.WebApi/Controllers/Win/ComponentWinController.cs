using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SL.Sigesoft.Common;
using SL.Sigesoft.Data.Contracts.Win;
using SL.Sigesoft.Dtos.Win;

namespace SL.Sigesoft.WebApi.Controllers.Win
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentWinController : ControllerBase
    {
        private IComponentRepository _componentRepository;
        private readonly IMapper _mapper;

        public ComponentWinController(IComponentRepository componentRepository, IMapper mapper)
        {
            this._componentRepository = componentRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<IEnumerable<ListComponentDto>>>> Get()
        {
            var response = new Response<IEnumerable<ListComponentDto>>();
            try
            {
                var components = await _componentRepository.GetAllAsync();
                response.Data = _mapper.Map<List<ListComponentDto>>(components);
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

    }
}