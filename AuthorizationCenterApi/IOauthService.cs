using AuthorizationCenterApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationCenterApi {
    /// <summary>
    /// 身份认证服务接口定义
    /// </summary>
   public interface IOauthService {
        /// <summary>
        /// 对登录请求 进行 身份认证 
        /// </summary>
        /// <param name="requestDto">用户名，密码</param>
        /// <param name="token">身份认证通过以后返回的JwtBearer Token</param>
        /// <returns></returns>
        bool IsAuthenticated(LoginRequestDto requestDto, out string token);
    }
}
