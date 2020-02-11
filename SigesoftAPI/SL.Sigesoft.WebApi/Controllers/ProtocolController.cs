using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SL.Sigesoft.Data.Contracts;

namespace SL.Sigesoft.WebApi.Controllers
{
    [Authorize(Roles = "Administrador,Comercial")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProtocolController : ControllerBase
    {
        private IProtocolRepository _protocolRepository;
        private readonly IMapper _mapper;

        public ProtocolController(IProtocolRepository protocolRepository, IMapper mapper)
        {
            this._protocolRepository = protocolRepository;
            this._mapper = mapper;
        }



    }
}