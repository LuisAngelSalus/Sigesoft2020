using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SL.Sigesoft.Dtos;
using SL.Sigesoft.WebApi.Services;

namespace SL.Sigesoft.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailSender _emailSender;

        public EmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpPost]        
        public void Post(EmailDto emailDto)
        {
            _emailSender
               .SendEmailAsync(emailDto.Email, emailDto.Subject, emailDto.Message)
               .ConfigureAwait(false);
        }
    }
}