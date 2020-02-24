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
    [Authorize(Roles = "Sistemas,Médico,Recepeción,Gerencia,ComercialAdministrador,Cliente")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountSettingController : ControllerBase
    {
        private IAccountSettingRepository _accountSettingRepository;
        private readonly IMapper _mapper;

        public AccountSettingController(IAccountSettingRepository accountSettingRepository, IMapper mapper)
        {
            this._accountSettingRepository = accountSettingRepository;
            this._mapper = mapper;
        }

    }
}