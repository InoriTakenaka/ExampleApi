using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*------------------------------------------*/
using ExampleApi.Model;
using Microsoft.AspNetCore.Cors;

namespace ExampleApi.Controllers {
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase {
        [HttpPost]
        public IActionResult Login([FromBody]LoginRequestDto request ) {
            Console.WriteLine(request.Password);
            Console.WriteLine(request.UserName);
            if (!ModelState.IsValid) {
                return BadRequest("Invalid Request");
            }
            return Ok();
        }
    }
}
