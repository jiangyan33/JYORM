using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
        /// <param name="userid"></param>
        /// <param name="userName"></param>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public static string IssueJwt(string key, int expireTime, User user, string[] roleCode = null)
        {
            //创建claim
            var authClaims = new List<Claim> {
                new Claim(ClaimTypes.Name,user.Name??string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti,user.Id.ToString())
            };

            if (roleCode != null)
                authClaims.AddRange(roleCode.Select(x => new Claim(ClaimTypes.Role, x)));

            // 过期时间
            var utcNow = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);

            //秘钥16位
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                    // 处理时差问题
                    notBefore: utcNow,
                    claims: authClaims,
                    expires: user.UpdateTokenTime.AddMinutes(expireTime),
                    signingCredentials: creds
                    );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// 获取过期时间
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static DateTime GetExp(string jwtStr)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);

            return DateTimeHelper.GetDateTimeFromTimestamp((jwtToken.Payload[JwtRegisteredClaimNames.Exp] ?? 0).ParseToLong());
        }

        public static bool IsExp(string jwtStr)
        {
            return GetExp(jwtStr) < DateTime.Now;
        }

        public static string GetUserId(string jwtStr)
        {
            try
            {
                return new JwtSecurityTokenHandler().ReadJwtToken(jwtStr).Id;
            }
            catch
            {
                return string.Empty;
            }
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

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static string GetUserRoleCodes(string jwtStr)
        {
            try
            {
                var jwtHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
                return jwtToken.Payload[ClaimTypes.Role]?.ToString();
            }
            catch { return string.Empty; }
        }
    }
}