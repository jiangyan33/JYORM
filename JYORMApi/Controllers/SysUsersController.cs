using JYORMApi.Entity;
using JYORMApi.Model;
using JYORMApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MyCommonTool.Models;
using MyCommonTool.Utils;
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
        /// 新增
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<Result<int>> Add([FromBody] SysUser sysUser)
        {
            if (string.IsNullOrEmpty(sysUser.UserName) || string.IsNullOrEmpty(sysUser.Password)) throw new CommonException(ResultCode.ArgumentError);
            var id = Convert.ToInt64(HttpContext.Items["Id"]);
            sysUser.UpdateBy = id;
            sysUser.CreateBy = id;
            string key = _configuration["AppSettings:Key"];
            sysUser.Password = CommonUtils.Encrypt(key, sysUser.Password);

            var result = await _sysUserService.Add(sysUser);

            return new Result<int>(result);
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

            // 对密码进行加密
            var key = _configuration.GetSection("AppSettings")["Key"];
            var expireTime = int.Parse(_configuration["AppSettings:ExpireTime"]);

            sysUser.Password = CommonUtils.Encrypt(key, sysUser.Password);
            var result = await _sysUserService.Get(sysUser);
            if (result.Count == 0) return new Result<string>(null);

            // 生成JWT验证Token
            //创建授权声明(Payload中的内容)
            var authClaims = new List<Claim> {
                new Claim(ClaimTypes.Sid,result[0].Id.ToString())
            };
            // 生成token
            var jwtSecurityToken = new JwtSecurityToken(
                    claims: authClaims,
                    expires: DateTime.Now.AddMinutes(expireTime),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256)
                    );
            var token = "Bearer " + new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return new Result<string>(token);
        }
    }
}
