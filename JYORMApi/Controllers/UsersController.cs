using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JYORMApi.Entity;
using JYORMApi.Model;
using JYORMApi.Service;

namespace JYORMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;

        private readonly ISysUserService _sysUserService;

        public UsersController(ILogger<UsersController> logger, ISysUserService sysUserService)
        {
            _logger = logger;
            _sysUserService = sysUserService;
        }

        [HttpGet("login")]
        public async Task<Result> Get([FromQuery] User user)
        {
            var res = await _sysUserService.AddOne(new SysUser());
            //if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Password)) throw new CoreCommonException("用户名或者密码不能为空", ResultCode.ArgumentError);
            //var result = await _userService.Login(user);
            //if (result == null) throw new CoreCommonException("用户名或者密码错误", ResultCode.ArgumentError);

            return new Result(res);
        }

        [HttpPost("RefreshToken")]
        [Authorize]
        public async Task<Result> RefreshToken()
        {
            var user = new User { Id = Convert.ToInt64(HttpContext.Items["Id"]) };

            var result = await _userService.RefreshToken(user);
            if (result == null) throw new CoreCommonException("Token刷新失败", ResultCode.AuthError);

            return new Result(data: result);
        }

        [HttpGet("Test")]
        public async Task<List<SysUser>> Test()
        {
            return await _sysUserService.List();
        }
    }
}