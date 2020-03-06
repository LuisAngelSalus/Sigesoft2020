using Microsoft.AspNetCore.Mvc;
using SL.Sigesoft.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SL.Sigesoft.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ValuesController : ControllerBase
    {
        private readonly IEmailSender _emailSender;

        public ValuesController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }



        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post()
        {
            _emailSender
               .SendEmailAsync("pool7592@gmail.com", "Asunto", "Mensaje")
               .ConfigureAwait(false);
        }

        [HttpPut]
        public void Put([FromBody] string value)
        {

        }

        [HttpDelete]
        public void Delete(int id)
        {

        }

    }
}
