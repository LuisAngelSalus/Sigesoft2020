using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SL.Sigesoft.Data.Contracts;
using SL.Sigesoft.Models;
using SL.Sigesoft.WebApi.Services;

namespace SL.Sigesoft.WebApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private ISystemUserRepository _systemUserRepository;
        private TokenService _tokenService;
        private IMapper _mapper;
        public SessionController(ISystemUserRepository systemUserRepository,
                               IMapper mapper,
                               TokenService tokenService)
        {
            _systemUserRepository = systemUserRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> Login(LoginModelDto systemUserLogin)
        {
            var dataLoginUser = _mapper.Map<SystemUser>(systemUserLogin);

            var resultadoValidacion = await _systemUserRepository.ValidateLogin(dataLoginUser);
            if (!resultadoValidacion.result)
            {
                return BadRequest("Usuario/Contraseña Inválidos.");
            }
            return _tokenService.GenerarToken(resultadoValidacion.systemUser);

        }
    }
}