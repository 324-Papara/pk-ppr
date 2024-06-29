using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Pa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneController : ControllerBase
    {
        // GET: api/<PhoneController.cs>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PhoneController.cs>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PhoneController.cs>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PhoneController.cs>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PhoneController.cs>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
