using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SL.Sigesoft.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ValuesController:ControllerBase
    {
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
        public void Post([FromBody] string value)
        {

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
