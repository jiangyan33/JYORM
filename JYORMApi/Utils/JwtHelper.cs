using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JYORMApi.Entity;

namespace JYORMApi.Utils
{
    public class JwtHelper
    {
        /// <summary>
        /// 生成JWT
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expireTime"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string IssueJwt(string key, int expireTime, SysUser user)
        {
            //创建授权声明(Payload中的内容)
            var authClaims = new List<Claim> {
                new Claim(ClaimTypes.Sid,user.Id.ToString())
            };

            // 生成token
            var token = new JwtSecurityToken(
                    // 处理时差问题
                    notBefore: DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
                    claims: authClaims,
                    expires: DateTime.Now.AddMinutes(expireTime),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256)
                    );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static string GetUserName(string jwtStr)
        {
            try
            {
                var jwtHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
                return jwtToken.Payload[ClaimTypes.Name]?.ToString();
            }
            catch { return string.Empty; }
        }
    }
}