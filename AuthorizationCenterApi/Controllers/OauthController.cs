using AuthorizationCenterApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationCenterApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class OauthController : ControllerBase {
        private readonly IOauthService oauthService_;
        public OauthController(IOauthService oauthService) {
            oauthService_ = oauthService;
        }

        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="requestDto">用户名，密码</param>
        /// <returns></returns>
        
        // 表示controller/action 可以匿名访问
        [AllowAnonymous]
        [HttpPost("requestToken")]
        
        public ActionResult RequestToken([FromBody] LoginRequestDto requestDto) {
            if (!ModelState.IsValid) {
                return BadRequest("invalied request body");
            }

            string token;
            // 调用身份认证服务 对登录请求进行验证 并且 签发token
            if (oauthService_.IsAuthenticated(requestDto, out token)) {
                return Ok(token);
            }

            return BadRequest("login failed.");
        }
    }
}
