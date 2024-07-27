using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Para.Api.Filter;

namespace Para.Api.Controllers
{
    
    [ApiController]
    [NonController]
    [Route("api/[controller]")]
    public class TokenTestController : ControllerBase
    {
       
        [TypeFilter(typeof(LogResourceFilter))]
        [TypeFilter(typeof(LogActionFilter))]
        [TypeFilter(typeof(LogAuthorizationFilter))]
        [TypeFilter(typeof(LogResultFilter))]
        [TypeFilter(typeof(LogExceptionFilter))]
        [HttpGet("NoToken")]
        public  string Get() { return "NoToken"; }

        [HttpGet("Authorize")]
        [Authorize]
        public  string Authorize() { return "Authorize"; }
        
        [HttpGet("Admin")]
        [Authorize(Roles = "admin")]
        public  string Admin() { return "Admin"; }
        
        [HttpGet("Manager")]
        [Authorize(Roles = "manager")]
        public  string Manager() { return "Manager"; }
        
        [HttpGet("ManagerAndAdmin")]
        [Authorize(Roles = "manager,admin")]
        public  string ManagerAndAdmin() { return "ManagerAndAdmin"; }
        
    }
}
