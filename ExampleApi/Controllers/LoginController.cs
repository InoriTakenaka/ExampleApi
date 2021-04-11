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
    // 允许跨域访问这个控制器 
    // 参数为：跨域访问规则的名字
    [EnableCors("MyCorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase {
        [HttpPost]
        public LoginResponse Login([FromBody]LoginRequestDto request ) {
            Console.WriteLine(request.Password);
            Console.WriteLine(request.UserName);
            if (!ModelState.IsValid) {
                return new LoginResponse {
                    code = 1,
                    message = "login failed",
                };
            }
            return new LoginResponse {
                code = 0,
                message = "request success.",
                username = request.UserName
            };
        }
    }
}
