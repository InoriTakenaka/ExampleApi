using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthorizationCenterApi.Models;
using Microsoft.Extensions.Configuration;

namespace AuthorizationCenterApi.Services {
    /// <summary>
    /// 对身份认证接口 进行实现
    /// </summary>
    public class OauthService:IOauthService {
        private readonly TokenManagement options_;

        public OauthService(IOptions<TokenManagement> options) {
            options_ = options.Value;
        }

        /*
        public OauthService (IConfiguration configuration ){
            var tokenConfig = configuration.GetSection("JwtToken");
        }
        */

        public bool IsAuthenticated(LoginRequestDto requestDto,out string token) {
            token = string.Empty;
            //对用户名，密码进行校验
            if (string.IsNullOrEmpty(requestDto.UserName))
                return false;

            // 登录用户的一些信息
            var authClaims = new[] {
                 new Claim(JwtRegisteredClaimNames.Sub,requestDto.UserName),
                 new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                 new Claim(ClaimTypes.Role,"Admin")
             }; 
            /*
             {
                "Role":"Admin"
             }
             
             */

            // 构造一个JwtBearer Token

            // 密钥
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options_.Secret));
            // 凭据
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                issuer: options_.Issuer,
                audience: options_.Audience,
                expires: DateTime.Now.AddMinutes(options_.AccessExpiration),
                claims: authClaims,
                signingCredentials: credentials
                );// 经过base64加密以后 转化成 payload 

            // 将 Jwt Token 写成字符串 
            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return true;
        }
    }
}
