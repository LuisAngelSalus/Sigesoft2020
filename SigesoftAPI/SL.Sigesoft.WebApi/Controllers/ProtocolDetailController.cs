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
    public class ProtocolDetailController : ControllerBase
    {
        private IProtocolDetailRepository _protocolDetailRepository;
        private readonly IMapper _mapper;

        public ProtocolDetailController(IProtocolDetailRepository protocolDetailRepository, IMapper mapper)
        {
            this._protocolDetailRepository = protocolDetailRepository;
            this._mapper = mapper;
        }
    }
}