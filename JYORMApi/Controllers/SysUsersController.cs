using JYORMApi.Entity;
using JYORMApi.Model;
using JYORMApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JYORMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SysUsersController : ControllerBase
    {
        private readonly ILogger<SysUsersController> _logger;

        private readonly ISysUserService _sysUserService;

        private readonly IConfiguration _configuration;

        public SysUsersController(ILogger<SysUsersController> logger, ISysUserService sysUserService, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _sysUserService = sysUserService;
        }

        /// <summary>
        ///  获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<Result<SysUser>> Get()
        {
            var id = Convert.ToInt64(HttpContext.Items["Id"]);
            var result = await _sysUserService.Get(id);
            return new Result<SysUser>(result);
        }

        /// <summary>
        /// 分页查找
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        [HttpGet("Pages")]
        public async Task<Result<PageResult<SysUser>>> GetPages([FromQuery] SysUser sysUser)
        {
            var result = await _sysUserService.GetPages(sysUser);

            return new Result<PageResult<SysUser>>(result);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        [HttpGet("login")]
        public async Task<Result<string>> Get([FromQuery] SysUser sysUser)
        {
            if (string.IsNullOrEmpty(sysUser.UserName) || string.IsNullOrEmpty(sysUser.Password)) throw new CommonException(ResultCode.ArgumentError);

            var result = await _sysUserService.Get(sysUser);
            if (result.Count == 0) return new Result<string>(null);
            string key = _configuration["AppSettings:Key"];

            // 生成JWT验证Token
            //创建授权声明(Payload中的内容)
            var authClaims = new List<Claim> {
                new Claim(ClaimTypes.Sid,result[0].Id.ToString())
            };
            var date1 = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
            var date2 = DateTime.Now.AddMinutes(3);
            Console.WriteLine(date1.ToString("yyyy-MM-dd HH:mm:ss"));
            Console.WriteLine(date2.ToString("yyyy-MM-dd HH:mm:ss"));
            // 生成token
            var jwtSecurityToken = new JwtSecurityToken(
                    // 处理时差问题
                    //notBefore: DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
                    claims: authClaims,
                    expires: DateTime.Now.AddMinutes(1),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256)
                    );
            var token = "Bearer " + new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return new Result<string>(token);
        }
    }
}
